import { Component, Input } from '@angular/core';
import { MenuService, Menu } from '@delon/theme';

@Component({
  selector: 'fb-toolbarmenu',
  template: `
    <li *ngIf="menu.children.length === 0; else submenu" nz-menu-item></li>
    <ng-template #submenu> </ng-template>
  `,
})
export class ToolbarmenuComponent {
  @Input() menu: Menu;
  constructor() {}
}
