import { isDefined } from '@angular/compiler/src/util';
import { Component, Input, OnInit } from '@angular/core';
import { ColumnInfoDto, DbColumnInfoDto, DbTableInfoDto, TableInfoDto } from 'src/app/core/services/dtos/tableinfo';
import { PickType } from 'src/app/core/services/interface/project';

@Component({
  selector: 'fb-tableinfo',
  templateUrl: './tableinfo.component.html',
  styles: [
    ` 
    `
  ]
})
export class TableinfoComponent implements OnInit {

  @Input() PickerType: PickType = PickType.Ignore;
  @Input() Datas: TableInfoDto[] | DbTableInfoDto[];
  esData: TableInfoDto[];
  constructor() {

  }
  isDs(value: TableInfoDto | DbTableInfoDto): value is DbTableInfoDto {
    return (value as DbTableInfoDto).columns !== undefined;
  }
  ngOnInit() {
    console.log(this.Datas instanceof Array);
  }
  public getColumnTitle(column: ColumnInfoDto) {
    return `${column.csName}[${column.comment}]`;
  }
  public getDbColumnTitle(column: DbColumnInfoDto) {
    return `${column.name}[${column.coment}]`;
  }
  getType() {
    return this.PickerType === PickType.Ignore ? `忽略` : `选中`;
  }

}
