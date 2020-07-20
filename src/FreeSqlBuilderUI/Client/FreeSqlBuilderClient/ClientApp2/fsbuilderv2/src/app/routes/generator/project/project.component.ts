import { Component, OnInit, ViewChild } from '@angular/core';
import { STColumn, STComponent, STData, STRes } from '@delon/abc/st';
import { SFSchema } from '@delon/form';
import { ModalHelper, _HttpClient } from '@delon/theme';
import { Console } from 'console';
import { NzMessageService } from 'ng-zorro-antd/message';
import { Page, PageView } from 'src/app/core/services/interface/dto';
import { Project } from 'src/app/core/services/interface/project';
import { ProjectService } from 'src/app/core/services/project.service';
import { GeneratorProjectEditComponent } from './edit/edit.component';

@Component({
  selector: 'fb-generator-project',
  templateUrl: './project.component.html',
})
export class GeneratorProjectComponent implements OnInit {
  page: Page;
  data: Project[];
  url = `api/project/page`;
  res: STRes = {
    reName: {
      list: ['datas'],
    },
    process: (datas, raw) => {
      console.log(raw);
      return datas;
    },
  };

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
    { title: '项目名称', index: 'projectInfo.projectName' },
    { title: '配置名称', index: 'generatorModeConfig.name' },
    {
      title: '操作',
      buttons: [
        {
          text: '生成',
          type: 'link',
          click: (item: any) => {
            this.projectService.buildTask(item.id).subscribe((r) => {
              this.msgSer.success(`生成成功`);
              this.st.reload();
            });
          },
        },
        {
          icon: 'edit',
          text: '编辑',
          type: 'modal',
          modal: {
            component: GeneratorProjectEditComponent,
          },
          click: (val, modal) => {
            if (modal === true) {
              this.st.reload();
            }
          },
        },
        {
          text: '删除',
          type: 'del',
          click: (item: any) => {
            this.projectService.deleteProject(item.id).subscribe((r) => {
              this.msgSer.success(`删除成功`);
              this.st.reload();
            });
          },
        },
      ],
    },
  ];
  // Data: string;
  // process = (data: STData[]) => {
  //   this.Data = JSON.stringify(data);
  //   console.log(data);
  //   return data;
  // };

  constructor(private projectService: ProjectService, private modal: ModalHelper, private msgSer: NzMessageService) {
    this.page = new Page();
  }

  ngOnInit() {}
  add() {
    this.modal
      .createStatic(
        GeneratorProjectEditComponent,
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
      .subscribe(() => {
        console.log(`刷新`);
        this.st.reload();
      });
  }
}
