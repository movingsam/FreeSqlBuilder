import { Component, OnInit, Input } from '@angular/core';
import { TableInfo } from '../modals/tableInfo';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-table-preview',
  templateUrl: './tablePreview.component.html',
  styles: ['']
})
export class TablePreViewComponent implements OnInit {

  @Input() path = '';
  @Input() entityBaseName = '';
  listOfData: TableInfo[];
  mapOfExpandData: { [key: string]: boolean } = {};
  mapOfNavExpandData: { [key: string]: boolean } = {};
  constructor(private client: HttpClient) {

  }

  ngOnInit() {
    this.client
      .get<TableInfo[]>(`http://localhost:5000/api/AllTable?path=${this.path}&entityBaseName=${this.entityBaseName}`)
      .subscribe((data) => {
        this.listOfData = data;
      });
  }


}
