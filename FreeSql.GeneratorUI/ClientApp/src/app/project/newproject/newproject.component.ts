import { Component, OnInit, Type } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectViewmodal, BuilderOptions } from '../modals/project';
import { NzModalService } from 'ng-zorro-antd/modal';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';

@Component({
  selector: 'app-newproject',
  templateUrl: './newproject.component.html',
  styles: ['']
})
export class NewProjectComponent implements OnInit {

  constructor(private fb: FormBuilder, private modalService: NzModalService,
    private message: NzMessageService,
    private router: Router, private client: HttpClient) { }

  project: ProjectViewmodal;
  tempTab: string;
  validateForm: FormGroup;
  isVisible: Map<string, boolean> = new Map().set('builder', false).set('globalBuilder', false);
  builderVisable = false;
  gBuilderVisable = false;
  csprojPath = '';
  previewShow = false;

  dataSourceType = ['MySql', 'SqlServer', 'PostgreSQL', 'Oracle',
    'Sqlite', 'OdbcOracle', 'OdbcSqlServer', 'OdbcMySql',
    'OdbcPostgreSQL', 'Odbc', 'OdbcDameng', 'MsAccess'];
  submitForm(): void {
    Object.getOwnPropertyNames(this.project).forEach(element => {
      if (this.validateForm.get(element)) {
        this.project[element] = this.validateForm.get(element).value;
      }
    });
    this.CheckController('projectName');
    this.CheckController('rootPath');
    this.CheckController('outPutPath');
    this.CheckController('generatorMode');
    this.CheckController('author');
    if (!this.validateForm.invalid) {
      this.AddTemplate();
    }
  }

  CheckController(key: string): void {
    this.validateForm.controls[key].markAsDirty();
    this.validateForm.controls[key].updateValueAndValidity();
  }

  ngOnInit(): void {
    this.project = new ProjectViewmodal();
    const modals = {};
    modals['addTabs'] = new FormControl('', []);
    this.validateForm = this.fb.group({
      projectName: [this.project.projectName, [Validators.required, Validators.maxLength(255)]],
      rootPath: [this.project.rootPath, [Validators.required, Validators.maxLength(255)]],
      outPutPath: [this.project.outPutPath, [Validators.required, Validators.maxLength(255)]],
      generatorMode: [this.project.generatorMode.toString(), [Validators.required]],
      author: [this.project.author, [Validators.required]],
      dataSourceType: [this.project.dataSource.DbType, [Validators.required]],
      dataSourceDb: [this.project.dataSource.Name, [Validators.required]],
      dataSourceConnectionStr: [this.project.dataSource.ConnectionString, [Validators.required]],
      csprojPath: ['', [Validators.required]],
      entityBaseName: [this.project.entityBaseName, [Validators.required]],
    });
    console.log(this.validateForm, 'modals');
  }
  AddTemplate() {
    this.project.dataSource.ConnectionString = this.validateForm.get('dataSourceConnectionStr').value;
    this.project.dataSource.DbType = this.dataSourceType.indexOf(this.validateForm.get('dataSourceType').value);
    this.project.dataSource.Name = this.validateForm.get('dataSourceDb').value;
    this.client.post(`http://localhost:5000/api/project/New`, this.project).subscribe((data) => {
      if (data) {
        this.message.success('成功');
        this.builderVisable = true;
        this.gBuilderVisable = true;
      }
    });
  }
  Add(key: string): void {
    this.isVisible[key] = true;

  }
  builderAdd(key: string): void {
    this.isVisible[key] = false;
    if (key === 'builder') {
      this.project.builders.push(new BuilderOptions('', this.validateForm.controls['addTabs'].value, false,
        '', '_', true, null, 0, './test', ''));
    } else {
      this.project.globalBuilders.push(new BuilderOptions('', this.validateForm.controls['addTabs'].value,
        false, '', '_', true, null, 0, './test', ''));
    }
  }
  removeBuilder(key: string, name: string): void {
    const builder = this.project.builders;
    const gbuilder = this.project.globalBuilders;
    this.modalService.confirm({
      nzTitle: '删除构建器',
      nzContent: `<b style="color: red;">是否要删除 ${key} 构建器的配置</b>`,
      nzOkText: '是',
      nzOkType: 'danger',
      nzOnOk: () => name === 'builder' ? builder.splice(builder.findIndex(b => b.name === key), 1) :
        gbuilder.splice(gbuilder.findIndex(b => b.name === key), 1),
      nzCancelText: '否',
      nzOnCancel: () => console.log('Cancel')
    });
  }

  back(e): void {
    this.router.navigateByUrl(`project/home`);
  }
  cahnged(e): void {
    this.csprojPath = e;
  }
  preview(): void {
    this.CheckController('csprojPath');
    this.CheckController('entityBaseName');
    // console.log(!this.validateForm.valid)
    if (!this.validateForm.valid) {
      this.previewShow = !this.previewShow;
    }
  }

}
