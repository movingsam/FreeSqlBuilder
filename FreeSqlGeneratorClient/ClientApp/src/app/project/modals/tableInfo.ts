
export class TableInfo {
    isServiceTable: boolean;
    nameSpace: string;
    importUsings: string[];
    primaryTypeName: string;
    comment: string;
    name: string;
    dbTableName: string;
    columnInfos: ColumnInfo[];
    navigateInfos: NavigateColumnInfo[];
}

export class ColumnInfo {
    columnName: string;
    comment: string;
    dbColumnName: string;
    columnAttribute: ColumnAttribute;
    type: any;
    csType: string;
}

export class NavigateColumnInfo extends ColumnInfo {
    navigateTableInfo: TableInfo;
    navigateCategory: NavigateCategory;
}


export enum NavigateCategory {
    OneToOne,
    OneToMany,
    None
}
export class ColumnAttribute {
    name: string;
    oldName: string;
    dbType: string;
    isPrimary: boolean;
    isIdentity: boolean;
    isNullable: boolean;
    isIgnore: boolean;
    isVersion: boolean;
    mapType: any;
    position: number;
    canInsert: boolean;
    canUpdate: boolean;
    stringLength: number;
    insertValueSql: string;
}

