import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalHelper } from '@delon/theme';
import { EntitysourceComponent } from '../entitysource/entitysource.component';
import { DatasourceComponent } from '../datasource/datasource.component';
import { EntitySource } from 'src/app/core/services/interface/project';

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
    for (const i in this.formGroup.controls) {
      this.formGroup.controls[i].markAsDirty();
      this.formGroup.controls[i].updateValueAndValidity();
    }
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
              nzOnOk: () => {},
              nzCancelDisabled: true,
              nzClosable: false,
              nzMaskClosable: false,
            },
          },
        )
        .subscribe((r) => {});
    }
  }
}
