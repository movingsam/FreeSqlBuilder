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
import { HighlightModule, HIGHLIGHT_OPTIONS, HighlightOptions } from 'ngx-highlightjs';

import { TemplatePreComponent } from './newTemplate/templatePre/templatePre.component';

import { ListTemplateComponent } from './listTemplate/listTemplate.component';
import { PathdirSelectComponent } from './dirPathComponent';

export function getHighlightLanguages() {
  return {
    typescript: () => import('highlight.js/lib/languages/typescript'),
    css: () => import('highlight.js/lib/languages/css'),
    javascript: () => import('highlight.js/lib/languages/javascript'),
    cs: () => import('highlight.js/lib/languages/cs'),
    cshtml: () => import('./cshtml'),
    xml: () => import('highlight.js/lib/languages/xml')
  };
}

@NgModule({
  imports: [
    CommonModule,
    NgZorroAntdModule,
    TemplateRoutingModule,
    FormsModule,
    NzFormModule,
    ReactiveFormsModule,
    HighlightModule
  ],
  providers: [
    {
      provide: HIGHLIGHT_OPTIONS,
      useValue: <HighlightOptions>{
        languages: getHighlightLanguages(),
        lineNumbers: true
      }
    }
  ],
  exports: [PathdirSelectComponent],
  declarations: [TemplateComponent, NewTemplateComponent, TemplatePreComponent, ListTemplateComponent,
    PathdirSelectComponent]
})
export class TemplateModule { }
