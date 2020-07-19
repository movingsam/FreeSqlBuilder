import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Page, PageView } from './interface/dto';
import { Template } from './interface/project';
import { SelectItem } from './interface/selectItem';

@Injectable({
  providedIn: 'root'
})
export class TemplateService {

  constructor(private client: _HttpClient) { }
  /**
   * 获取模板分页数据
   * @param page 分页信息
   */
  getTemplatePage(page: Page): Observable<PageView<Template>> {
    return this.client.get<PageView<Template>>(`api/template/Page?pageNumber=${page.pageNumber}&pageSize=${page.pageSize}`);
  }
  /**
   * 获取模板选项
   */
  getTemplateSelect(): Observable<SelectItem[]> {
    return this.client.get<PageView<Template>>(`api/template/Page?pageNumber=1&pageSize=100`).pipe(map(m => m.datas.map<SelectItem>(d =>
      new SelectItem(d.id.toString(), d.id, d.templateName, d.templateName))));
  }
}
