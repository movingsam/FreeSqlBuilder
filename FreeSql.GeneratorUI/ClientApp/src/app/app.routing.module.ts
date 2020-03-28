import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { HomeComponent } from './home/home.component';
// 根目录
const appRoutes: Routes = [
  {
    path: '', component: HomeComponent,
    data: {
      breadcrumb: '主页'
    }
  },
  {
    path: 'home', component: HomeComponent,
    data: {
      breadcrumb: '主页'
    }
  },
  {
    path: 'project', loadChildren: './project/project.module#ProjectModule',
    data: {
      breadcrumb: '项目'
    }
  }, {
    path: 'template', loadChildren: './template/template.module#TemplateModule',
    data: {
      breadcrumb: '模板'
    }
  }
];
export const AppRoutes = RouterModule.forChild(appRoutes);
@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
