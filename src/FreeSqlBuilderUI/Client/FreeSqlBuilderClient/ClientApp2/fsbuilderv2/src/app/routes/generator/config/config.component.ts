import { Component, OnInit, ViewChild } from '@angular/core';
import { STColumn, STComponent } from '@delon/abc/st';
import { SFSchema } from '@delon/form';
import { ModalHelper, _HttpClient } from '@delon/theme';
import { GeneratorconfigService } from 'src/app/core/services/generatorconfig.service';
import { GeneratorConfigEditComponent } from './edit/edit.component';

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
    { title: '配置类型', type: 'enum', enum: { 0: 'CodeFirst', 1: 'DbFirst' }, index: 'generatorMode' },
    { title: '选中模式', type: 'enum', enum: { 0: '选中', 1: '忽略' }, index: 'pickType' },
    {
      title: '操作',
      buttons: [
        { text: '查看', click: (item: any) => `/form/${item.id}` },
        {
          text: '编辑',
          type: 'modal',
          modal: {
            component: GeneratorConfigEditComponent,
          },
        },
      ],
    },
  ];

  constructor(private config: GeneratorconfigService, private modal: ModalHelper) {}

  ngOnInit() {}

  add() {
    // this.modal
    //   .createStatic(FormEditComponent, { i: { id: 0 } })
    //   .subscribe(() => this.st.reload());
  }
}