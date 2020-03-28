import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  index = 0;
  disable = false;
  onIndexChange(index: number): void {
    this.index = index;
  }
}
