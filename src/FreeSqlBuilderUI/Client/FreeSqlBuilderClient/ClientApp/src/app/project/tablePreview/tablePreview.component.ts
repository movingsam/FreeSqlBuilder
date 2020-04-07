import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
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
export class TablePreViewComponent implements OnInit, OnChanges {

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
  ngOnChanges(changes: import('@angular/core').SimpleChanges): void {
    if (changes['entityAssemblyName']) {
      this.entityAssemblyName = changes['entityAssemblyName']['currentValue'];
    }
    if (changes['entityBaseName']) {
      this.entityBaseName = changes['entityBaseName']['currentValue'];
    }
    this.getTableInfos();
  }

  ngOnInit() {
    this.getTableInfos();
  }
  getTableInfos() {
    this.client
      .get<TableInfo[]>(`/api/AllTable/${this.entityAssemblyName}?entityBaseName=${this.entityBaseName}`)
      .subscribe((data) => {
        const ignores = this.ignoreTables.split(',');
        if (ignores.length > 0 && ignores[0] !== '') {
          console.log(ignores.length, 'ignores');
          ignores.forEach(ignore => {
            data.filter(d => d.csName === ignore)[0].isIgnore = true;
          });
        }
        this.listOfData = data;
        this.allTable.emit(this.listOfData);
      });
  }

  ignoreTable(row: TableInfo): void {
    row.isIgnore = !row.isIgnore;
    this.listOfData.filter(x => x.csName === row.csName)[0].isIgnore = row.isIgnore;
    this.ignoreTables = this.listOfData.filter(x => x.isIgnore).map(x => x.csName).join(',');
    this.callBack.emit(this.ignoreTables);
  }
  getPkName(row: TableInfo): string {
    return row.primarys.map(x => `${x.csName}-${x.dbTypeText}`).join(',');
  }
  getObjectKeys(objcet) {
    console.log(objcet);
    return objcet;
  }

}
