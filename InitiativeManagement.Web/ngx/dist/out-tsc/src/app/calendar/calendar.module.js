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
var calendar_component_1 = require("./calendar.component");
var calendar_routing_1 = require("./calendar.routing");
var CalendarModule = (function () {
    function CalendarModule() {
    }
    return CalendarModule;
}());
CalendarModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            router_1.RouterModule.forChild(calendar_routing_1.CalendarRoutes),
            forms_1.FormsModule
        ],
        declarations: [calendar_component_1.CalendarComponent]
    })
], CalendarModule);
exports.CalendarModule = CalendarModule;
//# sourceMappingURL=calendar.module.js.map