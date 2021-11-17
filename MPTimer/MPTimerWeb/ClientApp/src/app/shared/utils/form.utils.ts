import { Injectable } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';

@Injectable()
export class FormUtils {

    private formGroup?: FormGroup;

    public static setEnabled(control: AbstractControl, enabled: boolean): void {
        if (enabled) {
            control.enable();
        }
        else {
            control.disable();
        }
    }

    public setForm(formGroup: FormGroup): void {
        this.formGroup = formGroup;
    }

    public hasError(controlName: string, errorName: string | null = null): boolean {
        if (this.formGroup == null) {
            throw new Error('FormGroup not set.');
        }

        if (controlName == null) {
            return this.formGroup.invalid;
        }

        const control = this.formGroup.get(controlName);
        if (control == null) {
            throw new Error(`Could not found control with name '${controlName}'.`);
        }

        if (errorName == null) {
            return control.invalid && control.touched;
        }

        return control.touched && control.hasError(errorName);
    }

    public getError(controlName: string, errorName: string): any {
        if (this.formGroup == null) {
            throw new Error('FormGroup not set.');
        }

        if (controlName == null) {
            return this.formGroup.getError(errorName);
        }

        const control = this.formGroup.get(controlName);
        if (control == null) {
            throw new Error(`Could not found control with name '${controlName}'.`);
        }

        const error = control.getError(errorName);
        if (error == null) {
            throw new Error(`Missing error with name ${errorName}`);
        }

        return error;
    }
}
