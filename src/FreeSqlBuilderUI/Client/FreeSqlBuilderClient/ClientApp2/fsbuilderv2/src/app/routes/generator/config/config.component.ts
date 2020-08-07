import { Component, EventEmitter, OnInit, ViewChild } from '@angular/core';
import { STColumn, STComponent } from '@delon/abc/st';
import { SFSchema } from '@delon/form';
import { ModalHelper, _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { GeneratorconfigService } from 'src/app/core/services/generatorconfig.service';
import { DatasourceIndexComponent } from './datasource/datasource.component';
import { GeneratorConfigEditComponent } from './edit/edit.component';
import { EntitysourceIndexComponent } from './entitysource/entitysource.component';

@Component({
  selector: 'fb-generator-config',
  templateUrl: './config.component.html',
})
export class GeneratorConfigComponent implements OnInit {
  url = `api/config`;
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
    { title: '配置名称', index: 'name' },
    { title: '配置类型', type: 'enum', enum: { 0: 'DbFirst', 1: 'CodeFirst' }, index: 'generatorMode' },
    { title: '选中模式', type: 'enum', enum: { 0: '选中', 1: '忽略' }, index: 'pickType' },
    {
      title: '操作',
      buttons: [
        {
          text: '编辑',
          type: 'modal',
          modal: {
            component: GeneratorConfigEditComponent,
            modalOptions: {
              nzWidth: '80vw',
              nzBodyStyle: {
                'overflow-y': 'scroll',
                'max-height': '70vh',
              },
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

  constructor(private config: GeneratorconfigService, private modal: ModalHelper, private msgSer: NzMessageService) { }

  checkDataSource() {
    this.modal.create(DatasourceIndexComponent, {}, {
      modalOptions: {
        nzWidth: '80vw',
        nzBodyStyle: {
          'overflow-y': 'scroll',
          'max-height': '70vh',
        },
      },
    }).subscribe(() => this.st.reload());
  }

  checkEntitySource() {
    this.modal.create(EntitysourceIndexComponent, {}, {
      modalOptions: {
        nzWidth: '80vw',
        nzBodyStyle: {
          'overflow-y': 'scroll',
          'max-height': '70vh',
        },
      },
    }).subscribe(() => this.st.reload());
  }
  ngOnInit() { }

  add() {
    this.modal
      .createStatic(
        GeneratorConfigEditComponent,
        { i: { id: 0 } },
        {
          modalOptions: {
            nzWidth: '80vw',
            nzBodyStyle: {
              'overflow-y': 'scroll',
              'max-height': '70vh',
            },
          },
        },
      )
      .subscribe(() => this.st.reload());
  }
}
