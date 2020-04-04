import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { ListTemplateComponent } from './listTemplate/listTemplate.component';

const routes: Routes = [
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
