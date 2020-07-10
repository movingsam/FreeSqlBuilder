import { SFSchemaEnum } from '@delon/form';

export class SelectItem implements SFSchemaEnum {
  constructor(public key: string = '', public value: any = null, public title: string, public disabled: boolean = false) {}
}
