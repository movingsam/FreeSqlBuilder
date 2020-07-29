import { SFSchemaEnum } from '@delon/form';

export class SelectItem implements SFSchemaEnum {
  constructor(
    public key: string = '',
    public value: any = null,
    public title: string,
    public label: string,
    public disabled: boolean = false,
    public chidren: any = null,
    public isLeft: boolean = false,
  ) {}
}
