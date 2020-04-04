import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { ProjectComponent } from './project.component';
import { NewProjectComponent } from './newproject/newproject.component';
import { ProjectListComponent } from './projectList/projectList.component';

const routes: Routes = [
  {
    path: 'home', component: ProjectComponent,
    data: {
      breadcrumb: '项目主页'
    }
  },
  {
    path: 'new', component: NewProjectComponent,
    data: {
      breadcrumb: '新增项目'
    }
  },
  {
    path: 'list', component: ProjectListComponent,
    data: {
      breadcrumb: '项目列表'
    }
  }
];

export const ProjectRoutes = RouterModule.forChild(routes);


@NgModule({
  imports: [
    ProjectRoutes
  ]
})
export class ProjectRoutingModule { }
