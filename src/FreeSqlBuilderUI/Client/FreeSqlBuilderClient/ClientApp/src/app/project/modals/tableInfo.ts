
export class TableInfo {
    columnsByCsIgnore = new Map<string, ColumnInfo>().set('', new ColumnInfo());
    dbOldName = '';
    dbName = '';
    csName = '';
    indexes: IndexInfo[];
    primarys = new Array<ColumnInfo>();
    columnsByPosition = new Array<ColumnInfo>();
    versionColumn = new ColumnInfo();
    columnsByCs = new Map<string, ColumnInfo>().set('', new ColumnInfo());
    columns = new Map<string, ColumnInfo>().set('', new ColumnInfo());
    disableSyncStructure = false;
    isIgnore = false;
    isPick = false;

}

export class ColumnInfo {
    table = new TableInfo();
    csName = '';
    attribute: ColumnAttribute;
    comment = '';
    dbTypeText = '';
    dbDefaultValue = '';
    dbInsertValue = '';
    dbUpdateValue = '';
    dbSize = 0;
    dbPrecision = 0;
    dbScale = 0;
}

export class IndexInfo {
    name = '';
    columns: IndexColumnInfo[];
    isUnique = false;
}

export class IndexColumnInfo {
    column: ColumnInfo;
    isDesc = false;
}

export class ColumnAttribute {
    name = '';
    oldName = '';
    dbType = '';
    isPrimary = false;
    isIdentity = false;
    isNullable = false;
    isIgnore = false;
    isVersion = false;
    position = 0;
    canInsert = false;
    canUpdate = false;
    serverTime: Date;
    stringLength = 0;
    insertValueSql = '';
}


