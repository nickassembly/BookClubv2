import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
let RegisterUserComponent = class RegisterUserComponent {
    constructor(_authService, _passConfValidator, _router) {
        this._authService = _authService;
        this._passConfValidator = _passConfValidator;
        this._router = _router;
        this.errorMessage = '';
        this.validateControl = (controlName) => {
            return this.registerForm.controls[controlName].invalid && this.registerForm.controls[controlName].touched;
        };
        this.hasError = (controlName, errorName) => {
            return this.registerForm.controls[controlName].hasError(errorName);
        };
        this.registerUser = (registerFormValue) => {
            this.showError = false;
            const formValues = Object.assign({}, registerFormValue);
            const user = {
                firstName: formValues.firstName,
                lastName: formValues.lastName,
                username: formValues.username,
                password: formValues.password,
                confirmPassword: formValues.confirm
            };
            this._authService.registerUser("api/accounts/registration", user)
                .subscribe(_ => {
                this._router.navigate(["/authentication/login"]);
            }, error => {
                this.errorMessage = error;
                this.showError = true;
            });
        };
    }
    ngOnInit() {
        this.registerForm = new FormGroup({
            firstName: new FormControl(''),
            lastName: new FormControl(''),
            username: new FormControl('', [Validators.required, Validators.email]),
            password: new FormControl('', [Validators.required]),
            confirm: new FormControl('')
        });
        this.registerForm.get('confirm').setValidators([Validators.required,
            this._passConfValidator.validateConfirmPassword(this.registerForm.get('password'))]);
    }
};
RegisterUserComponent = __decorate([
    Component({
        selector: 'app-register-user',
        templateUrl: './register-user.component.html',
        styleUrls: ['./register-user.component.css']
    })
], RegisterUserComponent);
export { RegisterUserComponent };
//# sourceMappingURL=register-user.component.js.map