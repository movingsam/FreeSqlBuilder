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
import { ProjectinfoComponent } from './newproject/projectinfo.component';
import { GeneratorModeComponent } from './newproject/generatormode.component';
import { BuilderContainerComponent } from './newproject/builderOption/BuilderContainer.component';
import { BuildTaskComponent } from './newproject/buildtask.component';
// import { Http } from '../base/webapi';
@NgModule({
  exports: [ProjectComponent, NewProjectComponent, BuilderOptionComponent, ProjectinfoComponent,
    GeneratorModeComponent, BuilderContainerComponent, BuildTaskComponent],
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
  declarations: [ProjectComponent, NewProjectComponent, TablePreViewComponent, ProjectinfoComponent, GeneratorModeComponent,
    BuilderContainerComponent, ProjectListComponent, BuilderOptionComponent, BuildTaskComponent]
})
export class ProjectModule { }
