import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { TableInfo, ColumnInfo } from '../modals/tableInfo';
import { HttpClient } from '@angular/common/http';
import { PickType } from '../modals/generatormodeconfig';

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
  @Input() pickTables = '';
  @Input() pickType = `${PickType.Ignore}`;
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
    if (changes["pickType"]) {
      console.log(`pickType changes`)
      this.pickType = changes["pickType"]["currentValue"];
      console.log(`pickType${this.pickType}`)
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
        if (this.ignoreTables !== ``) {
          const ignores = this.ignoreTables.split(',');
          if (ignores && ignores.length > 0 && ignores[0] !== '') {
            ignores.forEach(ignore => {
              const current = data.filter(d => d.csName === ignore);
              if (current && current.length > 0) {
                current[0].isIgnore = true;
              }
            });
          }
        }
        if (this.pickTables !== ``) {
          const isPicks = this.pickTables.split(',');
          if (isPicks && isPicks.length > 0 && isPicks[0] !== '') {
            isPicks.forEach(pick => {
              const current = data.filter(d => d.csName === pick);
              if (current && current.length > 0) {
                current[0].isPick = true;
              }
            });
          }
        }
        this.listOfData = data;
        this.allTable.emit(this.listOfData);
      });
  }

  ignoreTable(row: TableInfo): void {
    console.log(`${this.pickType},${PickType.Ignore}`);
    if (this.pickType === `${PickType.Ignore}`) {
      row.isIgnore = !row.isIgnore;
      this.listOfData.filter(x => x.csName === row.csName)[0].isIgnore = row.isIgnore;
      this.ignoreTables = this.listOfData.filter(x => x.isIgnore).map(x => x.csName).join(',');
      this.callBack.emit(this.ignoreTables);
    }
    else {
      console.log(`changes sourcs:${row.isPick}`);
      row.isPick = !row.isPick;
      console.log(`->${row.isPick}`);
      this.listOfData.filter(x => x.csName === row.csName)[0].isPick = row.isPick;
      this.pickTables = this.listOfData.filter(x => x.isPick).map(x => x.csName).join(',');
      console.log(this.pickTables, 'pickTables');
      this.callBack.emit(this.pickTables);
    }
  }

  getTypeBtnText(row: TableInfo) {
    if (this.pickType === `${PickType.Ignore}`) {
      if (!row.isIgnore) {
        row.isIgnore = false;
      }
      return row.isIgnore ? "选中" : "忽略"
    }
    if (!row.isPick) {
      row.isPick = false;
    }
    return row.isPick ? "忽略" : "选中"
  }

  getPkName(row: TableInfo): string {
    return row.primarys.map(x => `${x.csName}-${x.dbTypeText}`).join(',');
  }
  getObjectKeys(objcet) {
    console.log(objcet);
    return objcet;
  }

}
