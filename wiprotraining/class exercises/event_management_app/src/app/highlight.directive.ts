import { Directive, ElementRef, Input, OnChanges, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appHighlight]',
  standalone : true
})
export class HighlightDirective implements OnChanges {

  @Input() appHighlight : number = 0;

  constructor(private el: ElementRef,private renderer: Renderer2) { }

  ngOnChanges() {
    if (this.appHighlight > 2000) {
      this.renderer.setStyle(this.el.nativeElement, 'background-color', '#fff7cc');
    } else {
      this.renderer.removeStyle(this.el.nativeElement, 'background-color');
    }
  }

}
