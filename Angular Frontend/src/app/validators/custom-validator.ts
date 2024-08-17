import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function createPasswordValidator() : ValidatorFn {
    return (control:AbstractControl) : ValidationErrors | null => {

        const value = control.value;

        if (!value) {
            return null;
        }

        const hasAlphabet = /[a-zA-Z]+/.test(value);

        const hasSpecial = /[^a-zA-Z0-9]+/.test(value);

        const hasNumeric = /[0-9]+/.test(value);

        const passwordValid = hasAlphabet && hasSpecial && hasNumeric

        return !passwordValid ? {passwordStrength:true}: null;
    }
}