import { NgModule } from '@angular/core';
import { SharedModule } from '@shared';
import { GeneratorBuilderComponent } from './builder/builder.component';
import { GeneratorBuilderEditComponent } from './builder/edit/edit.component';
import { GeneratorBuilderViewComponent } from './builder/view/view.component';
import { GeneratorConfigComponent } from './config/config.component';
import { GeneratorConfigEditComponent } from './config/edit/edit.component';
import { GeneratorConfigViewComponent } from './config/view/view.component';
import { GeneratorRoutingModule } from './generator-routing.module';
import { GeneratorProjectEditComponent } from './project/edit/edit.component';
import { GeneratorProjectComponent } from './project/project.component';
import { GeneratorProjectViewComponent } from './project/view/view.component';
import { GeneratorTemplateEditComponent } from './template/edit/edit.component';
import { GeneratorTemplateComponent } from './template/template.component';
import { GeneratorTemplateViewComponent } from './template/view/view.component';

const COMPONENTS = [
  GeneratorProjectComponent,
  GeneratorProjectEditComponent,
  GeneratorProjectViewComponent,
  GeneratorConfigComponent,
  GeneratorBuilderComponent,
  GeneratorTemplateComponent,
];
const COMPONENTS_NOROUNT = [
  GeneratorConfigEditComponent,
  GeneratorConfigViewComponent,
  GeneratorBuilderEditComponent,
  GeneratorBuilderViewComponent,
  GeneratorTemplateEditComponent,
  GeneratorTemplateViewComponent,
];

@NgModule({
  imports: [SharedModule, GeneratorRoutingModule],
  declarations: [...COMPONENTS, ...COMPONENTS_NOROUNT],
})
export class GeneratorModule {}
