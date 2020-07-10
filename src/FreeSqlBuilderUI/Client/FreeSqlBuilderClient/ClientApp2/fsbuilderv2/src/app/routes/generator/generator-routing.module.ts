import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GeneratorBuilderComponent } from './builder/builder.component';
import { GeneratorConfigComponent } from './config/config.component';
import { GeneratorProjectComponent } from './project/project.component';
import { GeneratorTemplateComponent } from './template/template.component';

const routes: Routes = [
  { path: 'index', component: GeneratorProjectComponent },
  { path: 'config', component: GeneratorConfigComponent },
  { path: 'builder', component: GeneratorBuilderComponent },
  { path: 'template', component: GeneratorTemplateComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GeneratorRoutingModule {}
