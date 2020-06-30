import { Component } from '@angular/core';
import { MenuService, Menu } from '@delon/theme';
@Component({
  selector: 'fb-toolbar',
  template: `
    <ul nz-menu nzMode="horizontal">
      <li *ngFor="let item of list"></li>
      <li nz-menu-item nzSelected>
        项目
      </li>
      <li nz-menu-item nzDisabled>
        <i nz-icon nzType="appstore"></i>
        Navigation Two
      </li>
      <li nz-submenu nzTitle="Navigation Three - Submenu" nzIcon="setting">
        <ul>
          <li nz-menu-group nzTitle="Item 1">
            <ul>
              <li nz-menu-item>Option 1</li>
              <li nz-menu-item>Option 2</li>
            </ul>
          </li>
          <li nz-menu-group nzTitle="Item 2">
            <ul>
              <li nz-menu-item>Option 3</li>
              <li nz-menu-item>Option 4</li>
              <li nz-submenu nzTitle="Sub Menu">
                <ul>
                  <li nz-menu-item nzDisabled>Option 5</li>
                  <li nz-menu-item>Option 6</li>
                </ul>
              </li>
              <li nz-submenu nzDisabled nzTitle="Disabled Sub Menu">
                <ul>
                  <li nz-menu-item>Option 5</li>
                  <li nz-menu-item>Option 6</li>
                </ul>
              </li>
            </ul>
          </li>
        </ul>
      </li>
      <li nz-menu-item>
        <a href="https://ng.ant.design" target="_blank" rel="noopener noreferrer">Navigation Four - Link</a>
      </li>
    </ul>
  `,
})
export class ToolbarComponent {
  public menus: Menu[];
  constructor(public menuService: MenuService) {
    this.menus = menuService.menus;
  }
}
