import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TableInfo, ColumnInfo } from '../modals/tableInfo';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-table-preview',
  templateUrl: './tablePreview.component.html',
  styles: [`
  .lblCol{width:20vw; display:inline-block;}
  `]
})
export class TablePreViewComponent implements OnInit {

  @Input() entityBaseName = '';
  @Input() entityAssemblyName = '';
  @Output() allTable: EventEmitter<TableInfo[]> = new EventEmitter();
  listOfData: TableInfo[];
  mapOfExpandData: { [key: string]: boolean } = {};
  mapOfNavExpandData: { [key: string]: boolean } = {};
  constructor(private client: HttpClient) {

  }

  ngOnInit() {
    this.client
      .get<TableInfo[]>(`/api/AllTable/${this.entityAssemblyName}/${this.entityBaseName}`)
      .subscribe((data) => {
        this.listOfData = data;
        this.allTable.emit(this.listOfData);
      });
  }

}
