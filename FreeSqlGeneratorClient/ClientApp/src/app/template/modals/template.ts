import { BuilderOptions } from 'src/app/project/modals/project';

export class Template {
    constructor() {
        this.id = 0;
        this.templateName = '';
        this.templatePath = '';
        this.templateContent = '';
    }
    id = 0;
    projects = new Array<BuilderOptions>();
    templateName = '';
    templatePath = '';
    templateContent = '';
}

export class RootPath {
    root = '';
}
