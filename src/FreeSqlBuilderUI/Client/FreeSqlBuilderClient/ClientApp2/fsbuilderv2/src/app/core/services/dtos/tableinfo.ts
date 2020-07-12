export interface TableInfoDto {
    disableSyncStructure: boolean;
    dbOldName: string;
    dbName: string;
    csName: string;
    primarys: ColumnInfoDto[];
    columnsByCs: ColumnInfoDto[];
    comment: string;
}

export interface ColumnInfoDto {
    csName: string;
    comment: string;
    dbTypeText: string;
    dbDefaultValue: string;
    dbInsertValue: string;
    dbUpdateValue: string;
    dbSize: number;
    dbPrecision: number;
    dbScale: number;
}

export interface IndexInfoDto {
    name: string;
    isUnique: boolean;
}



export interface DbTableInfoDto {
    id: string;
    schema: string;
    name: string;
    comment: string;
    type: DbTableType;
    columns: DbColumnInfoDto[];
    primarys: DbColumnInfoDto[];
    foreignsDict: { [key: string]: DbForeignInfoDto; };
    foreigns: DbForeignInfoDto[];
}

export interface DbColumnInfoDto {
    name: string;
    dbTypeTextFull: string;
    maxLength: number;
    isPrimary: boolean;
    isIdentity: boolean;
    isNullable: boolean;
    coment: string;
    defaultValue: string;
}

export interface DbForeignInfoDto {
    table: DbTableInfoDto;
    referencedTable: DbTableInfoDto;
}

export enum DbTableType {
    TABLE,
    VIEW,
    StoreProcedure
}
