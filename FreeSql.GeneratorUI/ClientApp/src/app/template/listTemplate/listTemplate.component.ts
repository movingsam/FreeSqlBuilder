import { Component, OnInit } from '@angular/core';
import { Template } from '../modals/template';
import { PageView } from 'src/app/base/modalbase';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { Router } from '@angular/router';

@Component({
  selector: 'app-list-template',
  templateUrl: './listTemplate.component.html',
  styleUrls: ['./listTemplate.component.css']
})
export class ListTemplateComponent implements OnInit {

  pageData: PageView<Template> = new PageView();
  loading = false;

  constructor(private client: HttpClient, private message: NzMessageService, private router: Router) {
    this.pageData = new PageView();
    this.loadData();
  }
  loadData(): void {
    this.loading = true;
    this.client.get<PageView<Template>>(`http://localhost:5000/api/Template/Page?PageSize=${this.pageData.pageSize}&PageNumber=${this.pageData.pageNumber}`)
      .subscribe(
        (data) => {
          this.pageData = data;
          this.loading = false;
        },
        error => {
          console.log(error);
          this.loading = false;
        }
      );

  }

  ngOnInit() {

  }
  delete(id: number): void {
    const url = `http://localhost:5000/api/Template/${id}`;
    this.client.delete(url).subscribe((data) => {
      if (data === true) {
        this.message.info('成功');
        this.loadData();
      } else {
        this.message.info('失败');
      }
    });
  }
  IndeChange(e): void {
    this.pageData.pageNumber = e;
    this.loadData();
  }
  SizeChange(e): void {
    this.pageData.pageSize = e;
    this.loadData();
  }
  click(): void {
    this.router.navigateByUrl('template/home');
  }
}
