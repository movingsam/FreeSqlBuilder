import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { STColumn, STComponent } from '@delon/abc/st';
import { SFSchema } from '@delon/form';
import { ModalHelper } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { GeneratorconfigService } from 'src/app/core/services/generatorconfig.service';
import { DatasourceComponent } from '../../../../shared/component/datasource/datasource.component';

@Component({
  selector: 'fb-datasource-index',
  templateUrl: './datasource.component.html',
  styles: []
})
export class DatasourceIndexComponent implements OnInit {

  constructor(private config: GeneratorconfigService, private modal: ModalHelper, private modalHelpr: NzModalService, private msgSer: NzMessageService) { }
  url = `api/config/DataSource`;
  searchSchema: SFSchema = {
    properties: {
      keyword: {
        type: 'string',
        title: '关键字',
      },
    },
  };
  @ViewChild('st', { static: false }) st: STComponent;
  columns: STColumn[] = [
    { title: '编号', index: 'id' },
    { title: '数据源名称', index: 'name' },
    {
      title: '数据库类型',
      type: 'enum',
      enum:
      {
        0: 'MySql',
        1: 'SqlServer',
        2: 'PostgreSQL',
        3: 'Oracle',
        4: 'Sqlite',
        5: 'OdbcOracle',
        6: 'OdbcSqlServer',
        7: 'OdbcMySql',
        8: 'OdbcPostgreSQL',
        9: 'Odbc',
        10: 'OdbcDameng',
        11: 'MsAccess',
        12: 'Dameng',
        13: 'OdbcKingbaseES',
        14: 'ShenTong'
      }
      ,
      index: 'dbType'
    },
    { title: '数据库连接', index: 'connectionString' },
    {
      title: '操作',
      buttons: [
        {
          text: '编辑',
          type: 'modal',
          modal: {
            component: DatasourceComponent,
            modalOptions: {
              nzWidth: '80vw',
              nzBodyStyle: {
                'overflow-y': 'scroll',
                'max-height': '70vh',
              },
              nzFooter: [{
                label: '确定',
                onClick: (value) => {
                  console.log(value.sf.value, `update`);
                  this.config.updateDataSource(value.sf.value).subscribe(r => {
                    this.msgSer.success(`更新成功`);
                    this.st.reload();
                    this.modalHelpr.closeAll();
                  });
                }
              }]

            },

          },
          click: (val, modal) => {
            if (modal === true) {
              this.st.reload();
            }
          },
        },
        {
          text: '删除',
          type: 'del',
          click: (value: any) => {
            this.config.delConfig(value.id).subscribe((r) => {
              this.msgSer.success(`删除成功`);
              this.st.reload();
            });
          },
        },
      ],
    },
  ];

  ngOnInit() {
  }
  add() {

  }

}
