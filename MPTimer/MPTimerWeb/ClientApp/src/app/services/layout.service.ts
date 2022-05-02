import { Injectable } from "@angular/core";
import { BehaviorSubject, fromEvent, } from "rxjs";
import { map, startWith } from "rxjs/operators";

@Injectable({ providedIn: 'root' })
export class LayoutService {
    public toggleMenu$ = new BehaviorSubject(false);
    public sizeMode$ = fromEvent(window, 'resize')
        .pipe(
            map(e => (e.target as any)?.innerWidth),
            startWith(document.body.clientWidth),
            map((a) => this.mapToSizeMode(a)),
        );

    public toggleMenu(): void {
        this.toggleMenu$.next(!this.toggleMenu$.value);
    }

    private mapToSizeMode(width: number): 'phone' | 'tablet' | 'desktop' {
       if (width < 640) {
           return 'phone';
       }
       if (width < 960) {
           return 'tablet';
       }

       return 'desktop';
    }
}