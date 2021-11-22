import { WorkTimeLineActivityModel } from "./work-time-line-activity.model";

export class WorkTimeLineEventModel {
    public id!: string;
    public name!: string;
    public activities!: WorkTimeLineActivityModel[];
    public color!: string;
}
