import { __decorate } from "tslib";
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
let EnvironmentUrlService = class EnvironmentUrlService {
    constructor() {
        this.urlAddress = environment.urlAddress;
    }
};
EnvironmentUrlService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], EnvironmentUrlService);
export { EnvironmentUrlService };
//# sourceMappingURL=environment-url.service.js.map