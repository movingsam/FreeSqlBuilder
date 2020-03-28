import { Component, OnInit, Input } from '@angular/core';
import { BuilderOptions } from '../../modals/project';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-builder-option',
  templateUrl: './builderOption.component.html',
  styleUrls: ['./builderOption.component.css']
})
export class BuilderOptionComponent implements OnInit {
  @Input() builder: BuilderOptions = new BuilderOptions();
  validateForm: FormGroup;
  constructor(protected fb: FormBuilder) { }
  submitForm(): void {
    Object.getOwnPropertyNames(this.builder).forEach(element => {
      this.builder[element] = this.validateForm.get(element).value;
    });
    for (const i in this.validateForm.controls) {
      if (this.validateForm.controls.hasOwnProperty(i)) {
        this.validateForm.controls[i].markAsDirty();
        this.validateForm.controls[i].updateValueAndValidity();
      }
    }
  }
  ngOnInit() {
    const modals = {};
    Object.getOwnPropertyNames(this.builder).forEach(element => {
      modals[element] = new FormControl(this.builder[element], []);
    });
    this.validateForm = this.fb.group(modals);
    console.log(this.validateForm, 'modal');
  }

}
