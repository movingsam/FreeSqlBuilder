import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ImportData } from '../modals/keyword';
import { Template, RootPath } from '../modals/template';
import { NzMessageService } from 'ng-zorro-antd/message';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-template',
  templateUrl: './newTemplate.component.html',
  styleUrls: ['./newTemplate.component.css']
})
export class NewTemplateComponent implements OnInit {

  templateName = '';
  value = '';
  rootPath = '';
  rootPrefix = '';
  code = '';
  constructor(private client: HttpClient, private message: NzMessageService, private router: Router) {
  }

  ngOnInit() {
    this.client.get<RootPath>('/api/RootPath')
      .subscribe((data) => {
        this.rootPath = data.root;
        const len = data.root.length;
        this.rootPrefix = len > 30 ? `${data.root.substring(0, 19)}...${data.root.substring(len - 10, len)}` : data.root;
      });
  }
  AddTemplate(): void {
    if (this.templateName === '') {
      this.message.error('未命名');
      return;
    }
    if (this.code === '') {
      this.message.error('模板不能为空');
      return;
    }
    const res = new Template();
    res.templateContent = this.code;
    res.templateName = this.templateName;
    res.templatePath = `${this.rootPath}\\${this.templateName}.cshtml`;
    this.client.post<ImportData>(`/api/Template`, res)
      .subscribe((data) => {
        console.log(data);
        this.code = data.fileContent;
      });
  }
  cahnged($event): void {
    this.value = $event;
  }
  import(): void {
    if (this.value.endsWith('.cshtml')) {
      this.client.get<ImportData>(`/api/Cshtml/Import?path=${this.value}`)
        .subscribe((data) => {
          console.log(data);
          this.code = data.fileContent;
        });
    } else {
      this.message.warning('需要选中cshtml模板');
    }
  }
  Return(): void {
    this.router.navigateByUrl('template/home');
  }
}

