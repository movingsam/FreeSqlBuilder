import { Component, OnInit, Input, OnChanges, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { NzModalService, NzMessageService } from 'ng-zorro-antd';

@Component({
  selector: 'app-build-task',
  template: `
  <nz-spin *ngIf="this.loading" nzTip="正在生成代码中">
      <nz-alert
        nzType="info"
        nzMessage="后台正在生成代码中"
        nzDescription="正在为您生成代码"
      >
      </nz-alert>
    </nz-spin>
  `,
  styles: ['']
})
export class BuildTaskComponent implements OnInit, OnChanges {
  @Input() projectid: number;
  @Output() callBack: EventEmitter<boolean> = new EventEmitter();
  loading = true;
  constructor(private client: HttpClient,
    private message: NzMessageService,
    private activateInfo: ActivatedRoute) {

  }
  ngOnChanges(changes: import('@angular/core').SimpleChanges): void {
    this.projectid = changes['projectid']['currentValue'];
    if (this.projectid && this.projectid !== 0) {
      this.run(this.projectid);
    }
  }

  ngOnInit() {

  }
  run(id) {
    this.client.post(`/api/project/task/build/${this.projectid}`, null)
      .subscribe(res => {
        this.loading = false;
        this.message.success(`成功生成`);
        this.callBack.emit(true);
      }, err => {
        this.message.error(err);
        this.loading = false;
        this.callBack.emit(false);
      });
  }

}
