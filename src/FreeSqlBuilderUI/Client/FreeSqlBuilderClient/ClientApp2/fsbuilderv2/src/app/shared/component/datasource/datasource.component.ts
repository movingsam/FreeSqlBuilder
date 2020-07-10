import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { STComponent } from '@delon/abc/st';
import { SFSchema } from '@delon/form';
import { GeneratorconfigService } from 'src/app/core/services/generatorconfig.service';
import { DataSource } from 'src/app/core/services/interface/project';

@Component({
  selector: 'fb-datasource',
  templateUrl: './datasource.component.html',
  styles: [],
})
export class DatasourceComponent implements OnInit {
  @ViewChild('sf', { static: false }) sf: STComponent;

  ds: DataSource = new DataSource();
  /**
   * 数据源
   */
  @Input() dataSource: DataSource = new DataSource();
  @Output() dataSourceChange = new EventEmitter();
  /**
   * JsonSchema
   */
  schema: SFSchema = {
    properties: {
      connectionString: {
        type: 'string',
        title: '数据库链接',
      },
      dbType: {
        type: 'number',
        title: '数据库类型',
        enum: [
          { label: 'MySql', value: 0 },
          { label: 'SqlServer', value: 1 },
          { label: 'PostgreSQL', value: 2 },
          { label: 'Oracle', value: 3 },
          { label: 'Sqlite', value: 4 },
          { label: 'OdbcOracle', value: 5 },
          { label: 'OdbcSqlServer', value: 6 },
          { label: 'OdbcMySql', value: 7 },
          { label: 'OdbcPostgreSQL', value: 8 },
          { label: 'Odbc', value: 9 },
          { label: 'OdbcDameng', value: 10 },
          { label: 'MsAccess', value: 11 },
          { label: 'Dameng', value: 12 },
          { label: 'OdbcKingbaseES', value: 13 },
          { label: 'ShenTong', value: 14 },
        ],
      },
      name: {
        type: 'string',
        title: '名称',
      },
    },
  };

  /**
   * 构造
   */
  constructor() {}

  change(ds): void {
    this.dataSource = ds;
    this.dataSourceChange.emit(this.dataSource);
  }

  ngOnInit() {}
}
