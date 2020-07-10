import { Component } from '@angular/core';
import { Menu, MenuService } from '@delon/theme';
@Component({
  selector: 'fb-toolbar',
  template: `
    <ul nz-menu nzMode="horizontal">
      <fb-toolbarmenu *ngFor="let item of this.menus" [menu]="item"> </fb-toolbarmenu>
    </ul>
  `,
  styles: [
    `
      :host ::ng-deep .ant-menu-horizontal {
        line-height: 30px;
      }
    `,
  ],
})
export class ToolbarComponent {
  public menus: Menu[];
  constructor(public menuService: MenuService) {
    this.menus = menuService.menus;

    // console.log(this.menus);
  }
}
