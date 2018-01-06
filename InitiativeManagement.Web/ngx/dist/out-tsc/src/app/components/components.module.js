"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var common_1 = require("@angular/common");
var forms_1 = require("@angular/forms");
var buttons_component_1 = require("./buttons/buttons.component");
var components_routing_1 = require("./components.routing");
var grid_component_1 = require("./grid/grid.component");
var icons_component_1 = require("./icons/icons.component");
var notifications_component_1 = require("./notifications/notifications.component");
var panels_component_1 = require("./panels/panels.component");
var sweetalert_component_1 = require("./sweetalert/sweetalert.component");
var typography_component_1 = require("./typography/typography.component");
var field_group_component_1 = require("./field-group/field-group.component");
var ComponentsModule = (function () {
    function ComponentsModule() {
    }
    return ComponentsModule;
}());
ComponentsModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            router_1.RouterModule.forChild(components_routing_1.ComponentsRoutes),
            forms_1.FormsModule
        ],
        declarations: [
            buttons_component_1.ButtonsComponent,
            grid_component_1.GridSystemComponent,
            icons_component_1.IconsComponent,
            notifications_component_1.NotificationsComponent,
            panels_component_1.PanelsComponent,
            sweetalert_component_1.SweetAlertComponent,
            typography_component_1.TypographyComponent,
            field_group_component_1.FieldGroupComponent
        ]
    })
], ComponentsModule);
exports.ComponentsModule = ComponentsModule;
//# sourceMappingURL=components.module.js.map