import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { SFComponent, SFSchema } from '@delon/form';
import { NzCascaderOption } from 'ng-zorro-antd/cascader';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { basename } from 'path';
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
export class EntitysourceComponent implements OnInit, AfterViewInit {
  constructor(private service: HelperService, public modal: NzModalService, private configService: GeneratorconfigService, public modalRef: NzModalRef) { }

  @ViewChild('sf') sf: SFComponent;
  @Input() entitySource: EntitySource = new EntitySource();
  @Input() isDefault = false;
  @Output() entitySourceChange = new EventEmitter();
  record: any = {};
  public tableInfos: TableInfoDto[];
  assembly = '';
  es = {
    id: 0,
    name: '',
    entityAssemblyName: [],
    entityBaseName: [],
  };
  schema: SFSchema;
  nameUi: {};
  change(value): void {
    this.entitySource = this.checkEntitySourceData();
    this.entitySourceChange.emit(this.entitySource);
  }

  checkEntitySourceData(): EntitySource {
    const entitySource = new EntitySource();
    entitySource.name = this.sf.getProperty('/name').value;
    const baseName = this.sf.getProperty('/entityBaseName').value;
    const assemblies = this.sf.getProperty('/entityAssemblyName').value;
    if (assemblies && assemblies.length > 0) {
      entitySource.entityAssemblyName = assemblies?.join(';');
    }
    // console.log(typeof baseName, baseName);

    if (typeof baseName !== 'string' && baseName && baseName.length > 0) {
      entitySource.entityBaseName = baseName?.join(';');
    }
    else {
      entitySource.entityBaseName = baseName;
    }
    entitySource.id = this.entitySource.id;
    return entitySource;
  }
  preview(value, component: TemplateRef<{}>): void {
    const entitySource = this.checkEntitySourceData();
    // console.log(entitySource, `preview`);
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
      nzOnOk: () => { },
    });
  }
  ngAfterViewInit(): void {

  }
  ngOnInit() {
    if (this.record.id > 0) {
      // console.log(this.record);
      // this.es.entityBaseName = this.record.entityBaseName;
      this.es.id = this.record.id;
      this.es.name = this.record.name;
      if (this.record.entityAssemblyName !== ``) {
        this.es.entityAssemblyName = this.record.entityAssemblyName.split(';');
      }
      if (this.record.entityBaseName !== ``) {
        this.es.entityBaseName = this.record.entityBaseName.split(';');
      }
      this.entitySource.id = this.record.id;
      setTimeout(() => this.change(null), 500);
      /** 获取反射出的程序集 */


    }
    this.service.getAbstractEntity().subscribe((r) => {
      const entityBaseName = this.sf.getProperty('/entityBaseName');
      entityBaseName.schema.enum = r;
      this.sf.setValue('/entityBaseName', entityBaseName);
      entityBaseName.setValue(this.es.entityBaseName, true);
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
        entityAssemblyName: {
          type: 'string',
          title: '实体所在程序集',
          ui: {
            widget: 'select',
            mode: 'tags',
            tokenSeparators: [';'],
            asyncData: () => {
              return this.service.getAssemblies();
            },
            change: () => {
              this.checkEntitySourceData();
            },
          },
          default: [],
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
    // console.log(this.schema, `schema`);
  }
  Close() { }
}
