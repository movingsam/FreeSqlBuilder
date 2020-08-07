import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { STColumn, STComponent } from '@delon/abc/st';
import { SFSchema } from '@delon/form';
import { ModalHelper } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { ModalButtonOptions, NzModalService } from 'ng-zorro-antd/modal';
import { GeneratorconfigService } from 'src/app/core/services/generatorconfig.service';
import { EntitySource } from 'src/app/core/services/interface/project';
import { EntitysourceComponent } from 'src/app/shared/component/entitysource/entitysource.component';

@Component({
  selector: 'fb-entitysource-index',
  templateUrl: './entitysource.component.html',
  styles: []
})
export class EntitysourceIndexComponent implements OnInit {
  url = `api/config/entitysource`;
  @ViewChild('es', { static: true }) es: TemplateRef<{}>;
  entitySource: EntitySource = new EntitySource();
  constructor(private config: GeneratorconfigService, private modal: ModalHelper, private modalHelpr: NzModalService, private msgSer: NzMessageService) { }
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
    { title: '实体源名称', index: 'name' },
    {
      title: '从哪个程序集反射获取',
      index: 'entityAssemblyName'
    },
    { title: '基类', index: 'entityBaseName' },
    {
      title: '操作',
      buttons: [
        {
          text: '编辑',
          type: 'modal',
          modal: {
            component: EntitysourceComponent,
            modalOptions: {
              nzWidth: '80vw',
              nzBodyStyle: {
                'overflow-y': 'scroll',
                'max-height': '70vh',
              },
              nzFooter: [{
                label: '确定',
                show: (value) => {
                  return value.entitySource.name !== '' && value.entitySource !== undefined;
                },
                onClick: (value) => {
                  console.log(value.entitySource, `update`);
                  this.config.updateEntitySource(value.entitySource).subscribe(r => {
                    this.msgSer.success(`更新成功`);
                    value.modalRef.close();
                    this.st.reload();
                  });
                }
              }]
            },
          },
          click: (val, modal) => {
            console.log(val, modal, 'test');
            if (modal === true) {
              this.st.reload();
            }
          },
        },
        {
          text: '删除',
          type: 'del',
          click: (value: any) => {
            this.config.delEntitySource(value.id).subscribe((r) => {
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
  add(): void {
    this.modalHelpr.create({
      nzTitle: '新增数据源',
      nzContent: this.es,
      nzWidth: '80vw',
      nzOnOk: () => {
        this.config.createEntitySource(this.entitySource).subscribe((r) => {
          if (r) {
            this.msgSer.success(`新增成功!`);
            this.st.reload();
          }
        });
      }
    });
  }
  entitySourceChange(value: EntitySource): void {
    console.log(value, `change`);
    this.entitySource = value;
  }
}
