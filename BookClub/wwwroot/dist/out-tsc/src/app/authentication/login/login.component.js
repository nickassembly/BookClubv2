import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
let LoginComponent = class LoginComponent {
    constructor(_authService, _router, _route) {
        this._authService = _authService;
        this._router = _router;
        this._route = _route;
        this.errorMessage = '';
        this.validateControl = (controlName) => {
            return this.loginForm.controls[controlName].invalid && this.loginForm.controls[controlName].touched;
        };
        this.hasError = (controlName, errorName) => {
            return this.loginForm.controls[controlName].hasError(errorName);
        };
        this.loginUser = (loginFormValue) => {
            this.showError = false;
            const login = Object.assign({}, loginFormValue);
            const userForAuth = {
                username: login.username,
                password: login.password
            };
            this._authService.loginUser('api/auth/login', userForAuth)
                .subscribe(res => {
                localStorage.setItem("token", res.token);
                this._authService.sendAuthStateChangeNotification(res.isAuthSuccessful);
                this._router.navigate([this._returnUrl]);
            }, (error) => {
                this.errorMessage = error;
                this.showError = true;
            });
        };
    }
    ngOnInit() {
        this.loginForm = new FormGroup({
            username: new FormControl("", [Validators.required]),
            password: new FormControl("", [Validators.required])
        });
        this._returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
    }
};
LoginComponent = __decorate([
    Component({
        selector: 'app-login',
        templateUrl: './login.component.html',
        styleUrls: ['./login.component.css']
    })
], LoginComponent);
export { LoginComponent };
//# sourceMappingURL=login.component.js.map