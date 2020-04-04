import { Component, OnInit, Input } from '@angular/core';
import { TemplateKeyword } from '../../modals/keyword';
import { MentionOnSearchTypes, NzMessageService } from 'ng-zorro-antd';
import { Template } from '../../modals/template';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-template-pre',
  template: `
   <nz-mention [nzSuggestions]="gramar" id='template' (nzOnSearchChange)="onSearchChange($event)"
            [nzValueWith]="valueWith" [nzPrefix]="nzPrefix">
            <textarea *ngIf="preview === false && this.template && this.template.templateContent" rows="10"  nzMentionTrigger nz-input [(ngModel)]="template.templateContent"
                placeholder=""></textarea>
            <ng-container *nzMentionSuggestion="let item">
                <span>{{ item.key+item.word }}</span>
            </ng-container>
    </nz-mention>
        <div *ngIf="preview === true">
            <pre><code fg-highlight >{{template.templateContent}}</code></pre>
        </div>
        <div>
          <button nz-button nzType="primary" (click)="update()">提交</button>
          <button nz-button (click)="render()">{{btnTxt}}</button>
        </div>
  `,
  styles: ['']
})
export class TemplatePreComponent implements OnInit {
  @Input() template = new Template();
  btnTxt = '渲染';
  preview: boolean;
  gramar: TemplateKeyword[] = [];
  aKw: TemplateKeyword[] = [{ key: 'a', word: 'bstract' }, { key: 'a', word: 's' }, { key: 'a', word: 'dd' }
    , { key: 'a', word: 'lias' }, { key: 'a', word: 'sending' }, { key: 'a', word: 'sync' }, { key: 'a', word: 'wait' }];
  bKw: TemplateKeyword[] = [{ key: 'b', word: 'ase' }, { key: 'b', word: 'ool' },
  { key: 'b', word: 'reak' }, { key: 'b', word: 'yte' }, { key: 'b', word: 'y' }];
  cKw: TemplateKeyword[] = [{ key: 'c', word: 'lass' }, { key: 'c', word: 'ase' }, { key: 'c', word: 'atch' }, { key: 'c', word: 'har' },
  { key: 'c', word: 'hecked' }, { key: 'c', word: 'onst' }, { key: 'c', word: 'ontinue' }];
  dKw: TemplateKeyword[] = [{ key: 'd', word: 'ecimal' }, { key: 'd', word: 'efault' }, { key: 'd', word: 'elegate' },
  { key: 'd', word: 'o' }, { key: 'd', word: 'ouble' }, { key: 'd', word: 'escending' }, { key: 'd', word: 'ynamic' }];
  eKw: TemplateKeyword[] = [{ key: 'e', word: 'num' }, { key: 'e', word: 'vent' }, { key: 'e', word: 'xplicit' },
  { key: 'e', word: 'xtern' }, { key: 'e', word: 'quals' }];
  fKw: TemplateKeyword[] = [{ key: 'f', word: 'inally' }, { key: 'f', word: 'ixed' }, { key: 'f', word: 'loat' }, { key: 'f', word: 'or' }
    , { key: 'f', word: 'oreach' }, { key: 'f', word: 'rom' }];
  gKw: TemplateKeyword[] = [{ key: 'g', word: 'oto' }, { key: 'g', word: 'et' }, { key: 'g', word: 'lobal' }, { key: 'g', word: 'roup' }];
  iKw: TemplateKeyword[] = [{ key: 'i', word: 'f' }, { key: 'i', word: 'mplicit' }, { key: 'i', word: 'n' },
  { key: 'i', word: 'nt' }, { key: 'i', word: 'nterface' },
  { key: 'i', word: 'nternal' }, { key: 'i', word: 's' }, { key: 'i', word: 'nto' }, { key: 'i', word: 'oin' }];
  lKw: TemplateKeyword[] = [{ key: 'l', word: 'ock' }, { key: 'l', word: 'ong' }, { key: 'l', word: 'et' }];
  nKw: TemplateKeyword[] = [{ key: 'n', word: 'ameof' }];
  oKw: TemplateKeyword[] = [{ key: 'o', word: 'bject' }, { key: 'o', word: 'perator' }, { key: 'o', word: 'ut' }, { key: 'o', word: 'verride' },
  { key: 'o', word: 'n' }, { key: 'o', word: 'rderby' }];
  pKw: TemplateKeyword[] = [{ key: 'p', word: 'arams' }, { key: 'p', word: 'rivate' }, { key: 'p', word: 'rotected' },
  { key: 'p', word: 'ublic' }, { key: 'p', word: 'artial' }];
  rKw: TemplateKeyword[] = [{ key: 'r', word: 'eadonly' }, { key: 'r', word: 'ef' }, { key: 'r', word: 'emove' }];
  sKw: TemplateKeyword[] = [{ key: 's', word: 'byte' }, { key: 's', word: 'ealed' }, { key: 's', word: 'hort' },
  { key: 's', word: 'izeof' }, { key: 's', word: 'stackalloc' }, { key: 's', word: 'tatic' },
  { key: 's', word: 'tring' }, { key: 's', word: 'truct' },
  { key: 's', word: 'witch' }, { key: 's', word: 'elect' }, { key: 's', word: 'et' }];
  tKw: TemplateKeyword[] = [{ key: 't', word: 'his' }, { key: 't', word: 'ry' }, { key: 't', word: 'ypeof' }];
  uKw: TemplateKeyword[] = [{ key: 'u', word: 'int' }, { key: 'u', word: 'long' }, { key: 'u', word: 'nchecked' },
  { key: 'u', word: 'nsafe' }, { key: 'u', word: 'short' }, { key: 'u', word: 'sing' }];
  vKw: TemplateKeyword[] = [{ key: 'v', word: 'irtual' }, { key: 'v', word: 'oid' }, { key: 'v', word: 'olatile' },
  { key: 'v', word: 'alue' }, { key: 'v', word: 'ar' }];
  wKw: TemplateKeyword[] = [{ key: 'w', word: 'hile' }, { key: 'w', word: 'hen' }, { key: 'w', word: 'here' }];
  yKw: TemplateKeyword[] = [{ key: 'y', word: 'ield' }];
  nzPrefix: string[] = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'i', 'n', 'o', 'p', 'r', 's', 't', 'u', 'v', 'w', 'y', 'G', '@'];
  valueWith = (data: { key: string; word: string }) => data.word;
  constructor(private client: HttpClient, private message: NzMessageService) {
    if (!this.template) {
      this.template = new Template();
      this.template.templateContent = '';
    }
  }

  ngOnInit() {
    this.preview = false;
  }
  onSearchChange({ value, prefix }: MentionOnSearchTypes): void {
    switch (prefix) {
      case 'a':
        this.gramar = this.aKw;
        break;
      case 'b':
        this.gramar = this.bKw;
        break;
      case 'c':
        this.gramar = this.cKw;
        break;
      case 'd':
        this.gramar = this.dKw;
        break;
      case 'e':
        this.gramar = this.eKw;
        break;
      case 'f':
        this.gramar = this.fKw;
        break;
      case 'g':
        this.gramar = this.gKw;
        break;
      case 'G':
        this.gramar = [{ key: 'G', word: 'etNameSpaceUsing' }, { key: 'G', word: 'etUsing' },
        { key: 'G', word: 'etCSharpSummary' }, { key: 'G', word: 'etName' }
          , { key: 'G', word: 'etColumnName' }, { key: 'G', word: 'etBuilder' }];
        break;
      case 'i':
        this.gramar = this.iKw;
        break;
      case 'n':
        this.gramar = this.nKw;
        break;
      case 'o':
        this.gramar = this.oKw;
        break;
      case 'p':
        this.gramar = this.pKw;
        break;
      case 'r':
        this.gramar = this.rKw;
        break;
      case 's':
        this.gramar = this.sKw;
        break;
      case 't':
        this.gramar = this.tKw;
        break;
      case 'u':
        this.gramar = this.uKw;
        break;
      case 'v':
        this.gramar = this.vKw;
        break;
      case 'w':
        this.gramar = this.vKw;
        break;
      case 'y':
        this.gramar = this.vKw;
        break;
      case '@':
        this.gramar = [{ key: '@', word: 'model' }, { key: '@', word: 'using FreeSql.TemplateEngine' }, { key: '@', word: 'model FreeSql.TemplateEngine.BuildTask' }];
    }
  }
  render(): void {
    this.preview = !this.preview;
    if (this.preview) {
      this.btnTxt = '修改';
    } else {
      this.btnTxt = '渲染';
    }
  }
  update(): void {
    this.client.put<Template>(`/api/Template`, this.template).subscribe(res => {
      this.message.success(`成功修改模板`);
      this.template = res;
    }, err => {
      this.message.error(`修改失败`);
    });
  }
}

