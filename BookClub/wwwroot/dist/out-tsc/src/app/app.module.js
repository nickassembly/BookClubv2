import { __decorate } from "tslib";
import { ErrorHandlerService } from './shared/services/error-handler.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
let AppModule = class AppModule {
};
AppModule = __decorate([
    NgModule({
        declarations: [
            AppComponent,
            HomeComponent,
            MenuComponent,
            NotFoundComponent
        ],
        imports: [
            BrowserModule,
            HttpClientModule,
            RouterModule.forRoot([
                { path: 'home', component: HomeComponent },
                { path: 'book', loadChildren: () => import('./book/book.module').then(m => m.BookModule) },
                { path: 'authentication', loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule) },
                { path: '404', component: NotFoundComponent },
                { path: '', redirectTo: '/home', pathMatch: 'full' },
                { path: '**', redirectTo: '/404', pathMatch: 'full' }
            ])
        ],
        providers: [
            {
                provide: HTTP_INTERCEPTORS,
                useClass: ErrorHandlerService,
                multi: true
            }
        ],
        bootstrap: [AppComponent]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map