import { Injectable } from "@angular/core";
import { GuidUtils, ToastType } from "@app/shared";
import { Message } from "primeng/api";
import { Subject } from "rxjs";

@Injectable()
export class ToastService {
    public showToast$ = new Subject<Message>();

    public success(text: string, title?: string, sticky: boolean = false, life: number = 3000): void {
        this.show(ToastType.Success, text, title, sticky, life);
    }

    public info(text: string, title?: string, sticky: boolean = false, life: number = 3000): void {
        this.show(ToastType.Info, text, title, sticky, life);
    }

    public warn(text: string, title?: string, sticky: boolean = false, life: number = 3000): void {
        this.show(ToastType.Warn, text, title, sticky, life);
    }

    public error(text: string, title?: string, sticky: boolean = false, life: number = 3000): void {
        this.show(ToastType.Error, text, title, sticky, life);
    }

    public show(type: ToastType, text: string, title?: string, sticky: boolean = false, life: number = 3000): void {
        this.showToast$.next({
            key: 'standard',
            severity: type,
            summary: title,
            detail: text,
            life: life ?? 3000,
            sticky: sticky ?? false,
            id: GuidUtils.newGuid(),
        });
    }
}