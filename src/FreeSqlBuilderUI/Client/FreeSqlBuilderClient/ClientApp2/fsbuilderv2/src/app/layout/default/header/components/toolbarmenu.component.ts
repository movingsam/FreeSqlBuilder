import { Component, Input } from '@angular/core';
import { Menu } from '@delon/theme';

@Component({
  selector: 'fb-toolbarmenu',
  template: `
    <div *ngIf="this.menu">
      <li *ngIf="this.menu.children.length === 0; else submenu" nz-menu-item>{{ menu.text }}</li>
      <ng-template #submenu>
        <li nz-submenu [nzTitle]="menu.text" [nzIcon]="menu.icon">
          <ul>
            <fb-toolbarmenu *ngFor="let item of menu.children" [menu]="item"></fb-toolbarmenu>
          </ul>
        </li>
      </ng-template>
    </div>
  `,
  styles: [``],
})
export class ToolbarmenuComponent {
  @Input() menu: Menu;
  constructor() {}
}
