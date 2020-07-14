import { Component, OnInit, ViewChild } from '@angular/core';
import { STColumn, STComponent, STData, STRes } from '@delon/abc/st';
import { SFSchema } from '@delon/form';
import { ModalHelper, _HttpClient } from '@delon/theme';
import { Console } from 'console';
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
      no: {
        type: 'string',
        title: '编号',
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
        { text: '查看', click: (item: any) => `/form/${item.id}` },
        {
          icon: 'edit',
          text: '编辑',
          type: 'modal',
          modal: {
            component: GeneratorProjectEditComponent,
            modalOptions: {
              nzOnOk: (res) => {
                console.log(res);
                if (res === true) {
                  this.st.reload();
                }
              },
            },
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

  constructor(private projectService: ProjectService, private modal: ModalHelper) {
    this.page = new Page();
  }

  ngOnInit() {}
  add() {
    console.log(this.st);
    // console.log(this.st.dataSource);
    // this.modal
    //   .createStatic(FormEditComponent, { i: { id: 0 } })
    //   .subscribe(() => this.st.reload());
  }
}
