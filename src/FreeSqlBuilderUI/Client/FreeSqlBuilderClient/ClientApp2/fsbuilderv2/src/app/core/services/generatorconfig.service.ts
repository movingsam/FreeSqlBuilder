import { Injectable } from '@angular/core';
import { SFSchemaEnum, SFSchemaEnumType } from '@delon/form';
import { _HttpClient } from '@delon/theme';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Page, PageView } from './interface/dto';
import { DataSource, EntitySource, GeneratorModeConfig } from './interface/project';

@Injectable()
export class GeneratorconfigService {
  constructor(public client: _HttpClient) { }

  /**
   * 获取配置分页列表
   * @param page 分页
   */
  getGeneratorConfigList(page: Page): Observable<PageView<GeneratorModeConfig>> {
    return this.client.get<PageView<GeneratorModeConfig>>(`api/config?pageNumber=${page.pageNumber}&pageSize=${page.pageSize}`);
  }
  /**
   * 获取配置项
   */
  getGeneratorConfigSelect(): Observable<SFSchemaEnumType[]> {
    return this.client.get<PageView<GeneratorModeConfig>>(`api/config?pageNumber=1&pageSize=100`).pipe(
      map((page) =>
        page.datas.map<SFSchemaEnumType>((d) => {
          return {
            label: d.name,
            title: d.name,
            value: d.id,
          };
        }),
      ),
    );
  }

  /**
   * 获取配置详情
   * @param id 主键
   */
  getGeneratorConfig(id: number): Observable<GeneratorModeConfig> {
    return this.client.get<GeneratorModeConfig>(`api/config/${id}`);
  }

  /**
   * 新增生成器配置项
   * @param input 新增对象
   */
  createGeneratorConfig(input: GeneratorModeConfig): Observable<GeneratorModeConfig> {
    return this.client.post<GeneratorModeConfig>(`api/config`, input);
  }
  /**
   * 更新生成器配置
   * @param input 更新对象
   */
  updateGeneratorConfig(input: GeneratorModeConfig): Observable<GeneratorModeConfig> {
    return this.client.put<GeneratorModeConfig>(`api/config`, input);
  }
  /**
   * 获取数据源分页
   * @param page 分页信息
   */
  getDataSourceList(page: Page): Observable<PageView<DataSource>> {
    return this.client.get<PageView<DataSource>>(`api/config/datasource?pageNumber=${page.pageNumber}&pageSize=${page.pageSize}`);
  }
  /**
   * 获取数据源选项
   */
  getDataSourceSelect(): Observable<SFSchemaEnum[]> {
    return this.client.get<PageView<DataSource>>(`api/config/datasource?pageNumber=1&pageSize=100`).pipe(
      map((m) =>
        m.datas.map<SFSchemaEnum>((t) => {
          return {
            label: t.name,
            value: t.id,
            key: t.id,
          };
        }),
      ),
    );
  }
  /**
   * 检测数据源是否可以链接
   * @param ds 数据源
   */
  checkConnectioon(ds: DataSource): Observable<boolean> {
    return this.client.post<boolean>(`api/config/DataSource/Check`, ds);
  }

  /**
   * 获取数据源
   * @param id 主键
   */
  getDataSource(id: number): Observable<DataSource> {
    return this.client.get<DataSource>(`api/config/datasource/${id}`);
  }


  /**
   *  编辑数据源
   * @param input 编辑对象
   */
  updateDataSource(input: DataSource): Observable<DataSource> {
    return this.client.put<DataSource>(`api/config/datasource`, input);
  }

  /**
   *  新增数据源
   * @param ds 数据源
   */
  createDataSource(ds: DataSource): Observable<DataSource> {
    return this.client.post<DataSource>(`api/config/datasource`, ds);
  }
  /**
   * 获取实体源选项
   */
  getEntitySourceSelect(): Observable<SFSchemaEnum[]> {
    return this.client.get<PageView<EntitySource>>(`api/config/entitySource?pageNumber=1&pageSize=100`).pipe(
      map((m) =>
        m.datas.map<SFSchemaEnum>((t) => {
          return {
            label: t.name,
            value: t.id,
            key: t.id,
          };
        }),
      ),
    );
  }
  /**
   * 获取实体源
   * @param id 实体源id
   */
  getEntitySource(id: number): Observable<EntitySource> {
    return this.client.get<EntitySource>(`api/config/entitySource/${id}`);
  }
  /**
   * 修改实体源
   * @param input 修改的实体源
   */
  updateEntitySource(input: EntitySource): Observable<EntitySource> {
    return this.client.put<EntitySource>(`api/config/EntitySource`, input);
  }
  /**
   * 删除实体源
   * @param id 实体源Id
   */
  delEntitySource(id: number): Observable<boolean> {
    return this.client.delete<boolean>(`api/config/EntitySource/${id}`);
  }

  /**
   * 新增一个实体源
   * @param es 实体源
   */
  createEntitySource(es: EntitySource): Observable<EntitySource> {
    return this.client.post<EntitySource>(`api/config/entitySource`, es);
  }
  /**
   * 配置删除
   * @param id 配置id
   */
  delConfig(id: number): Observable<boolean> {
    return this.client.delete<boolean>(`api/config/${id}`);
  }
  /**
   * 删除数据源
   * @param id 数据源ID
   */
  delDataSouce(id: number): Observable<boolean> {
    return this.client.delete<boolean>(`api/config/dataSource/${id}`);
  }

}
