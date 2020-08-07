import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { STWidgetRegistry } from '@delon/abc/st';
// import { MultipFunctionWidget } from '../component/multipFunctionOptions/MultipFunctionWidget';
import { SharedModule } from '../shared.module';

export const STWIDGET_COMPONENTS = [];

@NgModule({
  declarations: STWIDGET_COMPONENTS,
  entryComponents: STWIDGET_COMPONENTS,
  imports: [SharedModule],
  exports: [...STWIDGET_COMPONENTS],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class STWidgetModule {


  constructor(widgetRegistry: STWidgetRegistry) {
    // widgetRegistry.register(MultipFunctionWidget.KEY, MultipFunctionWidget);
    // console.log(MultipFunctionWidget.KEY);
  }
}
