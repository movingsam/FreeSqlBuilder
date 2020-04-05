import { Component, OnInit, Type } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Project, BuilderOptions, BuilderType } from '../modals/project';
import { NzModalService } from 'ng-zorro-antd/modal';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { Item } from '../modals/item';

@Component({
  selector: 'app-newproject',
  templateUrl: './newproject.component.html',
  styles: [`

  .stepbtn{
    margin:10px;
  }

  .steps-content {
        margin-top: 16px;
        border: 1px dashed #e9e9e9;
        border-radius: 6px;
        background-color: #fafafa;
        min-height: 500px;
        text-align: left;
        padding-top: 80px;
      }
  `]
})
export class NewProjectComponent implements OnInit {
  current = 0;
  project: Project;
  projectId: number;
  tempTab: string;
  validateForm: FormGroup;
  itemList: Item[];
  isVisible: Map<string, boolean> = new Map().set('builder', false).set('globalBuilder', false);
  tag: string;
  builderVisable = false;
  gBuilderVisable = false;
  csprojPath = '';
  previewShow = false;
  currentFinish = new Map<number, boolean>();
  constructor(private fb: FormBuilder, private modalService: NzModalService,
    private message: NzMessageService, private activateInfo: ActivatedRoute,
    private router: Router, private client: HttpClient) {
    for (let index = 0; index < 6; index++) {
      this.currentFinish.set(index, false);
    }
    this.project = new Project();
    this.activateInfo.queryParams.subscribe(queryParams => {
      this.projectId = queryParams.id;
      if (this.projectId && this.projectId !== 0) {
        // this.loadProject(this.projectId);
      }
    });
  }



  callBack(e): void {
    this.project = e;
    if (this.project.projectInfoId !== 0) {
      this.currentFinish[0] = true;
    }
    if (this.project.generatorModeConfigId !== 0) {
      this.currentFinish[1] = true;
    }
    if (this.project.builders.length > 0) {
      this.currentFinish[2] = true;
    }
    this.currentFinish[3] = true;
    console.log(this.currentFinish, 'onload');
    this.projectId = e.id;
    this.currentFinish[this.current] = true;
  }

  builderCallBack(e: BuilderOptions[]): void {
    console.log(e.map(x => x.id));
    this.project.builders = e;
    if (this.project.builders.length > 0) {
      this.currentFinish[2] = true;
      this.currentFinish[3] = true;
    }
  }
  IsFinish(e: boolean): void {
    if (e) {
      this.current += 1;
    }
  }

  pre(): void {
    this.current -= 1;
  }
  next(): void {
    this.current += 1;
  }

  ngOnInit(): void {
    this.project.builders = new Array<BuilderOptions>();
    this.project.globalBuilders = new Array<BuilderOptions>();
  }
  finished() {
    this.router.navigateByUrl(`project/home`);
  }
  getCodePath() {
    return `${this.project.projectInfo.rootPath}\\${this.project.projectInfo.outPutPath}\\`;
  }
  back(e): void {
    this.router.navigateByUrl(`project/home`);
  }
}
