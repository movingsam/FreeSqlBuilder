import { Component, OnInit, ViewChild } from '@angular/core';
import { SFSchema, SFUISchema } from '@delon/form';
import { _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { type } from 'os';
import { BuilderService } from 'src/app/core/services/builder.service';
import { GeneratorconfigService } from 'src/app/core/services/generatorconfig.service';
import { BuilderOptions, GeneratorModeConfig } from 'src/app/core/services/interface/project';
import { ProjectService } from 'src/app/core/services/project.service';
import { TemplateService } from 'src/app/core/services/template.service';

@Component({
  selector: 'fb-generator-builder-edit',
  templateUrl: './edit.component.html',
})
export class GeneratorBuilderEditComponent implements OnInit {
  record: any = {};
  i: any;
  schema: SFSchema = {
    properties: {
      prefix: { type: 'string', title: '类名前缀', maxLength: 10, ui: { placeholder: '生成文件名及类名的前缀' } },
      name: { type: 'string', title: '构建器名', ui: { placeholder: '构建器名称,仅用来区分各个构建器,不参与文件生成操作' } },
      suffix: {
        type: 'string',
        title: '类名后缀',
        ui: { placeholder: '生成文件名及类名的后缀' }
      },
      outPutPath: {
        type: 'string',
        title: '输出路径',
        ui: {
          placeholder: '用来放置此构建器生成的文件夹名'
        }
      },
      mode: {
        type: 'number',
        enum: [{ label: '默认', value: 0, key: 0 }, { label: '首字母大写', value: 1, key: 1 }, { label: '全小写', value: 2, key: 2 }],
        ui: {
          widget: 'radio',
        },
        title: '名称转换器',
      },
      templateId: {
        type: 'number',
        title: '模板选择',
        ui: {
          widget: 'select',
          asyncData: () => {
            return this.templateService.getTemplateSelect();
          }
        }
      },

      type: {
        type: 'number',
        title: '构建器类型',
        enum: [{ label: '单表构建器', value: 0, key: 0 }, { label: '全表构建器', value: 1, key: 1 }],
        ui: { widget: 'radio' }
      },
      fileExtensions: {
        type: 'string',
        title: '文件后缀'
      },
      defaultConfigId: {
        type: 'number',
        title: '默认配置',
        ui: {
          widget: 'select',
          asyncData: () => {
            return this.configService.getGeneratorConfigSelect();
          }
        }
      },
      defaultProjectId: {
        type: 'number',
        title: '默认项目',
        ui: {
          widget: 'select',
          asyncData: () => {
            return this.projectService.getSelect();
          }
        }
      }
    },
    required: ['fileExtensions', 'name', 'outPutPath', 'templateId'],
  };
  ui: SFUISchema = {
    '*': {
      spanLabelFixed: 100,
      grid: { span: 12 },
    },
    $no: {
      widget: 'text'
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
    private service: BuilderService,
    private configService: GeneratorconfigService,
    private templateService: TemplateService,
    public http: _HttpClient,
    private projectService: ProjectService
  ) { }

  ngOnInit(): void {
    if (this.record.id > 0) {
      this.service.getBuilderById(this.record.id).subscribe(res => (this.i = res));
    }
    this.i = new BuilderOptions();
  }

  save(value: any) {
    if (this.record.id > 0) {
      this.service.updateBuilder(value).subscribe(r => {
        this.msgSrv.success(`更新成功`);
        this.modal.close(true);
      });
    } else {
      this.service.createBuilder(value).subscribe(r => {
        if (r.id > 0) {
          this.msgSrv.success(`新增成功`);
          this.modal.close(true);
        }
      });
    }
  }

  close() {
    this.modal.destroy();
  }
}
