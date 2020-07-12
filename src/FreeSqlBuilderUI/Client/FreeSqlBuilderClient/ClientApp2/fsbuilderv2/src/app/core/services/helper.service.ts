import { Injectable } from '@angular/core';
import { SFSchemaEnumType } from '@delon/form';
import { _HttpClient } from '@delon/theme';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DbTableInfoDto, TableInfoDto } from './dtos/tableinfo';
import { DataSource, EntitySource } from './interface/project';
import { SelectItem } from './interface/selectItem';

@Injectable({
  providedIn: 'root',
})
export class HelperService {
  constructor(public client: _HttpClient) { }

  getAssemblies(): Observable<SFSchemaEnumType[]> {
    return this.client.get<SelectItem[]>(`api/Assemblies`).pipe(
      map((m) =>
        m.map<SFSchemaEnumType>((t) => {
          return {
            key: t.key,
            value: t.value,
            title: t.key,
            label: t.key,
          };
        }),
      ),
    );
  }
  getAbstractEntity(asemblyName: string): Observable<SFSchemaEnumType[]> {
    return this.client.get<SelectItem[]>(`api/BaseClass?entityAssemblyName=${asemblyName}`).pipe(
      map((m) =>
        m.map<SFSchemaEnumType>((d) => {
          return {
            key: d.key,
            value: d.value,
            label: d.key,
          };
        }),
      ),
    );
  }

  getTableInfo(input: DataSource | EntitySource): Observable<TableInfoDto[] | DbTableInfoDto[]> {

    if ((input as EntitySource).entityAssemblyName !== undefined) {
      const i = (input as EntitySource);
      return this.client.get<TableInfoDto[]>(`api/AllTable?assemblyName=${i.entityAssemblyName}&entityBaseName=${i.entityBaseName}`);
    } else {
      return this.client.post<DbTableInfoDto[]>(`api/project/DbTableInfo`, input);
    }
  }


  // checkDataSource();
}
