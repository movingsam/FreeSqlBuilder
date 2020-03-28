import { BuilderOptions } from 'src/app/project/modals/project';

export class TemplateKeyword {
    key: string;
    word: string;
}


export interface Dir {
    key: string;
    value: string;
    title: string;
    children: Dir[];
}
export class ImportData {
    fileContent: string;

}



