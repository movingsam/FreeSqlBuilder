export class ProjectViewmodal {
    constructor(public projectName: string = '', public rootPath: string = '', public outPutPath: string = ''
        , public generatorMode: GeneratorMode = 0, public entityBaseName: string = '', public dataSource: DataSource = new DataSource(),
        public author: string = '', public entity: Entity = new Entity(),
        public includeTable: string[] = [], public builders: Array<BuilderOptions> = new Array(),
        public globalBuilders: Array<BuilderOptions> = new Array()) {

    }
}

export enum GeneratorMode {
    DbFirst = 0, CodeFirst = 1
}

export class DataSource {
    constructor(public Name: string = '', public DbType: DataType = 1, public ConnectionString: string = '') { }

}

export enum DataType {

    MySql = 0,
    SqlServer = 1,
    PostgreSQL = 2,
    Oracle = 3,
    Sqlite = 4,
    OdbcOracle = 5,
    OdbcSqlServer = 6,
    OdbcMySql = 7,
    OdbcPostgreSQL = 8,
    //
    // 摘要:
    //     通用的 Odbc 实现，只能做基本的 Crud 操作
    //     不支持实体结构迁移、不支持分页（只能 Take 查询）
    //     通用实现为了让用户自己适配更多的数据库，比如连接 mssql 2000、db2 等数据库
    //     默认适配 SqlServer，可以继承后重新适配 FreeSql.Odbc.Default.OdbcAdapter，最好去看下代码
    //     适配新的 OdbcAdapter，请在 FreeSqlBuilder.Build 之后调用 IFreeSql.SetOdbcAdapter 方法设置
    Odbc = 9,
    //
    // 摘要:
    //     武汉达梦数据库有限公司
    OdbcDameng = 10,
    //
    // 摘要:
    //     Microsoft Office Access 是由微软发布的关联式数据库管理系统
    MsAccess = 11
}

export class BuilderOptions {
    constructor(public classBase: string = '', public name: string = '',
        public isServiceOnly: boolean = false, public prefix: string = '', public splitDot: string = '_'
        , public isIgnorePrefix: boolean = true, public outPutPath: string = '', public mode: ConvertMode = 0,
        public templatePath: string = '', public suffix: string = '') {

    }
}
export enum ConvertMode {
    None,
    AllLower,
    AllUpper,
    FirstUpper
}

export interface Entity {
    templatepath: string;
    column: Column;
    outputpath: string;
}
export class Entity extends BuilderOptions implements Entity {

    constructor(public templatepath: string = '', public column: Column = new Column(), public outputpath: string = '') {
        super();
    }
}
export interface Column {
    Prefix: string;
    Mode: ConvertMode;
    Suffix: string;
}

export class Column implements Column {
    constructor(public prefix: string = '', public mode: ConvertMode = 1, public puffix: string = '') {

    }
}

