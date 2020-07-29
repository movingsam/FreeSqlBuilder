import { Injectable } from '@angular/core';
import { CascaderWidget, SFSchemaEnumType } from '@delon/form';
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
  constructor(public client: _HttpClient) {}
  /**
   * 获取当前站点入口目录下的程序集
   */
  getAssemblies(): Observable<SelectItem[]> {
    return this.client.get<SelectItem[]>(`api/Assemblies`);
  }

  /**
   * 获取某个程序集下的所有基类
   * @param asemblyName 程序集名称
   */
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

  /**
   * 获取表结构
   * @param input 入参数据源/实体源
   */
  getTableInfo(input: DataSource | EntitySource): Observable<TableInfoDto[] | DbTableInfoDto[]> {
    if ((input as EntitySource).entityAssemblyName !== undefined) {
      const i = input as EntitySource;
      return this.client.get<TableInfoDto[]>(`api/AllTable?assemblyName=${i.entityAssemblyName}&entityBaseName=${i.entityBaseName}`);
    } else {
      return this.client.post<DbTableInfoDto[]>(`api/project/DbTableInfo`, input);
    }
  }
  /**
   * 默认配置检测
   */
  checkConfig(): Observable<boolean> {
    return this.client.get<boolean>(`api/check`);
  }
}
