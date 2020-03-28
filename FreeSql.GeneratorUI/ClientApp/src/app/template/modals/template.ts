import { BuilderOptions } from 'src/app/project/modals/project';

export class Template {
    id = 0;
    projects = new Array<BuilderOptions>();
    templateName = '';
    templatePath = '';
    templateContent = '';
}

export class RootPath {
    root = '';
}
