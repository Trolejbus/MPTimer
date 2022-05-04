import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { SignalRService } from "@app/services";
import { DateUtils } from "@app/shared";
import { EntityCollectionServiceBase, EntityCollectionServiceElementsFactory } from "@ngrx/data";
import { environment } from "../../../../environments/environment";
import { combineLatest, Observable } from "rxjs";
import { map, startWith, switchMap } from "rxjs/operators";
import { SourceControlDto } from "../models";

@Injectable({ providedIn: 'root' })
export class SourceControlService extends EntityCollectionServiceBase<SourceControlDto> {

    public sourceControls$ = combineLatest([
        this.signalRService.on$("SourceControlBranchChanged").pipe(startWith(null)),
    ]).pipe(
        switchMap(() => this.getAll().pipe(map(control => control.map(c => this.parseDates(c))))),
    );

    constructor(
        private httpClient: HttpClient,
        private signalRService: SignalRService,
        serviceElementsFactory: EntityCollectionServiceElementsFactory,
    ) {
        super('SourceControl', serviceElementsFactory);
    }

    public get(date: Date): Observable<SourceControlDto[]> {
        const filter = this.getFilterParams(date);
        return this.httpClient.get<SourceControlDto[]>(`${environment.backendUrl}/api/sourceControls`, { params: filter }).pipe(
            map(result => result.map(r => this.parseDates(r))),
        );
    }

    private getFilterParams(date: Date): HttpParams {
        const from = new Date(new Date(date.getFullYear(), date.getMonth(), date.getDate()).getTime() + 1000 * 60 * 60 * 24);
        const to = new Date(new Date(date.getFullYear(), date.getMonth(), date.getDate()).getTime() + 2 * 1000 * 60 * 60 * 24);
        let params = new HttpParams();
        params = params.append('from', from.toISOString());
        params = params.append('to', to.toISOString());
        return params;
    }

    private parseDates(sourceControl: SourceControlDto): SourceControlDto {
        return {
            ...sourceControl,
            statuses: sourceControl.statuses.map(status => ({
                ...status,
                from: DateUtils.convertUTCDateToLocalDate(status.from),
                to: status.to != null ? DateUtils.convertUTCDateToLocalDate(status.to) : undefined,
            }))
        }
    }
}