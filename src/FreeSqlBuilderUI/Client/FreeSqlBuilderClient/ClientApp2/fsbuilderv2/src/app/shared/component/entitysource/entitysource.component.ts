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

@Component({
  selector: 'fb-entitysource',
  templateUrl: './entitysource.component.html',
  styles: [``],
})
export class EntitysourceComponent implements OnInit {
  @ViewChild('sf') sf: SFComponent;
  @Input() entitySource: EntitySource;
  @Output() entitySourceChange = new EventEmitter();
  public tableInfos: TableInfoDto[];
  assembly = '';
  es: EntitySource;
  schema: SFSchema;
  constructor(private service: HelperService, private modal: NzModalService, private configService: GeneratorconfigService) {
    this.schema = {
      properties: {
        name: {
          type: 'string',
          title: '名称',
        },
        entityBaseName: {
          type: 'string',
          title: '实体基类',
          enum: [],
          ui: {
            widget: 'cascader',
            showArrow: false,
            expandTrigger: 'hover',
          },
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

  change(es: EntitySource): void {
    this.entitySource = es;
    this.entitySourceChange.emit(this.entitySource);
  }

  preview(value, component: TemplateRef<{}>): void {
    const entitySource = new EntitySource();
    entitySource.entityAssemblyName = this.sf.getProperty('/entityAssemblyName').value;
    entitySource.entityBaseName = this.sf.getProperty('/entityBaseName').value;
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
    this.service.getAssemblies().subscribe((r) => {
      const entityBaseName = this.sf.getProperty('/entityBaseName');
      entityBaseName.schema.enum = r;
      console.log(r);
      this.sf.setValue('/entityBaseName', entityBaseName);
    });
  }
}
