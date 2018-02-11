(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', '$rootScope', 'authData', 'loginService', '$scope', 'authenticationService', 'userService', 'apiService', 'permissions'];

    function rootController($state, $rootScope, authData, loginService, $scope, authenticationService, userService, apiService, permissions) {
        $scope.logOut = function () {
            loginService.logOut();
            $state.go('login');
        }

        $scope.authentication = authData.authenticationData;
    }
})(angular.module('InitiativeManagement')); 