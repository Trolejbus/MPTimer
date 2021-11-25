export class SourceControlStateDto {
    public id!: string;
    public sourceControlId!: string;
    public from!: Date;
    public to?: Date;
    public branchName!: string;
}