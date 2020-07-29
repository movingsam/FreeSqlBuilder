import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { promise } from 'protractor';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Page, PageView, Result } from './interface/dto';
import { Project } from './interface/project';
import { SelectItem } from './interface/selectItem';

/**
 * 项目服务
 */
@Injectable()
export class ProjectService {
  constructor(public client: _HttpClient) { }
  /**
   * 项目生成
   * @param id 项目主键 
   */
  buildTask(id: number): Observable<boolean> {
    return this.client.post<boolean>(`api/project/task/build/${id}`);
  }
  /**
   * 构建器快速生成
   * @param id 构建器主键
   */
  buildTempTask(id: number): Observable<boolean> {
    return this.client.post<boolean>(`api/project/task/temp/build/${id}`);
  }

  /**
   * 获取项目分页
   * @param page 分页对象
   */
  getlist(page: Page): Observable<PageView<Project>> {
    return this.client.get<PageView<Project>>(`api/project/page?pageNumber=${page.pageNumber}&pageSize=${page.pageSize}`);
  }
  /**
   *  获取选择列表
   */
  getSelect(): Observable<SelectItem[]> {
    return this.client.get<PageView<Project>>(`api/project/page?pageNumber=1&pageSize=100`).pipe(map(m => m.datas.map<SelectItem>(d => {
      return new SelectItem(d.id.toString(), d.id, d.projectInfo.nameSpace, d.projectInfo.nameSpace);
    })));
  }

  /**
   * 获取项目
   * @param id 主键
   */
  getProject(id: number): Observable<Project> {
    return this.client.get<Project>(`api/project/${id}`);
  }
  /**
   * 更新项目
   * @param id 主键
   * @param project 项目实体
   */
  updateProject(project: Project): Observable<boolean> {
    return this.client.put<boolean>(`api/project`, project);
  }

  /**
   * 创建项目
   * @param project 项目对象
   */
  createProject(project: Project): Observable<boolean> {
    return this.client.post<boolean>(`api/project`, project);
  }

  /**
   * 删除项目
   * @param id 主键
   */
  deleteProject(id: number): Observable<boolean> {
    return this.client.delete<boolean>(`api/project/${id}`);
  }
}
