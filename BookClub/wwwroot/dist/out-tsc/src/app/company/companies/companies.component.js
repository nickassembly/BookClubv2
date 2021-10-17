import { __decorate } from "tslib";
import { Component } from '@angular/core';
let CompaniesComponent = class CompaniesComponent {
    constructor(repository) {
        this.repository = repository;
        this.getCompanies = () => {
            const apiAddress = "api/companies";
            this.repository.getData(apiAddress)
                .subscribe(res => {
                this.companies = res;
            });
        };
    }
    ngOnInit() {
        this.getCompanies();
    }
};
CompaniesComponent = __decorate([
    Component({
        selector: 'app-companies',
        templateUrl: './companies.component.html',
        styleUrls: ['./companies.component.css']
    })
], CompaniesComponent);
export { CompaniesComponent };
//# sourceMappingURL=companies.component.js.map