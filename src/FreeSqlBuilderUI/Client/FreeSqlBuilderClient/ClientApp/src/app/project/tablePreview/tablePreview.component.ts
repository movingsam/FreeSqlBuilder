import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TableInfo, ColumnInfo } from '../modals/tableInfo';
import { HttpClient } from '@angular/common/http';
import { stringify } from 'querystring';

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
  @Output() callBack: EventEmitter<string> = new EventEmitter();
  @Input() ignoreTables = '';
  listOfData: TableInfo[];
  mapOfExpandData: { [key: string]: boolean } = {};
  mapOfNavExpandData: { [key: string]: boolean } = {};
  constructor(private client: HttpClient) {

  }

  ngOnInit() {
    this.client
      .get<TableInfo[]>(`/api/AllTable/${this.entityAssemblyName}/${this.entityBaseName}`)
      .subscribe((data) => {
        const ignores = this.ignoreTables.split(',');
        if (ignores.length > 0 && ignores[0] !== '') {
          console.log(ignores.length, 'ignores');
          ignores.forEach(ignore => {
            data.filter(d => d.name === ignore)[0].isIgnore = true;
          });
        }
        this.listOfData = data;
        this.allTable.emit(this.listOfData);
      });
  }
  ignoreTable(row: TableInfo): void {
    row.isIgnore = !row.isIgnore;
    this.listOfData.filter(x => x.name === row.name)[0].isIgnore = row.isIgnore;
    this.ignoreTables = this.listOfData.filter(x => x.isIgnore).map(x => x.name).join(',');
    this.callBack.emit(this.ignoreTables);
  }
}
