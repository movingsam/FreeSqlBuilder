import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { SFComponent, SFObjectWidgetSchema, SFSchema, SFSelectWidgetSchema, SFUISchema } from '@delon/form';
import { ModalHelper, _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { of } from 'rxjs';
import { GeneratorconfigService } from 'src/app/core/services/generatorconfig.service';
import { HelperService } from 'src/app/core/services/helper.service';
import { Page } from 'src/app/core/services/interface/dto';
import { DataSource, EntitySource, GeneratorModeConfig } from 'src/app/core/services/interface/project';
import { DatasourceComponent } from 'src/app/shared/component/datasource/datasource.component';

@Component({
  selector: 'fb-generator-config-edit',
  templateUrl: './edit.component.html',
  styles: [
    `
      :host ::ng-deep .sf__fixed {
        flex-flow: wrap;
      }
    `,
  ],
})
export class GeneratorConfigEditComponent implements OnInit {
  @ViewChild('moreDs', { static: true }) private moreDs: TemplateRef<void>;
  @ViewChild('sf') private sf: SFComponent;
  dataSource: DataSource = new DataSource();
  record: any = {};
  i: GeneratorModeConfig;
  schema: SFSchema;
  ui: SFUISchema = {
    '*': {
      spanLabelFixed: 100,
      grid: { span: 12 },
    },
    $no: {
      widget: 'text',
    },
    $href: {
      widget: 'string',
    },
    $description: {
      widget: 'textarea',
      grid: { span: 24 },
    },
  };

  constructor(
    private modal: NzModalRef,
    private msgSrv: NzMessageService,
    public service: GeneratorconfigService,
    private modalHelper: NzModalService,
    public hepler: HelperService,
  ) {}

  DataSource(): SFSchema {
    return;
  }

  EntitySource(): SFSchema {
    return;
  }

  ngOnInit(): void {
    if (this.record.id > 0) {
      this.service.getGeneratorConfig(this.record.id).subscribe((t) => {
        this.i = t;
        console.log(this.i);
        if (!this.i.entitySource) {
          this.i.entitySource = new EntitySource();
        }
        if (!this.i.dataSource) {
          this.i.dataSource = new DataSource();
        }
        this.schemaInit();
      });
    }
  }

  schemaInit(): void {
    this.schema = {
      properties: {
        id: { type: 'number', title: '编号' },
        generatorMode: {
          type: 'number',
          title: '生成器模式',
          ui: {
            widget: 'radio',
          },
          enum: [
            {
              label: 'DbFirst',
              value: 0,
            },
            {
              label: 'CodeFirst',
              value: 1,
            },
          ],
        },
        dataSource: {
          type: 'object',
          title: '数据源',
          ui: {
            type: 'card',
            grid: { span: 24 },
            cardExtra: this.moreDs,
          } as SFObjectWidgetSchema,
          properties: {
            id: {
              type: 'number',
              ui: {
                widget: 'select',
                asyncData: () => {
                  return this.service.getDataSourceSelect();
                },
              },
              enum: [],
              title: '选择',
            },
          },
        },
        entitySource: {
          type: 'object',
          title: '实体源',
          ui: {
            type: 'card',
            grid: { span: 24 },
            cardExtra: this.moreDs,
          } as SFObjectWidgetSchema,
          properties: {
            id: {
              type: 'number',
              title: '选择',
              ui: {
                widget: 'select',
                asyncData: () => {
                  return this.service.getEntitySourceSelect();
                },
              },
            },
          },
        },
      },
      if: {
        properties: { generatorMode: { enum: [0] } },
      },
      then: {
        required: ['dataSource'],
      },
      else: {
        required: ['entitySource'],
      },
    };
  }
  addDataSource(component: TemplateRef<{}>): void {
    this.modalHelper.create({
      nzTitle: '新增数据源',
      nzContent: component,
      nzMaskClosable: false,
      nzClosable: false,
      nzOnOk: () => {
        this.service.createDataSource(this.dataSource).subscribe((r) => {
          if (r) {
            this.msgSrv.success(`新增成功!`);
            const dslist = this.sf.getProperty('/dataSource/id');
            this.service.getDataSourceSelect().subscribe((t) => {
              dslist.schema.enum = t;
              this.sf.setValue('/dataSource/id', r.id);
            });
          }
        });
      },
    });
  }
  dataSourceChange(value): void {
    this.dataSource = value;
  }
  save(value: any) {
    this.service.updateGeneratorConfig(value);
  }

  close() {
    this.modal.destroy();
  }
}
