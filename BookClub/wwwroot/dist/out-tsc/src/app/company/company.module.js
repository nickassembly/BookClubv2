import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompaniesComponent } from './companies/companies.component';
import { RouterModule } from '@angular/router';
let CompanyModule = class CompanyModule {
};
CompanyModule = __decorate([
    NgModule({
        declarations: [CompaniesComponent],
        imports: [
            CommonModule,
            RouterModule.forChild([
                { path: 'companies', component: CompaniesComponent }
            ])
        ]
    })
], CompanyModule);
export { CompanyModule };
//# sourceMappingURL=company.module.js.map