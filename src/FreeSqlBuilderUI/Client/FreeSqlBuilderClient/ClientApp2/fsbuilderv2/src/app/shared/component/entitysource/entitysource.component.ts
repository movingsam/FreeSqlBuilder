import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { SFComponent, SFSchema } from '@delon/form';
import { NzCascaderOption } from 'ng-zorro-antd/cascader';
import { NzModalService } from 'ng-zorro-antd/modal';
import { title } from 'process';
import { Observable, of } from 'rxjs';
import { TableInfoDto } from 'src/app/core/services/dtos/tableinfo';
import { GeneratorconfigService } from 'src/app/core/services/generatorconfig.service';
import { HelperService } from 'src/app/core/services/helper.service';
import { EntitySource } from 'src/app/core/services/interface/project';
import { isArray } from 'util';

@Component({
  selector: 'fb-entitysource',
  templateUrl: './entitysource.component.html',
  styles: [``],
})
export class EntitysourceComponent implements OnInit {
  constructor(private service: HelperService, private modal: NzModalService, private configService: GeneratorconfigService) {}
  @ViewChild('sf') sf: SFComponent;
  @Input() entitySource: EntitySource;
  @Input() isDefault: boolean;
  @Output() entitySourceChange = new EventEmitter();
  public tableInfos: TableInfoDto[];
  assembly = '';
  es: EntitySource;
  schema: SFSchema;
  nameUi: {};
  change(value): void {
    this.entitySource = this.checkEntitySourceData();
    this.entitySourceChange.emit(this.entitySource);
  }

  checkEntitySourceData(): EntitySource {
    const entitySource = new EntitySource();
    const es: string[] = this.sf.getProperty('/entityBaseName').value;
    const assemblies: string[] = this.sf.getProperty('/entityAssemblies').value;
    if (assemblies && assemblies.length > 0) {
      entitySource.entityAssemblyName = assemblies?.join(';');
    }
    if (es && es.length > 0) {
      entitySource.entityBaseName = es?.join(';');
    }
    console.log(entitySource, `entitySource`);
    return entitySource;
  }
  preview(value, component: TemplateRef<{}>): void {
    const entitySource = this.checkEntitySourceData();
    this.service.getTableInfo(entitySource).subscribe((r: TableInfoDto[]) => {
      this.tableInfos = r;
      this.showPreview(component);
    });
  }

  showPreview(component: TemplateRef<{}>): void {
    this.modal.create({
      nzTitle: '数据表预览',
      nzContent: component,
      nzWidth: '80vw',
      nzMaskClosable: false,
      nzClosable: false,
      nzBodyStyle: {
        'overflow-y': 'scroll',
        'max-height': '70vh',
      },
      nzOnOk: () => {},
    });
  }

  ngOnInit() {
    this.service.getAbstractEntity().subscribe((r) => {
      const entityBaseName = this.sf.getProperty('/entityBaseName');
      entityBaseName.schema.enum = r;
      console.log(entityBaseName.schema.enum, `enum`);
      this.sf.setValue('/entityBaseName', entityBaseName);
    });
    if (this.isDefault) {
      this.nameUi = {
        widget: 'text',
      };
      setTimeout(() => {
        this.sf.setValue('/name', 'DefaultEntitySource');
      }, 500);
    }
    this.schema = {
      properties: {
        name: {
          type: 'string',
          title: '名称',
          ui: this.nameUi,
        },
        entityAssemblies: {
          type: 'string',
          title: '实体所在程序集',
          ui: {
            widget: 'select',
            mode: 'multiple',
            asyncData: () => {
              return this.service.getAssemblies();
            },
            change: () => {
              this.checkEntitySourceData();
            },
          },
          default: null,
        },
        entityBaseName: {
          type: 'string',
          title: '实体基类',
          // description: '选择一个实体基类/接口,不选择则是所有符合CodeFirst的实体',
          enum: [],
          ui: {
            widget: 'cascader',
            placeholder: '选择一个实体基类/接口,不选择则是所有符合CodeFirst的实体',
            allowClear: true,
            change: () => {
              this.checkEntitySourceData();
            },
          },
          default: null,
        },
        // entityBaseName: {
        //   type: 'string',
        //   title: '实体基类',
        //   ui: {
        //     widget: 'select',
        //   },
        // },
        preview: {
          type: 'string',
          title: '预览',
          ui: {
            widget: 'custom',
          },
        },
      },
    };
  }
  Close() {}
}
