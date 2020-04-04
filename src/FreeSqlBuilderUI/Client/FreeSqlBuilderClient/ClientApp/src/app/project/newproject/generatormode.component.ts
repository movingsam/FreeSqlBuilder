import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChange } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzModalService, NzMessageService } from 'ng-zorro-antd';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { GeneratorModeConfig, GeneratorMode } from '../modals/generatormodeconfig';
import { Item } from '../modals/item';
import { Project } from '../modals/project';

@Component({
  selector: 'app-generator-mode',
  template: `
    <form nz-form [formGroup]="validateForm" >
    <nz-form-item>
        <nz-form-label [nzSm]="4" [nzXs]="24" nzFor="generatorMode" nzRequired>
          <span>生成器模式
            <i nz-icon nz-tooltip nzTitle="CodeFirst从代码实体派生所有逻辑DbFirst从数据库结构派生" nzType="question-circle"
              nzTheme="outline"></i>
          </span>
        </nz-form-label>
        <nz-form-control [nzSm]="10" [nzXs]="24" nzErrorTip="CodeFirst/DbFirst">
          <nz-radio-group formControlName="generatorMode">
            <label nz-radio [nzDisabled]="true" nzValue=0>DbFirst</label>
            <label nz-radio nzValue=1>CodeFirst(当前Web项目)</label>
          </nz-radio-group>
        </nz-form-control>
      </nz-form-item>
      <nz-card  nzTitle="模式相关配置项">
        <div id="dbSetting" *ngIf="this.validateForm.get('generatorMode').value === '0'">
          <div nz-col [nzSpan]="10">
            <nz-form-item>
              <nz-form-label nz-col [nzSm]="6" [nzXs]="24">数据库类型</nz-form-label>
              <nz-form-control [nzSm]="10" [nzXs]="24">
                <nz-select formControlName="dataSourceType">
                  <nz-option *ngFor="let p of this.dataSourceType" [nzValue]="p" [nzLabel]="p">
                  </nz-option>
                </nz-select>
              </nz-form-control>
            </nz-form-item>
          </div>
          <div nz-col [nzSpan]="14">
            <nz-form-item>
              <nz-form-label nz-col [nzSm]="4" [nzXs]="24">数据库名</nz-form-label>
              <nz-form-control [nzSm]="10" [nzXs]="24">
                <input nz-input type="text" formControlName="dataSourceDb" id="dataSourceDb" name='dataSourceDb'
                  placeholder="数据库名称" />
              </nz-form-control>
            </nz-form-item>
          </div>
          <div nz-col [nzSpan]="10">
            <nz-form-item>
              <nz-form-label nz-col [nzSm]="8" [nzXs]="24">数据库连接字符串</nz-form-label>
              <nz-form-control [nzSm]="10" [nzXs]="24">
                <input nz-input type="text" formControlName="dataSourceConnectionStr" id="dataSourceConnectionStr"
                  name='dataSourceConnectionStr' placeholder="数据库连接字符串" />
              </nz-form-control>
            </nz-form-item>
          </div>
        </div>
        <div id="baseEntity" *ngIf="this.validateForm.get('generatorMode').value === '1'">
          <div nz-col [nzSpan]="14">
            <nz-form-item>
              <nz-form-label nz-col [nzSm]="4" [nzXs]="24">实体基类</nz-form-label>
              <nz-form-control [nzSm]="10" [nzXs]="24" >
                    <nz-select formControlName="entityAssemblyName" nzAllowClear nzPlaceHolder="选择程序集" (ngModelChange)="assemblyChange($event)">
                  <nz-option *ngFor="let item of assemblyList" [nzLabel]="item.key" [nzValue]="item.value" ></nz-option>
                </nz-select>
              </nz-form-control>
              <nz-form-control [nzSm]="10" [nzXs]="24" >
                    <nz-select formControlName="entityBaseName" nzAllowClear nzPlaceHolder="选择基类">
                  <nz-option *ngFor="let item of itemList" [nzLabel]="item.key" [nzValue]="item.value"></nz-option>
                </nz-select>
              </nz-form-control>
            </nz-form-item>
          </div>
          <div nz-col [nzSpan]="8">
            <nz-form-control  [nzSm]="5" [nzXs]="24">
              <button nz-button style="width:100%" (click)="preview()" > 查看</button>
            </nz-form-control>
            <nz-form-control  [nzSm]="5" [nzXs]="24">
              <button nz-button style="width:100%" (click)='submitForm()' [nzType]="'primary'">提交</button>
            </nz-form-control>
          </div>
        </div>
      </nz-card>
      <nz-form-item>
      <div nz-col [nzSpan]="24" *ngIf="this.previewShow">
        <app-table-preview  (allTable)="getAllTable($event)" [entityAssemblyName]="this.validateForm.get('entityAssemblyName').value"  [entityBaseName]="this.validateForm.get('entityBaseName').value">
        </app-table-preview>
      </div>
      </nz-form-item>
</form>
    `
})

export class GeneratorModeComponent implements OnInit, OnChanges {

  @Output() callBack: EventEmitter<Project> = new EventEmitter();
  @Input() project: Project = new Project();
  validateForm: FormGroup;
  itemList: Item[];
  assemblyList: Item[];
  previewShow: boolean;
  dataSourceType = ['MySql', 'SqlServer', 'PostgreSQL', 'Oracle',
    'Sqlite', 'OdbcOracle', 'OdbcSqlServer', 'OdbcMySql',
    'OdbcPostgreSQL', 'Odbc', 'OdbcDameng', 'MsAccess'];
  generatorModeConfig: GeneratorModeConfig;
  constructor(private fb: FormBuilder, private modalService: NzModalService,
    private message: NzMessageService,
    private router: Router, private client: HttpClient) {
    console.log(this.project, 'constructor');
  }

  ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
    this.project = changes['project']['currentValue'];
    this.generatorModeConfig = this.project.generatorModeConfig;
    if (this.project.generatorModeConfig) {
      console.log(this.project, 'initValidateForm');
      this.initValidateForm();
      this.baseEntity();
    }
  }
  submitForm() {
    this.generatorModeConfig.projectId = this.project.id;
    this.generatorModeConfig.generatorMode = this.validateForm.controls['generatorMode'].value;
    this.generatorModeConfig.entityBaseName = this.validateForm.controls['entityBaseName'].value;
    this.generatorModeConfig.entityAssemblyName = this.validateForm.controls['entityAssemblyName'].value;
    if (!this.generatorModeConfig.generatorMode) {
      this.message.warning(`未检测到模式无法提交`);
      return;
    }
    if (!this.generatorModeConfig.projectId) {
      this.message.warning(`未检测到相关项目 请先提交项目详情`);
      return;
    }
    if (this.generatorModeConfig.id && this.generatorModeConfig.id !== 0) {
      this.updateConfig();
      return;
    }
    this.newConfig();
  }

  newConfig() {
    this.client.post<GeneratorModeConfig>(`/api/project/Config/New`, this.generatorModeConfig).subscribe(res => {
      this.project.generatorModeConfig = res;
      this.callBack.emit(this.project);
      this.message.success(`成功提交,可以执行下一步`);
    });
  }
  updateConfig(): void {
    this.client.put<GeneratorModeConfig>(`/api/project/Config`, this.generatorModeConfig).subscribe(res => {
      this.project.generatorModeConfig = res;
      this.callBack.emit(this.project);
      this.message.success(`成功更新,可以执行下一步`);
    });
  }

  ngOnInit() {
    this.getAssemblies();
    if (this.project.generatorModeConfig) {
      this.generatorModeConfig = this.project.generatorModeConfig;
    } else {
      this.generatorModeConfig = new GeneratorModeConfig(this.project.id);
    }
    this.initValidateForm();
  }

  initValidateForm() {
    this.validateForm = this.fb.group({
      generatorMode: [`${this.generatorModeConfig.generatorMode}`, [Validators.required]],
      entityBaseName: [`${this.generatorModeConfig.entityBaseName}`, [Validators.required]],
      entityAssemblyName: [`${this.generatorModeConfig.entityAssemblyName}`, [Validators.required]]
    });
  }
  getAssemblies() {
    this.client.get<Item[]>(`/api/Assemblies`).subscribe(res => {
      this.assemblyList = res;
    });
  }
  assemblyChange(e): void {
    console.log(e);
    this.baseEntity();
  }
  baseEntity(): void {
    // this.validateForm.setValue({ 'entityBaseName': [''] });
    this.client.get<Item[]>(`/api/BaseClass/${this.validateForm.controls['entityAssemblyName'].value}`).subscribe((data) => {
      this.itemList = data;
    });
  }
  preview(): void {
    this.validateForm.controls['entityBaseName'].markAsDirty();
    this.validateForm.controls['entityBaseName'].updateValueAndValidity();
    if (!this.validateForm.invalid) {
      console.log(2);
      this.generatorModeConfig.entityAssemblyName = this.validateForm.controls['entityAssemblyName'].value;
      this.generatorModeConfig.generatorMode = this.validateForm.controls['generatorMode'].value;
      this.generatorModeConfig.entityBaseName = this.validateForm.controls['entityBaseName'].value;
      this.previewShow = !this.previewShow;
    }
  }
  getAllTable(e): void {
    if (this.project) {
      this.generatorModeConfig.projectId = this.project.id;
    } else {
      this.message.warning(`无法找到项目`);
    }
    console.log(this.generatorModeConfig);
  }
}
