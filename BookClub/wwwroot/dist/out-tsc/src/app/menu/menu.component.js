import { __decorate } from "tslib";
import { Component } from '@angular/core';
let MenuComponent = class MenuComponent {
    constructor(_authService, _router) {
        this._authService = _authService;
        this._router = _router;
        this.logout = () => {
            this._authService.logout();
            this._router.navigate(["/"]);
        };
    }
    ngOnInit() {
        this._authService.authChanged
            .subscribe(res => {
            this.isUserAuthenticated = res;
        });
    }
};
MenuComponent = __decorate([
    Component({
        selector: 'app-menu',
        templateUrl: './menu.component.html',
        styleUrls: ['./menu.component.css']
    })
], MenuComponent);
export { MenuComponent };
//# sourceMappingURL=menu.component.js.map