import { HttpClient, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

/**
 * Http方法
 */
export enum HttpMethod {
    Get,
    Post,
    Put,
    Delete
}
export enum HttpContentType {
    /**
     * application/x-www-form-urlencoded
     */
    FormUrlEncoded,
    /**
     * application/json
     */
    Json
}
@Injectable()
export class HttpHelper {
    client: HttpClient;
    private headers: HttpHeaders;
    private parameters: HttpParams;
    private httpResponseType: HttpContentType = HttpContentType.Json;
    options = { headers: this.headers, params: this.parameters, responseType: this.httpResponseType };
    send<T>(url: string, httpMethod: HttpMethod, data?: any) {
        switch (httpMethod) {
            case HttpMethod.Get:
                return this.client.get<T>(url, data);
            case HttpMethod.Post:
                return this.client.post<T>(url, data);
            case HttpMethod.Put:
                return this.client.put<T>(url, data);
            case HttpMethod.Delete:
                this.param(data);
                return this.client.delete<T>(url);
        }
    }
    constructor(_client: HttpClient) {
        this.client = _client;
    }
    param(data: any, value?: string): void {
        if (typeof data === 'object') {
            this.paramByObject(data);
        }
        if (typeof data === 'string' && value) {
            this.parameters = this.parameters.append(data, value);
        }
    }
    private paramByObject(data: object) {
        for (const key in data) {
            if (data.hasOwnProperty(key)) {
                let value = data[key];
                if (value == null) {
                    value = '';
                }
                this.parameters = this.parameters.append(key, value);
            }
        }
    }

}

