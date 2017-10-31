(function (app) {
    'use strict';
    app.factory('authData', [function () {
        var authDataFactory = {};

        var authentication = {
            IsAuthenticated: false,
            userName: "",
            IsAdmin:false
        };
        
        authDataFactory.authenticationData = authentication;
        
        return authDataFactory;
    }]);
})(angular.module('InitiativeManagement.common'));
