import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

// 根目录
const appRoutes: Routes = [
  {
    path: '', redirectTo: 'project/new', pathMatch: `full`,
    data: {
      breadcrumb: '快速开始'
    }
  },
  {
    path: 'home', redirectTo: 'project/new', pathMatch: `full`,
    data: {
      breadcrumb: '快速开始'
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
      {
        enableTracing: true,
        useHash: true
      } // <-- debugging purposes only
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
