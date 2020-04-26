import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChange } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzModalService, NzMessageService } from 'ng-zorro-antd';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { GeneratorModeConfig, GeneratorMode, PickType } from '../modals/generatormodeconfig';
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
            <label nz-radio   nzValue=0>DbFirst</label>
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
              <nz-form-label nz-col [nzSm]="6" [nzXs]="24">数据库连接字符串</nz-form-label>
              <nz-form-control [nzSm]="10" [nzXs]="24">
                <input nz-input type="text" formControlName="dataSourceConnectionStr" id="dataSourceConnectionStr"
                  name='dataSourceConnectionStr' placeholder="数据库连接字符串" />
              </nz-form-control>
            </nz-form-item>
          <div nz-col [nzSpan]="8">
            <nz-form-control  [nzSm]="5" [nzXs]="24">
              <button nz-button style="width:100%" (click)='submitForm()' [nzType]="'primary'">提交</button>
            </nz-form-control>
          </div>
          </div>
        </div>
        <div id="baseEntity" *ngIf="this.validateForm.get('generatorMode').value === '1'">
          <div nz-col [nzSpan]="14">
            <nz-form-item>
              <nz-form-label nz-col [nzSm]="4" [nzXs]="24">选择相关程序集/实体基类</nz-form-label>
              <nz-form-control [nzSm]="10" [nzXs]="24" >
                    <nz-select formControlName="entityAssemblyName" nzAllowClear nzPlaceHolder="选择程序集" (ngModelChange)="assemblyChange($event)">
                  <nz-option *ngFor="let item of assemblyList" [nzLabel]="item.key" [nzValue]="item.value" ></nz-option>
                </nz-select>
              </nz-form-control>
              <nz-form-control [nzSm]="10" [nzXs]="24" >
                  <nz-select formControlName="entityBaseName" nzAllowClear nzPlaceHolder="选择基类"  (ngModelChange)="entityBaseChange($event)" >
                    <nz-option *ngFor="let item of itemList" [nzLabel]="item.key" [nzValue]="item.value"></nz-option>
                  </nz-select>
              </nz-form-control>
            </nz-form-item>
          </div>
          <div nz-col [nzSpan]="8">
            <nz-form-control  [nzSm]="5" [nzXs]="24">
              <button nz-button style="width:100%" (click)='submitForm()' [nzType]="'primary'">提交</button>
            </nz-form-control>
          </div>
          <div nz-col [nzSpan]="14">
              <nz-form-label nz-col [nzSm]="4" [nzXs]="24">确定实体方式</nz-form-label>
              <nz-form-control [nzSm]="10" [nzXs]="24" >
                <nz-radio-group formControlName="pickType" [nzButtonStyle]="'solid'">
                  <label nz-radio-button nzValue="0">选中</label>
                  <label nz-radio-button nzValue="1">忽略</label>
                </nz-radio-group>
              </nz-form-control>
        </div>
        </div>
      </nz-card>
      <nz-form-item>
      <div nz-col [nzSpan]="24" *ngIf="this.previewShow">
        <app-table-preview  (allTable)="getAllTable($event)"
         [ignoreTables] = "this.generatorModeConfig.ignoreTables"
         [pickTables] = "this.generatorModeConfig.includeTables"
         [pickType] = "this.validateForm.get('pickType').value"
         (callBack) = "getTables($event)"
         [entityAssemblyName]="this.validateForm.get('entityAssemblyName').value"
         [entityBaseName]="this.validateForm.get('entityBaseName').value">
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
    this.generatorModeConfig = new GeneratorModeConfig(0);
  }

  ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
    this.project = changes['project']['currentValue'];
    if (this.project.generatorModeConfig) {
      this.generatorModeConfig = this.project.generatorModeConfig;
      this.initValidateForm();
      this.baseEntity();
    }
  }
  submitForm() {

    this.generatorModeConfig.projectId = this.project.id;
    this.generatorModeConfig.generatorMode = this.validateForm.controls['generatorMode'].value;
    this.generatorModeConfig.entityBaseName = this.validateForm.controls['entityBaseName'].value;
    this.generatorModeConfig.entityAssemblyName = this.validateForm.controls['entityAssemblyName'].value;
    this.generatorModeConfig.pickType = this.validateForm.controls['pickType'].value;
    this.generatorModeConfig.dataSource.name = this.validateForm.controls[`dataSourceDb`].value;
    this.generatorModeConfig.dataSource.dbType = this.validateForm.controls[`dataSourceType`].value;
    this.generatorModeConfig.dataSource.connectionString = this.validateForm.controls[`dataSourceConnectionStr`].value;
    if (!this.generatorModeConfig.generatorMode) {
      this.message.warning(`未检测到模式无法提交`);
      return;
    }
    if (!this.generatorModeConfig.projectId) {
      this.message.warning(`未检测到相关项目 请先提交项目详情`);
      return;
    }
    if (this.generatorModeConfig.generatorMode === GeneratorMode.CodeFirst) {
      if (this.generatorModeConfig.entityAssemblyName === '') {
        this.message.warning(`必须选择一个程序集进行反射`);
        return;
      }
    } else {
      if (this.generatorModeConfig.dataSource.name === ``) {
        this.message.warning(`必须填写数据库名称`);
      }
      if (!this.generatorModeConfig.dataSource.dbType) {
        this.message.warning(`必须选择一个数据库类型`);
      }
      if (this.generatorModeConfig.dataSource.connectionString === ``) {
        this.message.warning(`必须填写数据库连接字符串`);
      }
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
    console.log(this.generatorModeConfig.pickType);
    this.validateForm = this.fb.group({
      generatorMode: [`${this.generatorModeConfig.generatorMode}`, [Validators.required]],
      entityBaseName: [`${this.generatorModeConfig.entityBaseName}`, []],
      entityAssemblyName: [`${this.generatorModeConfig.entityAssemblyName}`, [Validators.required]],
      pickType: [`${this.generatorModeConfig.pickType}`, [Validators.required]],
      dataSourceType: [`${ this.dataSourceType[this.generatorModeConfig.dataSource.dbType]}`, [Validators.required]],
      dataSourceDb: [`${this.generatorModeConfig.dataSource.name}`, [Validators.required]],
      dataSourceConnectionStr: [`${this.generatorModeConfig.dataSource.connectionString}`, [Validators.required]]

    });
  }
  getAssemblies() {
    this.client.get<Item[]>(`/api/Assemblies`).subscribe(res => {
      this.assemblyList = res;
    });
  }
  assemblyChange(e): void {
    this.baseEntity();
  }
  entityBaseChange(e): void {
    this.preview();
  }
  baseEntity(): void {
    this.client.get<Item[]>(`/api/BaseClass/${this.validateForm.controls['entityAssemblyName'].value}`).subscribe((data) => {
      this.itemList = data;
    });
    this.validateForm.patchValue({ 'entityBaseName': '' });
    this.preview();
  }
  preview(): void {
    this.validateForm.controls['entityAssemblyName'].markAsDirty();
    this.validateForm.controls['entityAssemblyName'].updateValueAndValidity();
    if (!this.validateForm.invalid) {
      this.generatorModeConfig.entityAssemblyName = this.validateForm.controls['entityAssemblyName'].value;
      this.generatorModeConfig.generatorMode = this.validateForm.controls['generatorMode'].value;
      this.generatorModeConfig.entityBaseName = this.validateForm.controls['entityBaseName'].value;
      this.previewShow = true;
    } else {
      this.previewShow = false;
    }
  }
  getAllTable(): void {
    if (this.project) {
      this.generatorModeConfig.projectId = this.project.id;
    } else {
      this.message.warning(`无法找到项目`);
    }
  }
  getTables(e): void {
    if (this.validateForm.controls['pickType'].value === "0") {
      this.generatorModeConfig.includeTables = e;
    } else {
      this.generatorModeConfig.ignoreTables = e;
    }
  }
}