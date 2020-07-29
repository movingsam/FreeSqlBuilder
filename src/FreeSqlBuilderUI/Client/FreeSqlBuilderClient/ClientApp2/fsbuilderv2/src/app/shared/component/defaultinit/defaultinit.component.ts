import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalHelper } from '@delon/theme';
import { EntitySource } from 'src/app/core/services/interface/project';
import { DatasourceComponent } from '../datasource/datasource.component';
import { EntitysourceComponent } from '../entitysource/entitysource.component';

@Component({
  selector: 'fb-defaultinit',
  templateUrl: './defaultinit.component.html',
  styles: [],
})
export class DefaultinitComponent implements OnInit {
  formGroup: FormGroup;
  constructor(private fb: FormBuilder, private modal: ModalHelper) {}
  entitySource: EntitySource = new EntitySource();
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
      this.modal
        .createStatic(
          EntitysourceComponent,
          {
            entitySource: this.entitySource,
            isDefault: true,
          },
          {
            modalOptions: {
              nzWidth: '80vw',
              nzStyle: {
                top: '35vh',
              },
              nzBodyStyle: {
                'overflow-y': 'scroll',
                'max-height': '70vh',
              },
              nzFooter: [
                {
                  label: '取消',
                  type: 'default',
                  onClick: () => {},
                },
                {
                  label: '确认',
                  type: 'primary',
                  onClick: () => {},
                },
              ],
              nzOnOk: () => {},
              nzCancelDisabled: true,
              nzMaskClosable: false,
            },
          },
        )
        .subscribe((r) => {});
    }
  }
}
