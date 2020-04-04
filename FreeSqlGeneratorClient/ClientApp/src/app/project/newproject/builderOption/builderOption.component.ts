import { Component, OnInit, Input, Output, OnChanges, EventEmitter } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BuilderOptions, BuilderType } from '../../modals/project';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { Template } from 'src/app/template/modals/template';

@Component({
  selector: 'app-builder-option',
  templateUrl: './builderOption.component.html',
  styles: ['']
})
export class BuilderOptionComponent implements OnInit, OnChanges {
  @Input() projectid = 0;
  @Input() builder: BuilderOptions = new BuilderOptions(BuilderType.Builder, 'Entity');
  @Output() callBack: EventEmitter<BuilderOptions> = new EventEmitter();
  validateForm: FormGroup;
  templateSel: boolean;
  constructor(protected fb: FormBuilder, private client: HttpClient,
    private message: NzMessageService,
  ) {

  }
  ngOnChanges(changes: import('@angular/core').SimpleChanges): void {
    this.projectid = changes['projectid']['currentValue'];
    if (!this.projectid || this.projectid === 0) {
      this.message.warning('未检测到项目');
    }
  }
  submitForm(): void {
    for (const i in this.validateForm.controls) {
      if (this.validateForm.controls.hasOwnProperty(i)) {
        this.validateForm.controls[i].markAsDirty();
        this.validateForm.controls[i].updateValueAndValidity();
      }
    }
    if (!this.validateForm.invalid) {
      this.builder.projectId = this.projectid;
      this.builder.fileExtensions = this.validateForm.controls['fileExtensions'].value;
      this.builder.isIgnorePrefix = this.validateForm.controls['isIgnorePrefix'].value;
      this.builder.name = this.validateForm.controls['name'].value;
      this.builder.prefix = this.validateForm.controls['prefix'].value;
      this.builder.splitDot = this.validateForm.controls['splitDot'].value;
      this.builder.outPutPath = this.validateForm.controls['outPutPath'].value;
      this.builder.mode = this.validateForm.controls['mode'].value;
      this.builder.suffix = this.validateForm.controls['suffix'].value;
      this.builder.classBase = this.validateForm.controls['classBase'].value;
      if (this.builder.id && this.builder.id !== 0) {
        this.UpdateBuilder();
        return;
      } this.AddBuilder();
    }
  }

  ngOnInit() {
    this.builder.template = new Template();
    this.validateForm = this.fb.group({
      name: [`${this.builder.name}`, [Validators.required]],
      isServiceOnly: [`${this.builder.isServiceOnly}`, [Validators.required]],
      prefix: [`${this.builder.prefix}`, []],
      splitDot: [`${this.builder.splitDot}`, []],
      isIgnorePrefix: [`${this.builder.isIgnorePrefix}`, [Validators.required]],
      outPutPath: [`${this.builder.outPutPath}`, [Validators.required]],
      mode: [`${this.builder.mode}`, [Validators.required]],
      suffix: [`${this.builder.suffix}`, []],
      classBase: [`${this.builder.classBase}`, []],
      fileExtensions: [`${this.builder.fileExtensions}`, [Validators.required]]
    });
    console.log(this.validateForm, 'modal');
  }
  AddBuilder(): void {
    this.client.post<BuilderOptions>(`/api/project/Builder/New`, this.builder).subscribe(res => {
      this.message.success(`成功上传配置`);
      this.builder = res;
      console.log(res);
      this.callBack.emit(this.builder);
    }, err => {
      console.log(err);
      this.message.error(err);
    });
  }
  UpdateBuilder(): void {
    this.client.put<BuilderOptions>(`/api/project/Builder`, this.builder).subscribe(res => {
      this.message.success(`成功修改配置`);
      this.builder = res;
      this.callBack.emit(this.builder);
    }, err => {
      this.message.error(err);
    });
  }
  selectTemplate(): void {
    this.templateSel = true;
  }
  selectCallBack(e: Template): void {
    console.log(e);
    this.builder.templateId = e.id;
    this.builder.template = e;
    this.templateSel = false;
  }

}
