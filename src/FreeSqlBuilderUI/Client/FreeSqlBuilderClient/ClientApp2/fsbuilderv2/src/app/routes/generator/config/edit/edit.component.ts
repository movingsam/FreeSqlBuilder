import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { SFComponent, SFObjectWidgetSchema, SFSchema, SFSelectWidgetSchema, SFUISchema } from '@delon/form';
import { _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { DbTableInfoDto, TableInfoDto } from 'src/app/core/services/dtos/tableinfo';
import { GeneratorconfigService } from 'src/app/core/services/generatorconfig.service';
import { HelperService } from 'src/app/core/services/helper.service';
import { DataSource, EntitySource, GeneratorModeConfig, PickType } from 'src/app/core/services/interface/project';

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
  constructor(
    private modal: NzModalRef,
    private msgSrv: NzMessageService,
    public service: GeneratorconfigService,
    private modalHelper: NzModalService,
    public hepler: HelperService,
  ) {}
  @ViewChild('moreDs', { static: true }) private moreDs: TemplateRef<void>;
  @ViewChild('moreEs', { static: true }) private moreEs: TemplateRef<void>;
  @ViewChild('sf') private sf: SFComponent;

  tableDto: TableInfoDto[] | DbTableInfoDto[];
  mode = 'default';
  title = '新增配置';
  dataSource: DataSource = new DataSource();
  entitySource: EntitySource = new EntitySource();
  tableInfos: TableInfoDto[];
  dbTableInfos: DbTableInfoDto[];
  record: any = {};
  i: GeneratorModeConfig = new GeneratorModeConfig();
  pickType: PickType = PickType.Ignore;
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
  tableNames: string[] = [];

  /**
   * 界面初始化钩子
   */
  ngOnInit(): void {
    if (this.record.id > 0) {
      this.mode = 'edit';
      console.log(`${this.record.id}`);
      this.service.getGeneratorConfig(this.record.id).subscribe((t) => {
        this.i = t;
        this.title = `修改${t.name}的配置`;
      });
      setTimeout(() => {
        const isDs = this.sf.getProperty('/generatorMode').value === 0;
        const id = isDs ? this.sf.getProperty('/dataSourceId').value : this.sf.getProperty('/entitySourceId').value;
        this.previewTable(id, isDs);
      }, 1000);
    }
    this.schemaInit();
  }
  /**
   * schema初始化
   */
  schemaInit(): void {
    this.schema = {
      properties: {
        name: {
          type: 'string',
          title: '名称',
          ui: {
            change: (val) => {
              this.title = `修改${val}的配置`;
            },
          },
        },
        generatorMode: {
          type: 'number',
          title: '生成器模式',
          ui: {
            widget: 'radio',
            change: (val) => {
              const isDs = val === 0;
              const id = isDs ? this.sf.getProperty('/dataSourceId').value : this.sf.getProperty('/entitySourceId').value;
              this.previewTable(id, isDs);
            },
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
        dataSourceId: {
          type: 'number',
          title: '数据源',
          ui: {
            widget: 'select',
            dropdownRender: this.moreDs,
            asyncData: () => {
              return this.service.getDataSourceSelect();
            },
            change: (val) => {
              this.dataSourceIdChange(val);
            },
          },
        },
        entitySourceId: {
          type: 'number',
          title: '实体源',
          ui: {
            widget: 'select',
            dropdownRender: this.moreEs,
            asyncData: () => {
              return this.service.getEntitySourceSelect();
            },
            change: (val) => {
              this.entitySourceIdChange(val);
            },
          },
        },
        pickType: {
          type: 'number',
          title: '选择类型',
          ui: {
            widget: 'radio',
            styleType: 'button',
            buttonStyle: 'solid',
            change: (val) => {
              this.pickType = val;
            },
          },
          default: 0,
          enum: [
            { label: '选中', value: 0 },
            { label: '忽略', value: 1 },
          ],
        },
        preview: {
          type: 'number',
          title: '预览',
          ui: {
            widget: 'custom',
            grid: { span: 24 },
          },
          default: 0,
        },
        ignoreTables: {
          type: 'string',
          title: '忽略的数据表',
          ui: {
            widget: 'custom',
            grid: { span: 24 },
            visibleIf: {
              pickType: (value: any) => value === 1,
            },
            change: (val) => {
              console.log(val);
            },
          },
          default: '',
        },
        includeTables: {
          type: 'string',
          title: '包含的数据表',
          ui: {
            widget: 'custom',
            grid: { span: 24 },
            visibleIf: {
              pickType: (value: any) => value === 0,
            },
          },
          default: '',
        },
      },
      if: {
        properties: { generatorMode: { enum: [0] } },
      },
      then: {
        required: ['dataSourceId'],
      },
      else: {
        required: ['entitySourceId'],
      },

      required: ['name', 'generatorMode'],
    };

    console.log(this.schema, `Init`);
  }
  /**
   *  新增数据源组件
   * @param component 组件
   */
  addDataSource(component: TemplateRef<{}>): void {
    this.modalHelper.create({
      nzTitle: '新增数据源',
      nzContent: component,
      nzWidth: '80vw',
      nzMaskClosable: false,
      nzClosable: false,
      nzOnOk: () => {
        this.service.createDataSource(this.dataSource).subscribe((r) => {
          if (r) {
            this.msgSrv.success(`新增成功!`);
            const dslist = this.sf.getProperty('/dataSourceId');
            this.service.getDataSourceSelect().subscribe((t) => {
              dslist.schema.enum = t;
              this.sf.setValue('/dataSourceId', r.id);
            });
          }
        });
      },
    });
  }
  /**
   * 新增实体源组件
   * @param component 组件
   */
  addEntitySourrce(component: TemplateRef<{}>): void {
    this.modalHelper.create({
      nzTitle: '新增实体源',
      nzContent: component,
      nzMaskClosable: false,
      nzClosable: false,
      nzOnOk: () => {
        console.log(this.entitySource);
        this.service.createEntitySource(this.entitySource).subscribe((r) => {
          if (r) {
            this.msgSrv.success(`新增成功!`);
            const esid = this.sf.getProperty('/entitySourceId');
            this.service.getEntitySourceSelect().subscribe((t) => {
              esid.schema.enum = t;
              this.sf.setValue('/entitySourceId', r.id);
            });
          }
        });
      },
    });
  }
  /**
   * 数据变更回调
   * @param value 变更数据
   */
  dataSourceChange(value): void {
    this.dataSource = value;
  }
  /**
   * 实体源数据变更回调
   * @param value 变更数据
   */
  entitySourceChange(value): void {
    this.entitySource = value;
  }
  /**
   * 数据源id变更
   * @param dsId 数据源id
   */
  dataSourceIdChange(dsId: number): void {
    this.previewTable(dsId, true);
  }

  /**
   * 实体源id变更
   * @param esId  实体源id
   */
  entitySourceIdChange(esId: number): void {
    this.previewTable(esId, false);
  }

  /**
   * 保存
   * @param value 数据
   */
  save(value: any) {
    if (this.record.id > 0) {
      this.service.updateGeneratorConfig(value).subscribe((r) => {
        this.msgSrv.success(`保存成功`);
        this.modal.close(true);
      });
    } else {
      console.log(value);
      this.service.createGeneratorConfig(value).subscribe((r) => {
        this.msgSrv.success(`新增成功`);
        this.modal.close(true);
      });
    }
  }

  /**
   * 预览接口
   * @param id id
   * @param isds 是否是数据源
   */
  previewTable(id: number, isds: boolean): void {
    if (id > 0) {
      if (isds) {
        this.service.getDataSource(id).subscribe((r) => {
          this.hepler.getTableInfo(r).subscribe((result) => {
            this.tableDto = result;
          });
        });
      } else {
        this.service.getEntitySource(id).subscribe((r) => {
          this.hepler.getTableInfo(r).subscribe((result) => {
            this.tableDto = result;
          });
        });
      }
    }
  }

  console(val) {
    console.log(val);
  }
  tableNameChange(value): void {
    console.log(value, `tableNameChange`);
    if (this.pickType === PickType.Ignore) {
      console.log(value, `ignoreTables`);
      this.sf.setValue('/ignoreTables', value.join(','));
    } else {
      console.log(value, `includeTables`);
      this.sf.setValue('/includeTables', value.join(','));
    }
    this.tableNames = value;
  }

  /**
   * 关闭视窗
   */
  close() {
    this.modal.destroy();
  }
}
