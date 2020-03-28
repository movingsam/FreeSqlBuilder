import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NzTreeNodeOptions, NzFormatEmitEvent, NzMessageService } from 'ng-zorro-antd';
@Component({
    selector: 'app-pathdir-select',
    template: `
     <nz-tree-select style="width: 100%" nzPlaceHolder="选择目录" [(ngModel)]="value"
     (ngModelChange)="onChange($event)"
     [nzDropdownMatchSelectWidth]="true" nzShowSearch
            [nzDropdownStyle]="{ 'max-height': '300px' }" [nzNodes]="nodes" [nzAsyncData]="true"
            (nzExpandChange)="onExpandChange($event)">
            <ng-template #nzTreeTemplate let-node>
                <span class="ant-tree-node-content-wrapper" [class.ant-tree-node-selected]="node.isSelected">
                    <span> <i nz-icon [nzType]="node.isExpanded ? 'folder-open' : 'folder'"></i> {{ node.title }} </span>
                </span>
            </ng-template>
     </nz-tree-select>
    `,
    styles: ['']
})
export class PathdirSelectComponent implements OnInit {
    // tslint:disable-next-line: no-output-on-prefix
    @Output() change: EventEmitter<any> = new EventEmitter();
    @Input() onlyDir = true;
    @Input() value = '';
    @Input() fileType = '';
    nodes: NzTreeNodeOptions[] = [];
    constructor(private client: HttpClient, private message: NzMessageService) {
        client.get<NzTreeNodeOptions[]>('http://localhost:5000/api/DriveInofs')
            .subscribe((data) => { this.nodes = data; }, error => console.log(error));
    }
    ngOnInit() {
    }
    onExpandChange(e: NzFormatEmitEvent): void {
        const node = e.node;
        if (node.key.endsWith('.cshtml')) {
            return;
        }
        if (node && node.getChildren().length === 0 && node.isExpanded && !node.key.endsWith('.cshtml')) {
            this.loadNode(node.key).then(data => {
                node.addChildren(data);
            });
        }
    }
    loadNode(path: string): Promise<NzTreeNodeOptions[]> {

        let param = `http://localhost:5000/api/DriveInfos/Dir?path=${path}`;
        if (!this.onlyDir) {
            if (this.fileType !== '') {
                param = `http://localhost:5000/api/File/${this.fileType}?path=${path}`;
            } else {
                this.message.error('文件类型不能为空');
            }
        }
        return new Promise(resolve => {
            this.client.get<NzTreeNodeOptions[]>(param).subscribe(
                (data) => {
                    resolve(data);
                },
                (error) => {
                    console.log(error);
                });
        });
    }
    onChange($event): void {
        this.change.emit($event);
    }
}
