import { ProjectInfo } from './projectInfo';
import { GeneratorModeConfig } from './generatormodeconfig';
import { Template } from 'src/app/template/modals/template';

export class Project {
    id: number;
    projectInfo: ProjectInfo;
    projectInfoId: number;
    generatorModeConfig: GeneratorModeConfig = new GeneratorModeConfig(this.projectInfoId);
    generatorModeConfigId: number;
    builders: BuilderOptions[] = new Array<BuilderOptions>();
    globalBuilders: BuilderOptions[];
}
export class BuilderOptions {
    constructor(_type: BuilderType, _name: string) {
        this.id = 0;
        this.projectId = 0;
        this.splitDot = '_';
        this.suffix = '';
        this.templateId = 0;
        this.type = _type;
        this.isServiceOnly = false;
        this.mode = 0;
        this.name = _name;
        this.outPutPath = '';
        this.fileExtensions = 'cs';
        this.classBase = '';
        this.prefix = '';
        this.isIgnorePrefix = true;
    }
    id: number;
    projectId: number;
    project: Project;
    classBase: string;
    name: string;
    isServiceOnly: boolean;
    prefix: string;
    splitDot: string;
    isIgnorePrefix: boolean;
    outPutPath: string;
    mode: ConvertMode;
    template: Template;
    templateId: number;
    suffix: string;
    type: BuilderType;
    fileExtensions: string;
}

export enum BuilderType {
    Builder,
    GlobalBuilder
}

export enum ConvertMode {
    None,
    AllLower,
    AllUpper,
    FirstUpper
}
