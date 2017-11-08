(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', '$rootScope', 'authData', 'loginService', '$scope', 'authenticationService', 'userService', 'apiService','commonService'];

    function rootController($state, $rootScope, authData, loginService, $scope, authenticationService, userService, apiService,commonService) {
        $scope.logOut = function () {
            loginService.logOut();
            $state.go('login');
        }

        getRole();
        
        function getRole() {
            if (authData.authenticationData.IsAuthenticated)
                apiService.get('/api/account/permission', null, function (res) {
                    if(res.data){
                        var role = commonService.getRole(res.data);
                        authenticationService.setRole(role);
                        $scope.authentication = authData.authenticationData;
                    }
                }, null);
        }
    }
})(angular.module('InitiativeManagement')); 