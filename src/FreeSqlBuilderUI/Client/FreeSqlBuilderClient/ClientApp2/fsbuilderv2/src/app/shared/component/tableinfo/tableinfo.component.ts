import { isDefined } from '@angular/compiler/src/util';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ColumnInfoDto, DbColumnInfoDto, DbTableInfoDto, TableInfoDto } from 'src/app/core/services/dtos/tableinfo';
import { PickType } from 'src/app/core/services/interface/project';

@Component({
  selector: 'fb-tableinfo',
  templateUrl: './tableinfo.component.html',
  styles: [``],
})
export class TableinfoComponent implements OnInit {
  @Input() Style: object;
  @Input() PickerType: PickType = PickType.Ignore;
  @Input() Datas: TableInfoDto[] | DbTableInfoDto[];
  @Output() TableNamesChange: EventEmitter<string[]> = new EventEmitter();
  tagColor = this.PickerType === PickType.Ignore ? 'error' : 'success';
  @Input() tableNames: string[] = [];
  esData: TableInfoDto[];
  constructor() {}
  isDs(value: TableInfoDto | DbTableInfoDto): value is DbTableInfoDto {
    return (value as DbTableInfoDto).columns !== undefined;
  }
  ngOnInit() {}
  public getColumnTitle(column: ColumnInfoDto) {
    return `${column.csName}[${column.comment}]`;
  }
  public getDbColumnTitle(column: DbColumnInfoDto) {
    return `${column.name}[${column.coment}]`;
  }
  getType() {
    return this.PickerType === PickType.Ignore ? `忽略` : `选中`;
  }

  /**
   * 选择
   * @param value 值
   */
  pick(value) {
    if (this.isDs(this.Datas[0])) {
      const tableName = (this.Datas[value] as DbTableInfoDto).name;
      this.IgnoreOrPick(tableName);
    } else {
      const tableName = (this.Datas[value] as TableInfoDto).csName;
      this.IgnoreOrPick(tableName);
    }
    this.TableNamesChange?.emit(this.tableNames);
  }
  /**
   * 忽略或者选取
   * @param tableName 表名
   */
  IgnoreOrPick(tableName: string) {
    const index = this.tableNames.indexOf(tableName);
    if (index === -1) {
      this.tableNames.push(tableName);
    } else {
      this.tableNames.splice(index, 1);
    }
  }

  checked(index: number): boolean {
    if (this.isDs(this.Datas[0])) {
      const tableName = (this.Datas[index] as DbTableInfoDto).name;
      return this.tableNames.indexOf(tableName) !== -1;
    } else {
      const tableName = (this.Datas[index] as TableInfoDto).csName;
      return this.tableNames.indexOf(tableName) !== -1;
    }
  }
}
