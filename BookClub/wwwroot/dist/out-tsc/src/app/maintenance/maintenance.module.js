import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookListComponent } from './book-list/book-list.component';
import { RouterModule } from '@angular/router';
let MaintenanceModule = class MaintenanceModule {
};
MaintenanceModule = __decorate([
    NgModule({
        declarations: [
            BookListComponent
        ],
        imports: [
            CommonModule,
            RouterModule.forChild([
                { path: 'list', component: BookListComponent }
            ])
        ],
    })
], MaintenanceModule);
export { MaintenanceModule };
//# sourceMappingURL=maintenance.module.js.map