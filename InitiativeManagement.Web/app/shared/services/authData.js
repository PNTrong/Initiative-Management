(function (app) {
    'use strict';
    app.factory('authData', ['$http', function ($http) {
        var authDataFactory = {};

        var authentication = {
            IsAuthenticated: false,
            userName: "",
            IsPermissionLoad: false,
        };

        authDataFactory.authenticationData = authentication;

        return authDataFactory;
    }]);
})(angular.module('InitiativeManagement.common'));