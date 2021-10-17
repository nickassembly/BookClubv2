import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterUserComponent } from './register-user/register-user.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
let AuthenticationModule = class AuthenticationModule {
};
AuthenticationModule = __decorate([
    NgModule({
        declarations: [RegisterUserComponent, LoginComponent],
        imports: [
            CommonModule,
            ReactiveFormsModule,
            RouterModule.forChild([
                { path: 'register', component: RegisterUserComponent },
                { path: 'login', component: LoginComponent }
            ])
        ]
    })
], AuthenticationModule);
export { AuthenticationModule };
//# sourceMappingURL=authentication.module.js.map