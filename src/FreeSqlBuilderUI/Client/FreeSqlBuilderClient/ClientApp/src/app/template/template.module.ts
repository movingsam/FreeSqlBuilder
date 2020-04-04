import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TemplateComponent } from './template.component';
import { TemplateRoutingModule } from './template.routing';
import { NewTemplateComponent } from './newTemplate/newTemplate.component';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { FormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { ReactiveFormsModule } from '@angular/forms';
// import { hljs } from 'highlight.js';

import { TemplatePreComponent } from './newTemplate/templatePre/templatePre.component';

import { ListTemplateComponent } from './listTemplate/listTemplate.component';
import { PathdirSelectComponent } from './dirPathComponent';
import { HighlightDirective } from './newTemplate/templatePre/highlight.directive';

@NgModule({
  imports: [
    CommonModule,
    NgZorroAntdModule,
    TemplateRoutingModule,
    FormsModule,
    NzFormModule,
    ReactiveFormsModule
  ],
  providers: [
  ],
  exports: [PathdirSelectComponent, ListTemplateComponent, TemplatePreComponent],
  declarations: [TemplateComponent, NewTemplateComponent, TemplatePreComponent, ListTemplateComponent,
    PathdirSelectComponent, HighlightDirective]
})
export class TemplateModule { }
