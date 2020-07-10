import { Injectable } from '@angular/core';
import { SFSchemaEnumType } from '@delon/form';
import { _HttpClient } from '@delon/theme';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SelectItem } from './interface/selectItem';

@Injectable({
  providedIn: 'root',
})
export class HelperService {
  constructor(public client: _HttpClient) {}

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
  getAbstractEntity(asemblyName: Observable<string>): Observable<SFSchemaEnumType[]> {
    asemblyName.subscribe((t) => {
      return this.client.get<SelectItem[]>(`api/BaseClass/${asemblyName}`).pipe(
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
    });
    return null;
  }

  // checkDataSource();
}
