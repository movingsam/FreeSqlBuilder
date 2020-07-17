import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import {
  SFArrayWidgetSchema,
  SFComponent,
  SFObjectWidgetSchema,
  SFSchema,
  SFSelectWidgetSchema,
  SFTextWidgetSchema,
  SFUISchema,
} from '@delon/form';
import { ModalHelper, _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { of } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { GeneratorconfigService } from 'src/app/core/services/generatorconfig.service';
import { Page } from 'src/app/core/services/interface/dto';
import { Project } from 'src/app/core/services/interface/project';
import { ProjectService } from 'src/app/core/services/project.service';
import { GeneratorConfigEditComponent } from '../../config/edit/edit.component';

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
  @ViewChild('sf') sf: SFComponent;
  Title = '新增项目';
  i: Project;
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
    private modalHelper: ModalHelper,
    public msgSrv: NzMessageService,
    public projectService: ProjectService,
    public configService: GeneratorconfigService,
  ) {}

  ngOnInit(): void {
    if (this.record.id > 0) {
      this.projectService.getProject(this.record.id).subscribe((t) => {
        this.i = t;
        this.Title = `编辑项目:${t.projectInfo.projectName}`;
      });
    }
    this.schema = this.SchemaInit();
  }
  SchemaInit(): SFSchema {
    return {
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
              ui: {
                change: (value) => {
                  this.Title = `编辑项目${value}`;
                },
              },
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
      required: ['projectInfo.projectName', 'projectInfo.author', 'projectInfo.outPutPath', 'projectInfo.rootPath'],
    };
  }

  /**
   * 新配置
   */
  newConfig() {
    this.modalHelper
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
      .subscribe(() => this.sf.refreshSchema(this.SchemaInit()));
  }
  /**
   * 保存
   */
  save(value) {
    if (this.record.id > 0) {
      this.projectService.updateProject(value as Project).subscribe((res) => {
        this.msgSrv.success('保存成功');
        this.modal.close(true);
      });
    } else {
      this.projectService.createProject(value as Project).subscribe((res) => {
        this.msgSrv.success('新增成功');
        this.modal.close(true);
      });
    }
  }

  close() {
    this.modal.destroy();
  }
}
