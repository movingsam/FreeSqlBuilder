import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { HttpRequest } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { mergeMap, catchError } from 'rxjs/operators';
import { NzMessageService } from 'ng-zorro-antd';


@Injectable()
export class CommonInterceptor implements HttpInterceptor {
    constructor(private message: NzMessageService) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        console.log(req.url, '拦截器');
        if (!req.url.startsWith(location.host)) {
            const clonedRequest = req.clone({
                url: location.host + req.url
            });
            console.log('new headers', clonedRequest.url);
            return next.handle(clonedRequest).pipe(mergeMap((event: any) => {
                // 正常返回，处理具体返回参数
                if (event instanceof HttpResponse && event.status === 200) {
                    return this.handleData(event);
                }// 具体处理请求返回数据
                return of(event);
            }), catchError((err: HttpErrorResponse) => this.handleData(err)));
        }
        return next.handle(req)
            .pipe(mergeMap((event: any) => {
                // 正常返回，处理具体返回参数
                if (event instanceof HttpResponse && event.status === 200) {
                    return this.handleData(event);
                }// 具体处理请求返回数据
                return of(event);
            }), catchError((err: HttpErrorResponse) => this.handleData(err)));
    }
    private handleData(
        event: HttpResponse<any> | HttpErrorResponse,
    ): Observable<any> {
        let errorMsg = '';
        // 业务处理：一些通用操作
        switch (event.status) {
            case 200:
                break;
            case 401:
                errorMsg = '没有权限';
                break;
            case 404:
                errorMsg = '找不到相关接口';
                break;
            case 500:
                errorMsg = '服务器异常';
                break;
            default:
                return;
        }
        if (errorMsg !== '') {
            this.message.error(errorMsg);
        }
    }
}
