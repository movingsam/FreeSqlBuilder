import { Component, OnInit, Input, OnChanges, Output, EventEmitter, KeyValueDiffer } from '@angular/core';
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
                <i nz-icon nzType="close" (click)="removeBuilder(item)" class="ant-tabs-close-x"></i>
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
    console.log(this.builders, `constructor builders`);
  }

  ngOnChanges(changes: import('@angular/core').SimpleChanges): void {
    this.builders = changes['builders']['currentValue'];
    this.projectid = changes['projectid']['currentValue'];
    console.log(this.builders, 'ngOnChanges');
  }

  ngOnInit() {
  }
  removeBuilder(currentBuilder: BuilderOptions): void {
    this.modalService.confirm({
      nzTitle: '删除构建器',
      nzContent: `<b style="color: red;">是否要删除 ${currentBuilder.name} 构建器的配置？</b>`,
      nzOkText: '是',
      nzOkType: 'danger',
      nzOnOk: () => this.remove(currentBuilder),
      nzCancelText: '否',
      nzOnCancel: () => console.log('Cancel')
    });
  }

  remove(currentBuilder: BuilderOptions) {
    if (currentBuilder.id && currentBuilder.id !== 0) {
      this.client.delete<boolean>(`/api/project/builder/${currentBuilder.id}`).subscribe(res => {
        if (res) {
          this.message.success('删除成功');
          return;
        }
      });
    }
    this.builders.splice(this.builders.findIndex(b => b.name === currentBuilder.name), 1);
    return;
  }
  Add() {
    this.isVisible = true;
  }
  CallBack(e: BuilderOptions): void {
    if (this.builders.filter(x => x.name === e.name).length > 0) {
      this.builders.filter(x => x.name === e.name)[0].id = e.id;
    }
    this.callBack.emit(this.builders);
  }
  builderAdd() {
    this.builders.push(new BuilderOptions(this.type, this.tag));
    this.isVisible = false;
  }

}
