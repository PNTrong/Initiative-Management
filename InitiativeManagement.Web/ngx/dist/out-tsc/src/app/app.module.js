"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var pages_module_1 = require("./pages/pages.module");
var authentication_service_1 = require("./services/authentication.service");
var core_1 = require("@angular/core");
var animations_1 = require("@angular/platform-browser/animations"); // this is needed!
var router_1 = require("@angular/router");
var http_1 = require("@angular/http");
var forms_1 = require("@angular/forms");
var app_component_1 = require("./app.component");
var sidebar_module_1 = require("./sidebar/sidebar.module");
var fixedplugin_module_1 = require("./shared/fixedplugin/fixedplugin.module");
var footer_module_1 = require("./shared/footer/footer.module");
var navbar_module_1 = require("./shared/navbar/navbar.module");
var pagesnavbar_module_1 = require("./shared/pagesnavbar/pagesnavbar.module");
var admin_layout_component_1 = require("./layouts/admin/admin-layout.component");
var auth_layout_component_1 = require("./layouts/auth/auth-layout.component");
var app_routing_1 = require("./app.routing");
var base_service_1 = require("app/services/base.service");
var devextreme_angular_1 = require("devextreme-angular");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [
            animations_1.BrowserAnimationsModule,
            forms_1.FormsModule,
            router_1.RouterModule.forRoot(app_routing_1.AppRoutes),
            http_1.HttpModule,
            sidebar_module_1.SidebarModule,
            navbar_module_1.NavbarModule,
            footer_module_1.FooterModule,
            fixedplugin_module_1.FixedPluginModule,
            pagesnavbar_module_1.PagesnavbarModule,
            devextreme_angular_1.DevExtremeModule,
            pages_module_1.PagesModule
        ],
        declarations: [
            app_component_1.AppComponent,
            admin_layout_component_1.AdminLayoutComponent,
            auth_layout_component_1.AuthLayoutComponent
        ],
        providers: [base_service_1.BaseService, authentication_service_1.AuthenticationService],
        bootstrap: [app_component_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map