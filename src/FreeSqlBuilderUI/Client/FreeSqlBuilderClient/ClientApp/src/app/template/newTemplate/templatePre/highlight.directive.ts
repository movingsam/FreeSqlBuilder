import { Directive, ElementRef, Input, AfterViewInit } from '@angular/core';
import * as hljs from 'highlight.js';
import * as hljsDefineCshtmlRazor from 'highlightjs-cshtml-razor';

@Directive({
  selector: '[fg-highlight]'
})
export class HighlightDirective implements AfterViewInit {
  constructor(private elRef: ElementRef) {
    hljsDefineCshtmlRazor(hljs);
    hljs.initHighlightingOnLoad();
  }

  ngAfterViewInit() {
    hljs.highlightBlock(this.elRef.nativeElement);
  }
}
