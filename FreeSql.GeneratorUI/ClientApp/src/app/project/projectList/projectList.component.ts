import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProjectViewmodal } from '../modals/project';
import { PageView } from 'src/app/base/modalbase';

@Component({
  selector: 'app-projectlist',
  templateUrl: './projectList.component.html',
  styleUrls: ['./projectList.component.css']
})
export class ProjectListComponent implements OnInit {

  pageData: PageView<ProjectViewmodal>;
  constructor(private client: HttpClient) {
    this.pageData = new PageView();
    this.client.get<PageView<ProjectViewmodal>>('http://localhost:5000/api/project/page?PageSize=10&PageNumber=1').subscribe(
      (data) => {
        this.pageData = data;
      },
      error => console.log(error)
    );
  }

  ngOnInit() {

  }
}
