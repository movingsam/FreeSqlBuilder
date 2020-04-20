import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzModalService, NzMessageService } from 'ng-zorro-antd';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ProjectInfo } from '../modals/projectInfo';
import { Project } from '../modals/project';

@Component({
  selector: 'app-project-info',
  template: `
<form nz-form [formGroup]="validateForm" (ngSubmit)="submitForm()">
      <nz-form-item>
        <nz-form-label [nzSm]="6" [nzXs]="24" nzRequired nzFor="projectName">项目名称</nz-form-label>
        <nz-form-control [nzSm]="10" [nzXs]="24" nzErrorTip="这里写下项目名称">
          <input nz-input formControlName="projectName" id="projectName" placeholder="项目名称" />
        </nz-form-control>
      </nz-form-item>
      <nz-form-item>
        <nz-form-label [nzSm]="6" [nzXs]="24" nzFor="rootPath" nzRequired>项目根目录路径</nz-form-label>
        <nz-form-control [nzSm]="10" [nzXs]="24" nzErrorTip="根目录路径必须填写">
          <input nz-input type="text" id="rootPath" formControlName="rootPath" placeholder="项目文件最终输出路径=项目根目录路径/输出路径" />
        </nz-form-control>
      </nz-form-item>
      <nz-form-item>
        <nz-form-label [nzSm]="6" [nzXs]="24" nzFor="outPutPath" nzRequired>输出路径</nz-form-label>
        <nz-form-control [nzSm]="10" [nzXs]="24" nzErrorTip="输出路径必须填写" >
          <input nz-input type="text" formControlName="outPutPath" id="outPutPath" placeholder="项目文件最终输出路径=项目根目录路径/输出路径" />
        </nz-form-control>
      </nz-form-item>
      <nz-form-item>
        <nz-form-label [nzSm]="6" [nzXs]="24" nzFor="author" nzRequired>作者</nz-form-label>
        <nz-form-control [nzSm]="10" [nzXs]="24"  nzErrorTip="作者必须填写">
          <input nz-input type="text" formControlName="author" id="author" placeholder="作者会提现在代码生成器的备注中" />
        </nz-form-control>
      </nz-form-item>
      <nz-form-item>
        <nz-form-control  [nzOffset]="6" [nzXs]="18"   >
          <button nz-button type='submit'   [nzType]="'primary'" >提交</button>
        </nz-form-control>
      </nz-form-item>
</form>
  `,
  styles: ['']
})
export class ProjectinfoComponent implements OnInit {

  @Output() callBack: EventEmitter<Project> = new EventEmitter();
  @Input() projectId: number;
  validateForm: FormGroup;
  projectInfo = new ProjectInfo();
  private isEdite = false;
  constructor(private fb: FormBuilder, private modalService: NzModalService,
    private message: NzMessageService, private activateInfo: ActivatedRoute,
    private router: Router, private client: HttpClient) {
    this.activateInfo.queryParams.subscribe(queryParams => {
      this.projectId = queryParams.id;
      if (this.projectId !== 0) {
        this.isEdite = true;
      }
    });
  }
  submitForm(): void {
    for (const key in this.validateForm.controls) {
      if (this.validateForm.controls.hasOwnProperty(key)) {
        this.validateForm.controls[key].markAsDirty();
        this.validateForm.controls[key].updateValueAndValidity();
      }
    }
    this.projectInfo.outPutPath = this.validateForm.controls['outPutPath'].value;
    this.projectInfo.author = this.validateForm.controls['author'].value;
    this.projectInfo.projectName = this.validateForm.controls['projectName'].value;
    this.projectInfo.rootPath = this.validateForm.controls['rootPath'].value;
    if (!this.validateForm.invalid) {
      this.submit();
    }

  }
  ngOnInit() {
    if (this.projectId) {
      this.loadProject(this.projectId);
    }
    this.controlerLoad();
  }
  controlerLoad(): void {
    this.validateForm = this.fb.group({
      projectName: [this.projectInfo.projectName, [Validators.required, Validators.maxLength(255)]],
      rootPath: [this.projectInfo.rootPath, [Validators.required, Validators.maxLength(255)]],
      outPutPath: [this.projectInfo.outPutPath, [Validators.required, Validators.maxLength(255)]],
      author: [this.projectInfo.author, [Validators.required, Validators.maxLength(255)]]
    });
  }

  loadProject(id: number): void {
    this.client.get<Project>(`/api/project/${id}`).subscribe(res => {
      this.projectInfo = res.projectInfo;
      this.callBack.emit(res);
      this.controlerLoad();
    });
  }
  submit(): void {
    if (this.projectInfo.id && this.projectInfo.id !== 0) {
      console.log(`put`);
      this.client.put<Project>(`/api/project/Info`, this.projectInfo)
        .subscribe((res) => {
          this.message.success('修改成功');
          this.callBack.emit(res);
        });
      return;
    }
    this.client.post<Project>(`/api/project/Info/New`, this.projectInfo)
      .subscribe((res) => {
        this.message.success('添加成功 可以进行下一步');
        this.callBack.emit(res);
        console.log(this.callBack);
      });
  }

}
