import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SFComponent } from '@delon/form';
import { ModalHelper } from '@delon/theme';
import { NzModalService } from 'ng-zorro-antd/modal';
import { HelperService } from 'src/app/core/services/helper.service';
import { EntitySource } from 'src/app/core/services/interface/project';
import { ProjectService } from 'src/app/core/services/project.service';
import { DatasourceComponent } from '../datasource/datasource.component';
import { EntitysourceComponent } from '../entitysource/entitysource.component';

@Component({
  selector: 'fb-defaultinit',
  templateUrl: './defaultinit.component.html',
  styles: [],
})
export class DefaultinitComponent implements OnInit {
  formGroup: FormGroup;
  constructor(private fb: FormBuilder, private modal: NzModalService, private service: HelperService) {}
  entitySource: EntitySource = new EntitySource();
  @ViewChild('es', { static: true }) es: TemplateRef<{}>;
  ngOnInit() {
    this.formGroup = this.fb.group({
      defaultSource: [null, [Validators.required]],
    });
  }
  validate() {
    // for (const i in this.formGroup.controls) {
    //   this.formGroup.controls[i].markAsDirty();
    //   this.formGroup.controls[i].updateValueAndValidity();
    // }
  }
  submit() {
    this.validate();
    if (this.formGroup.valid) {
      this.modal.create({
        nzContent: this.es,
        nzWidth: '80vw',
        nzStyle: {
          top: '35vh',
        },
        nzBodyStyle: {
          'overflow-y': 'scroll',
          'max-height': '70vh',
        },
        nzOnOk: () => {
          this.service.initDefault(this.entitySource).subscribe((r) => console.log(r));
        },
        nzCancelDisabled: true,
        nzMaskClosable: false,
      });
    }
  }

  public Change(value): void {
    console.log(`change`, value);
  }
}
