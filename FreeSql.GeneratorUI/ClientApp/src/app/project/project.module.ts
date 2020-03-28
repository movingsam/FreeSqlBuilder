import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectComponent } from './project.component';
import { NewProjectComponent } from './newproject/newproject.component';
import { BuilderOptionComponent } from './newproject/builderOption/builderOption.component';
import { ProjectRoutingModule } from './project.routing.module';
import { ProjectListComponent } from './projectList/projectList.component';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { FormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { TablePreViewComponent } from './tablePreview/tablePreview.component';
import { TemplateModule } from '../template/template.module';

// import { Http } from '../base/webapi';
@NgModule({
  exports: [ProjectComponent, NewProjectComponent, BuilderOptionComponent],
  imports: [
    CommonModule,
    ProjectRoutingModule,
    NgZorroAntdModule,
    NzFormModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    TemplateModule
  ],
  declarations: [ProjectComponent, NewProjectComponent, TablePreViewComponent,
    ProjectListComponent, BuilderOptionComponent]
})
export class ProjectModule { }
