import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let RepositoryService = class RepositoryService {
    constructor(http, envUrl) {
        this.http = http;
        this.envUrl = envUrl;
        this.getData = (route) => {
            return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress));
        };
        this.createCompleteRoute = (route, envAddress) => {
            return `${envAddress}/${route}`;
        };
    }
};
RepositoryService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], RepositoryService);
export { RepositoryService };
//# sourceMappingURL=repository.service.js.map