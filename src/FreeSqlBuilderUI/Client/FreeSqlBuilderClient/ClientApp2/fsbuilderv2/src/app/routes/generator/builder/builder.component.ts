import { Component, OnInit, ViewChild } from '@angular/core';
import { STColumn, STColumnTag, STComponent, STData } from '@delon/abc/st';
import { SFSchema } from '@delon/form';
import { ModalHelper, _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { type } from 'os';
import { BuilderService } from 'src/app/core/services/builder.service';
import { Page } from 'src/app/core/services/interface/dto';
import { BuilderOptions } from 'src/app/core/services/interface/project';
import { ProjectService } from 'src/app/core/services/project.service';
import { GeneratorBuilderEditComponent } from './edit/edit.component';

@Component({
  selector: 'fb-generator-builder',
  templateUrl: './builder.component.html',
})
export class GeneratorBuilderComponent implements OnInit {
  url = `api/builder`;
  searchSchema: SFSchema = {
    properties: {
      no: {
        type: 'string',
        title: '编号'
      }
    }
  };
  formData: BuilderOptions[] = [];
  @ViewChild('st', { static: false }) st: STComponent;
  TAG: STColumnTag = {
    0: { text: '单表', color: 'green' },
    1: { text: '全表', color: 'blue' },
  };
  MODE: STColumnTag = {
    0: { text: '默认', color: 'default' },
    1: { text: '首字大写', color: 'blue' },
    2: { text: '全小写', color: 'green' },
  };

  columns: STColumn[] = [
    { title: '编号', index: 'id' },
    { title: '构建器名', index: 'name' },
    { title: '转换器模式', index: 'mode', type: 'tag', tag: this.MODE },
    { title: '模板名称', index: 'template.templateName' },
    { title: '文件后缀', index: 'fileExtensions' },
    { title: '构建器类型', type: 'tag', index: 'type', tag: this.TAG },
    {
      title: '操作',
      buttons: [
        {
          text: '生成',
          type: 'link',
          click: (value: any) => {
            console.log(value, `click`);
            this.projectService.buildTempTask(value.id)
              .subscribe(x => {
                this.msgServ.success(`生成成功!文件地址${x}`);
              });
          }
        },
        {
          text: '编辑', type: 'modal', modal: {
            component: GeneratorBuilderEditComponent,
            modalOptions: {
              nzWidth: '80vw',
              nzBodyStyle: {
                'overflow-y': 'scroll',
                'max-height': '70vh',
              },
            },
          },
          click: (val, modal) => {
            if (modal === true) {
              this.st.reload();
            }
          },
        }, {
          text: '删除', type: 'del', click: (value: any) => {
            this.service.deleteBuilder(value.id).subscribe(r => {
              this.msgServ.success(`成功删除构建器${value.name}`);
              this.st.reload();
            });
          }
        }
      ]
    }
  ];

  constructor(private service: BuilderService, private modal: ModalHelper, private msgServ: NzMessageService, private projectService: ProjectService) { }

  ngOnInit() {
    // this.service.getBuilder(new Page()).subscribe(r => {
    //   this.formData = r.datas;
    // });
  }

  add() {
    this.modal
      .createStatic(GeneratorBuilderEditComponent, { i: { id: 0 } })
      .subscribe(() => this.st.reload());
  }

}
