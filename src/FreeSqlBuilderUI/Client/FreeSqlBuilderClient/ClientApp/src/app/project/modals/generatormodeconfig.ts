
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
    Odbc = 9,
    OdbcDameng = 10,
    MsAccess = 11
}
export class DataSource {
    constructor(public id = 0, public name = ``, public dbType = DataType.SqlServer, public connectionString = ``
    ) {

    }
}

export enum PickType {
    Pick, Ignore
}
export class GeneratorModeConfig {
    constructor(projectid: number) {
        this.projectId = projectid;
        this.generatorMode = 1;
        this.id = 0;
        this.entityAssemblyName = '';
        this.entityBaseName = '';
        this.ignoreTables = '';
        this.includeTables = '';
        this.pickType = PickType.Ignore;
    }
    id: number;
    projectId: number;
    generatorMode: GeneratorMode = 1;
    dataSource: DataSource = new DataSource();
    entityAssemblyName: string;
    dataSourceId: number;
    includeTables: string;
    ignoreTables: string;
    entityBaseName: string;
    pickType: PickType = PickType.Ignore;
}
export class TableInfo {
    isServiceTable: boolean;
    nameSpace: string;
    importUsings: string[];
    primaryTypeName: string;
    comment: string;
    name: string;
    dbTableName: string;
    columnInfos: ColumnInfo[] = new Array<ColumnInfo>();
    navigateInfos: NavigateColumnInfo[] = new Array<NavigateColumnInfo>();
}

export class ColumnInfo {
    columnName: string;
    comment: string;
    dbColumnName: string;
    dbType: string;
    csType: string;
}

// tslint:disable-next-line: no-use-before-declare
export class NavigateColumnInfo extends ColumnInfo {
    dbColumnName: string;
    navigateTableInfo: TableInfo;
    navigateCategory: NavigateCategory;
}


export enum NavigateCategory {
    OneToOne,
    OneToMany,
    None
}

export enum GeneratorMode {
    DbFirst,
    CodeFirst
}

