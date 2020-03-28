import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { TemplateComponent } from './template.component';
import { ListTemplateComponent } from './listTemplate/listTemplate.component'
import { NewTemplateComponent } from './newTemplate/newTemplate.component';

const routes: Routes = [
  {
    path: 'home', component: TemplateComponent,
    data: {
      breadcrumb: '模板主页'
    }
  },
  {
    path: 'new', component: NewTemplateComponent,
    data: {
      breadcrumb: '新增模板'
    }
  },
  {
    path: 'list', component: ListTemplateComponent,
    data: {
      breadcrumb: '模板列表'
    }
  }
];

export const TemplateRoutes = RouterModule.forChild(routes);


@NgModule({
  imports: [TemplateRoutes]
})
export class TemplateRoutingModule { }
