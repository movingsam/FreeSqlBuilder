import { CommonModule } from '@angular/common';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { DelonACLModule } from '@delon/acl';
import { AlainThemeModule } from '@delon/theme';

import { TranslateModule } from '@ngx-translate/core';

import { DatasourceComponent } from './component/datasource/datasource.component';
import { DefaultinitComponent } from './component/defaultinit/defaultinit.component';
import { EntitysourceComponent } from './component/entitysource/entitysource.component';
import { TableinfoComponent } from './component/tableinfo/tableinfo.component';
import { SHARED_DELON_MODULES } from './shared-delon.module';
import { SHARED_ZORRO_MODULES } from './shared-zorro.module';
// #region third libs

const THIRDMODULES = [];

// #endregion

// #region your componets & directives

const COMPONENTS = [DatasourceComponent, EntitysourceComponent, TableinfoComponent, DefaultinitComponent];
const DIRECTIVES = [];

// #endregion

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    AlainThemeModule.forChild(),
    DelonACLModule,
    ...SHARED_DELON_MODULES,
    ...SHARED_ZORRO_MODULES,
    // third libs
    ...THIRDMODULES,
  ],
  declarations: [
    // your components
    ...COMPONENTS,
    ...DIRECTIVES,
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    AlainThemeModule,
    DelonACLModule,
    TranslateModule,
    ...SHARED_DELON_MODULES,
    ...SHARED_ZORRO_MODULES,
    // third libs
    ...THIRDMODULES,
    // your components
    ...COMPONENTS,
    ...DIRECTIVES,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class SharedModule {}
