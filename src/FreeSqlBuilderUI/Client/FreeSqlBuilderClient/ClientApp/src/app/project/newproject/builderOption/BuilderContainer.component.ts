import { Component, OnInit, Input, OnChanges, Output, EventEmitter } from '@angular/core';
import { BuilderOptions, BuilderType, Project } from '../../modals/project';
import { NzModalService, NzMessageService } from 'ng-zorro-antd';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-builder-container',
  template: `
    <nz-card style="width: 100%;" nzTitle="配置构建器" [nzExtra]="builderExtraTemplate">
      <nz-card-tab>
        <nz-tabset  style='margin-bottom: 0px !important'>
          <nz-tab  [nzTitle]="buildtitleTemplate" *ngFor="let item of this.builders ">
            <ng-template #buildtitleTemplate>
              <div>
                {{ item.name }}
                <i nz-icon nzType="close" (click)="removeBuilder(item.name,'builder')" class="ant-tabs-close-x"></i>
              </div>
            </ng-template>
            <app-builder-option (callBack)="CallBack($event)"  [projectid]="this.projectid" *ngIf="item" [builder]="item">
            </app-builder-option>
          </nz-tab>
        </nz-tabset>
      </nz-card-tab>
    </nz-card>
    <ng-template #builderExtraTemplate>
      <a (click)="Add()">添加</a>
    </ng-template>
    <nz-modal [(nzVisible)]="isVisible" nzTitle="添加构建器" (nzOnOk)="builderAdd()"
      (nzOnCancel)="this.isVisible = false">
      构建器名：
      <input nz-input id='key' [(ngModel)]="tag">
    </nz-modal>
  `,
  styles: [`
  `]
})
export class BuilderContainerComponent implements OnInit, OnChanges {
  @Input() projectid = 0;
  @Input() builders: BuilderOptions[] = new Array<BuilderOptions>();
  @Input() type: BuilderType;
  @Output() callBack: EventEmitter<BuilderOptions[]> = new EventEmitter();
  protected isVisible = false;
  protected tag = '';
  constructor(private modalService: NzModalService,
    private message: NzMessageService,
    private client: HttpClient) {
    console.log(this.projectid, 'init');
  }
  ngOnChanges(changes: import('@angular/core').SimpleChanges): void {
    this.builders = changes['builders']['currentValue'];
    this.projectid = changes['projectid']['currentValue'];
    console.log(this.projectid, 'changed');
  }

  ngOnInit() {
  }
  removeBuilder(key: string): void {
    const builder = this.builders;
    this.modalService.confirm({
      nzTitle: '删除构建器',
      nzContent: `<b style="color: red;">是否要删除 ${key} 构建器的配置？</b>`,
      nzOkText: '是',
      nzOkType: 'danger',
      nzOnOk: () => builder.splice(builder.findIndex(b => b.name === key), 1),
      nzCancelText: '否',
      nzOnCancel: () => console.log('Cancel')
    });
  }
  remove(key: string) {
    this.client.delete<boolean>(`/api/project/builder/${key}`).subscribe(res => {
      if (res) {
        this.message.success('删除成功');
        return;
      }
      this.message.success('删除失败');
    });
  }
  Add() {
    this.isVisible = true;
  }
  CallBack(e: BuilderOptions): void {
    if (this.builders.filter(x => x.name === e.name).length > 0) {
      this.builders.filter(x => x.name === e.name)[0] = e;
    } else {
      this.builders.push(e);
    }
    this.callBack.emit(this.builders);
  }
  builderAdd() {
    this.builders.push(new BuilderOptions(this.type, this.tag));
    this.isVisible = false;
  }

}
