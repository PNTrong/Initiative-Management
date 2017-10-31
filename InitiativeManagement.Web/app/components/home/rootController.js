(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService','userService','apiService'];

    function rootController($state, authData, loginService, $scope, authenticationService,userService,apiService) {
        $scope.logOut = function () {
            loginService.logOut();
            $state.go('login');
        }
        debugger;
        $scope.authentication = authData.authenticationData;
        
        $scope.role = authenticationService.getRole();

        getRole();

        function getRole() {
            apiService.get('/api/home/permission', null, function (res) {
                authenticationService.setRole(res.data);
            }, null);
        }
    }
})(angular.module('InitiativeManagement'));