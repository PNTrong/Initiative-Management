"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var Rx_1 = require("rxjs/Rx");
var serverUrl = '';
var httpOptions = { withCredentials: true };
var BaseService = (function () {
    function BaseService(http) {
        this.http = http;
        if (window.location.host === 'localhost:4200') {
            serverUrl = 'http://localhost:55429/'; // VS
        }
    }
    BaseService.prototype.getApiHost = function () { return serverUrl; };
    BaseService.prototype.get = function (url, options) {
        if (options) {
            return this.http.get(url, options).map(function (response) {
                return response.json();
            }).catch(function (error) {
                if (error.status === 302 || error.status === '302') {
                    // do some thing
                }
                else if (error.status === 401 || error.status === '401') {
                    // redirect to login page
                }
                else {
                    return Rx_1.Observable.throw(new Error(error));
                }
            });
        }
        else {
            return this.http.get(url).map(function (response) {
                return response.json();
            }).catch(function (error) {
                if (error.status === 302 || error.status === '302') {
                    // do some thing
                }
                else {
                    return Rx_1.Observable.throw(new Error(error));
                }
            });
        }
    };
    BaseService.prototype.post = function (url, data, options) {
        if (options) {
            return this.http.post(url, data, httpOptions).map(function (response) {
                return response.json();
            }).catch(function (error) {
                if (error.status === 302 || error.status === '302') {
                    // do some thing
                }
                else if (error.status === 401 || error.status === '401') {
                    // redirect to login page
                }
                else {
                    return Rx_1.Observable.throw(new Error(error));
                }
            });
        }
        else {
            return this.http.post(url, data).map(function (response) {
                return response.json();
            }).catch(function (error) {
                if (error.status === 302 || error.status === '302') {
                    // do some thing
                }
                else {
                    return Rx_1.Observable.throw(new Error(error));
                }
            });
        }
    };
    return BaseService;
}());
BaseService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], BaseService);
exports.BaseService = BaseService;
//# sourceMappingURL=base.service.js.map