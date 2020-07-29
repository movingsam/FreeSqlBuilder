import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { ModalHelper, TitleService, VERSION as VERSION_ALAIN } from '@delon/theme';
import { NzModalService } from 'ng-zorro-antd/modal';
import { VERSION as VERSION_ZORRO } from 'ng-zorro-antd/version';
import { filter } from 'rxjs/operators';
import { HelperService } from './core/services/helper.service';
import { DefaultinitComponent } from './shared/component/defaultinit/defaultinit.component';

@Component({
  selector: 'app-root',
  template: ` <router-outlet></router-outlet> `,
})
export class AppComponent implements OnInit {
  constructor(
    el: ElementRef,
    renderer: Renderer2,
    private router: Router,
    private titleSrv: TitleService,
    private modalSrv: NzModalService,
    private modal: ModalHelper,
    private helperSrv: HelperService,
  ) {
    renderer.setAttribute(el.nativeElement, 'ng-alain-version', VERSION_ALAIN.full);
    renderer.setAttribute(el.nativeElement, 'ng-zorro-version', VERSION_ZORRO.full);
  }

  ngOnInit() {
    this.router.events.pipe(filter((evt) => evt instanceof NavigationEnd)).subscribe(() => {
      this.titleSrv.setTitle();
      this.modalSrv.closeAll();
      this.helperSrv.checkConfig().subscribe((res) => {
        console.log(res, `check`);
        if (!res) {
          this.modal
            .create(DefaultinitComponent, null, {
              modalOptions: {
                nzMaskClosable: false,
              },
            })
            .subscribe((t) => console.log(t));
        }
      });
    });
  }
}
