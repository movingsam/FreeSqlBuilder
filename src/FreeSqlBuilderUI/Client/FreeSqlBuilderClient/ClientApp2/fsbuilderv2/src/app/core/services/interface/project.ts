export class Project {
  id: number;
  projectInfoId: number;
  projectInfo: ProjectInfo;
  generatorModeConfigId: number;
  generatorModeConfig: GeneratorModeConfig;
  builders: BuilderOptions[];
  buildersId: number[];
  _buildersId: number[];
  globalBuilders: BuilderOptions[];
  globalBuildersId: number[];
  _globalBuildersId: number[];
}

export class ProjectInfo {
  id: number;
  projectName: string;
  rootPath: string;
  outPutPath: string;
  author: string;
}

export class GeneratorModeConfig {
  id = 0;
  name = ``;
  generatorMode: GeneratorMode = GeneratorMode.DbFirst;
  dataSourceId = 0;
  dataSource: DataSource = new DataSource();
  entitySourceId = 0;
  entitySource: EntitySource = new EntitySource();
  pickType: PickType = PickType.Ignore;
  projects: Project[];
  includeTables = ``;
  ignoreTables = ``;
}

export enum PickType {
  Pick,
  Ignore,
}
export enum GeneratorMode {
  DbFirst,
  CodeFirst,
}
export class EntitySource {
  id: number;
  name = '';
  entityAssemblyName = '';
  entityBaseName = '';
}

export class DataSource {
  id = 0;
  name = '';
  dbType: DataType = DataType.SqlServer;
  connectionString = '';
  generatorModeConfigs: GeneratorModeConfig[];
}

export enum DataType {
  MySql,
  SqlServer,
  PostgreSQL,
  Oracle,
  Sqlite,
  OdbcOracle,
  OdbcSqlServer,
  OdbcMySql,
  OdbcPostgreSQL,
  Odbc,
  OdbcDameng,
  MsAccess,
  Dameng,
  OdbcKingbaseES,
  ShenTong,
}

export class BuilderOptions {
  id: number;
  strExtensionOptions: string;
  name: string;
  prefix: string;
  outPutPath: string;
  mode: ConvertMode;
  template: Template;
  templateId: number;
  suffix: string;
  type: BuilderType;
  fileExtensions: string;
  projects: Project[];
}

export enum BuilderType {
  Builder,
  GlobalBuilder,
}

export enum ConvertMode {
  None,
  AllLower,
  AllUpper,
  FirstUpper,
}

export class Template {
  id: number;
  templateName: string;
  templatePath: string;
  templateContent: string;
  fileExtension: string;
}
