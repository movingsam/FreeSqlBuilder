import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { promise } from 'protractor';
import { Observable } from 'rxjs';
import { Page, PageView, Result } from './interface/dto';
import { Project } from './interface/project';

/**
 * 项目服务
 */
@Injectable()
export class ProjectService {
  constructor(public client: _HttpClient) {}

  /**
   * 获取项目分页
   * @param page 分页对象
   */
  getlist(page: Page): Observable<PageView<Project>> {
    return this.client.get<PageView<Project>>(`api/project/page?pageNumber=${page.pageNumber}&pageSize=${page.pageSize}`);
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
