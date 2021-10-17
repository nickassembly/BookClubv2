import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
let AuthenticationService = class AuthenticationService {
    constructor(_http, _envUrl) {
        this._http = _http;
        this._envUrl = _envUrl;
        this._authChangeSub = new Subject();
        this.authChanged = this._authChangeSub.asObservable();
        this.registerUser = (route, body) => {
            return this._http.post(this.createCompleteRoute(route, this._envUrl.urlAddress), body);
        };
        this.loginUser = (route, body) => {
            return this._http.post(this.createCompleteRoute(route, this._envUrl.urlAddress), body);
        };
        this.logout = () => {
            localStorage.removeItem("token");
            this.sendAuthStateChangeNotification(false);
        };
        this.sendAuthStateChangeNotification = (isAuthenticated) => {
            this._authChangeSub.next(isAuthenticated);
        };
        this.createCompleteRoute = (route, envAddress) => {
            return `${envAddress}/${route}`;
        };
    }
};
AuthenticationService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], AuthenticationService);
export { AuthenticationService };
//# sourceMappingURL=authentication.service.js.map