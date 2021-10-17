import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
let ErrorHandlerService = class ErrorHandlerService {
    constructor(_router) {
        this._router = _router;
        this.handleError = (error) => {
            if (error.status === 404) {
                return this.handleNotFound(error);
            }
            else if (error.status === 400) {
                return this.handleBadRequest(error);
            }
            else if (error.status === 401) {
                return this.handleUnauthorized(error);
            }
        };
        this.handleUnauthorized = (error) => {
            if (this._router.url === '/authentication/login') {
                return 'Authentication failed. Wrong Username or Password';
            }
            else {
                this._router.navigate(['/authentication/login']);
                return error.message;
            }
        };
        this.handleNotFound = (error) => {
            this._router.navigate(['/404']);
            return error.message;
        };
        this.handleBadRequest = (error) => {
            if (this._router.url === '/authentication/register') {
                let message = '';
                const values = Object.values(error.error.errors);
                values.map((m) => {
                    message += m + '<br>';
                });
                return message.slice(0, -4);
            }
            else {
                return error.error ? error.error : error.message;
            }
        };
    }
    intercept(req, next) {
        return next.handle(req)
            .pipe(catchError((error) => {
            let errorMessage = this.handleError(error);
            return throwError(errorMessage);
        }));
    }
};
ErrorHandlerService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], ErrorHandlerService);
export { ErrorHandlerService };
//# sourceMappingURL=error-handler.service.js.map