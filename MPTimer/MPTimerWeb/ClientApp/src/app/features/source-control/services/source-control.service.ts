import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { SignalRService } from "@app/services";
import { DateUtils } from "@app/shared";
import { EntityCollectionServiceBase, EntityCollectionServiceElementsFactory } from "@ngrx/data";
import { combineLatest } from "rxjs";
import { map, startWith, switchMap } from "rxjs/operators";
import { SourceControlDto } from "../models";

@Injectable({ providedIn: 'root' })
export class SourceControlService extends EntityCollectionServiceBase<SourceControlDto> {

    public sourceControls$ = combineLatest([
        this.signalRService.on$("SourceControlBranchChanged").pipe(startWith(null)),
    ]).pipe(
        switchMap(() => this.getAll().pipe(map(control => control.map(c => this.mapDates(c))))),
    );

    constructor(
        private httpClient: HttpClient,
        private signalRService: SignalRService,
        serviceElementsFactory: EntityCollectionServiceElementsFactory,
    ) {
        super('SourceControl', serviceElementsFactory);
    }

    private mapDates(sourceControl: SourceControlDto): SourceControlDto {
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