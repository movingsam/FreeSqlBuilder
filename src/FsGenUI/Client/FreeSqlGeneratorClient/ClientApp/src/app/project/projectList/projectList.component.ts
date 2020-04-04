import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Project } from '../modals/project';
import { PageView } from 'src/app/base/modalbase';
import { NzMessageService } from 'ng-zorro-antd';
import { Router } from '@angular/router';

@Component({
  selector: 'app-projectlist',
  templateUrl: './projectList.component.html',
  styleUrls: ['./projectList.component.css']
})
export class ProjectListComponent implements OnInit {

  pageData: PageView<Project>;
  loading = false;
  constructor(private client: HttpClient, private router: Router
    , private message: NzMessageService) {
    this.pageData = new PageView();
  }
  loadData() {
    this.loading = true;
    this.client.get<PageView<Project>>(`/api/project/page?PageSize=${this.pageData.pageSize}&PageNumber=${this.pageData.pageNumber}`)
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
    this.loadData();
  }
  IndeChange(e): void {
    this.pageData.pageNumber = e;
    this.loadData();
  }
  SizeChange(e): void {
    this.pageData.pageSize = e;
    this.loadData();
  }
  delproj(id: number): void {
    this.client.delete<number>(`api/project/${id}`).subscribe(res => {
      if (res > 0) {
        this.message.success(`删除成功`);
        this.loadData();
      }
    });
  }
  renderGeneratorMode(data: Project): string {
    if (data.generatorModeConfig) {
      return data.generatorModeConfig.generatorMode === 0 ? 'DbFirst' : 'CodeFirst';
    }
    return '';
  }
  gotoEdit(id: number): void {
    this.router.navigate(['project/new'], {
      queryParams: {
        id: id
      }
    });
  }
  build(data: Project): void {
    this.client.post(`/api/project/task/build/${data.id}`, null).subscribe(res => {
      this.message.success(`生成成功---地址${data.projectInfo.rootPath}\\${data.projectInfo.outPutPath}`);
    });
  }
}
