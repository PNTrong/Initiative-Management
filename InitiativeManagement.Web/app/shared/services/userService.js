(function (app) {
    'use strict';
    app.factory('userService', [function () {
        var userStore = {};

        userStore = {
            isSupperAdmin:false
        }
        
        return userStore;
    }]);
})(angular.module('InitiativeManagement.common'));
