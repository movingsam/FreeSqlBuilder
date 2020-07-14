import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { SFArrayWidgetSchema, SFObjectWidgetSchema, SFSchema, SFSelectWidgetSchema, SFTextWidgetSchema, SFUISchema } from '@delon/form';
import { _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { of } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { GeneratorconfigService } from 'src/app/core/services/generatorconfig.service';
import { Page } from 'src/app/core/services/interface/dto';
import { ProjectService } from 'src/app/core/services/project.service';

@Component({
  selector: 'fb-generator-project-edit',
  templateUrl: './edit.component.html',
  styles: [
    `
      :host ::ng-deep .sf__fixed {
        flex-flow: wrap;
      }

      ::-webkit-scrollbar {
        /*隐藏滚轮*/

        display: none;
      }
    `,
  ],
})
export class GeneratorProjectEditComponent implements OnInit {
  record: any = {};
  @ViewChild('moreConfig', { static: true }) private moreConfig: TemplateRef<void>;
  i: any;
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
    public msgSrv: NzMessageService,
    public projectService: ProjectService,
    public configService: GeneratorconfigService,
  ) {}

  ngOnInit(): void {
    if (this.record.id > 0) {
      this.projectService.getProject(this.record.id).subscribe((t) => (this.i = t));
    }
    this.schema = {
      properties: {
        id: { type: 'number', ui: { widget: 'text' } as SFTextWidgetSchema, title: '编号' },
        projectInfo: {
          title: '项目信息',
          type: 'object',
          ui: {
            type: 'card',
            grid: { span: 24 },
          } as SFObjectWidgetSchema,
          properties: {
            projectName: {
              type: 'string',
              title: '项目名称',
              description: '项目的名称',
            },
            author: {
              type: 'string',
              title: '作者',
              description: '项目作者',
            },
            outPutPath: {
              type: 'string',
              title: '输出路径',
              description: '输出路径将跟在物理根路径后面',
            },
            rootPath: {
              type: 'string',
              title: '物理根路径',
              description: '最终会输出到的物理路径',
            },
          },
        },
        generatorModeConfig: {
          type: 'object',
          title: '生成器配置',
          ui: {
            type: 'card',
            cardExtra: this.moreConfig,
            grid: { span: 24 },
          } as SFObjectWidgetSchema,
          properties: {
            id: {
              type: 'number',
              ui: {
                widget: 'select',
                asyncData: () => this.configService.getGeneratorConfigSelect(),
              } as SFSelectWidgetSchema,
              title: '选择',
            },
          },
        },
        projectBuilders: {
          type: 'array',
          title: '构建器',
          properties: {},
          ui: {
            grid: {
              arraySpan: 24,
            },
          } as SFArrayWidgetSchema,
        },
      },
      required: ['owner', 'callNo', 'href', 'description'],
    };
  }
  newConfig() {
    // this.modal.open()
  }
  save(value: any) {
    this.projectService.updateProject(value.id, value).subscribe((res) => {
      this.msgSrv.success('保存成功');
      this.modal.close(true);
    });
  }

  close() {
    this.modal.destroy();
  }
}
