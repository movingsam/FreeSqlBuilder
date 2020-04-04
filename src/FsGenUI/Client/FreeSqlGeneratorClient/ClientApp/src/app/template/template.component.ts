import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-template',
  templateUrl: './template.component.html',
  styleUrls: ['./template.component.css']
})
export class TemplateComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }
  newTemplate(): void {
    this.router.navigateByUrl('template/new');
  }
  listTemplate(): void {
    this.router.navigateByUrl('template/list');
  }
}
