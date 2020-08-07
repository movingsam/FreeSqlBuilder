import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { STComponent } from '@delon/abc/st';
import { SFComponent, SFSchema, SFUISchema } from '@delon/form';
import { NzMessageService } from 'ng-zorro-antd/message';
import { TableInfoDto } from 'src/app/core/services/dtos/tableinfo';
import { GeneratorconfigService } from 'src/app/core/services/generatorconfig.service';
import { HelperService } from 'src/app/core/services/helper.service';
import { DataSource } from 'src/app/core/services/interface/project';

@Component({
  selector: 'fb-datasource',
  templateUrl: './datasource.component.html',
  styles: [],
})
export class DatasourceComponent implements OnInit {
  record: any = {};
  /**
   * 构造
   */
  constructor(private service: GeneratorconfigService, private helper: HelperService, private msgServe: NzMessageService) {

  }
  @ViewChild('sf') sf: SFComponent;
  ds: DataSource = new DataSource();
  tableInfos: TableInfoDto[];
  @Input() isDefault = false;
  /**
   * 数据源
   */
  @Input() dataSource: DataSource = new DataSource();
  @Output() dataSourceChange = new EventEmitter();
  nameUI = {};
  /**
   * JsonSchema
   */
  schema: SFSchema;
  ui: SFUISchema = {
    '*': {
      spanLabelFixed: 100,
      grid: { span: 12 },
    },
  };

  checkConnection() {
    console.log(`checkConnection`);
    const ds = new DataSource();
    console.log(this.sf);
    ds.connectionString = this.sf.getProperty('/connectionString').value;
    ds.dbType = this.sf.getProperty('/dbType').value;
    console.log(ds);
    this.service.checkConnectioon(ds)
      .subscribe(r => {
        if (r) {
          this.msgServe.success(`连接成功`);
          this.helper.getTableInfo(ds).subscribe((tbInfo: TableInfoDto[]) => {
            this.tableInfos = tbInfo;
          });
        }
        else {
          this.msgServe.warning(`连接失败`);
        }
      });
  }
  change(ds): void {
    this.dataSource = ds;
    this.dataSourceChange.emit(this.dataSource);
  }

  ngOnInit() {
    if (this.record.id > 0) {
      this.service.getDataSource(this.record.id).subscribe(r =>
        this.ds = r
      );
    }

    if (this.isDefault) {
      this.nameUI = {
        widget: 'text'
      };
      setTimeout(() => this.sf.setValue('/name', 'DefaultDataSource'), 500);
    }
    this.schema = {
      properties: {
        name: {
          type: 'string',
          title: '名称',
          ui: this.nameUI
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
        connectionString: {
          type: 'string',
          title: '数据库链接',
          ui: {
            widget: 'custom',
            grid: { span: 24 }
          },
          default: ''
        },
      },
    };
    console.log(this.schema, `schema`);
  }
}
