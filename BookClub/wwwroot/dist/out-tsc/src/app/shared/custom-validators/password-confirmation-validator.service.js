import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let PasswordConfirmationValidatorService = class PasswordConfirmationValidatorService {
    constructor() {
        this.validateConfirmPassword = (passwordControl) => {
            return (confirmationControl) => {
                const confirmValue = confirmationControl.value;
                const passwordValue = passwordControl.value;
                if (confirmValue === '') {
                    return;
                }
                if (confirmValue !== passwordValue) {
                    return { mustMatch: true };
                }
                return null;
            };
        };
    }
};
PasswordConfirmationValidatorService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], PasswordConfirmationValidatorService);
export { PasswordConfirmationValidatorService };
//# sourceMappingURL=password-confirmation-validator.service.js.map