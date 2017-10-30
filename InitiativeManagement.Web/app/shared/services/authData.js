(function (app) {
    'use strict';
    app.factory('authData', [function () {
        var authDataFactory = {};

        var authentication = {
            IsAuthenticated: false,
            userName: ""
        };
        
        //var authenticationJson = localStorage.getItem('userInfo');
        //if( authenticationJson!= null){
        //    authentication = JSON.parse(authenticationJson);
        //}
        authDataFactory.authenticationData = authentication;
        
        return authDataFactory;
    }]);
})(angular.module('InitiativeManagement.common'));
