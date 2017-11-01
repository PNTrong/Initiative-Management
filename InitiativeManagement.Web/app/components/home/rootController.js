(function (app) {
    app.controller('rootController', rootController); 

    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService', 'userService', 'apiService']; 

    function rootController($state, authData, loginService, $scope, authenticationService, userService, apiService) {
        $scope.logOut = function () {
            loginService.logOut(); 
            $state.go('login'); 
        }

        debugger; 
        $scope.authentication = authData.authenticationData; 
        
        getRole(); 

        $scope.role = authenticationService.getRole(); 

        function getRole() {
            if(authData.authenticationData.IsAuthenticated)
            apiService.get('/api/account/permission',null, function (res) {
                authenticationService.setRole(res.data); 
            }, null); 
        }

        $scope.sideBarClass = "main-sidebar";
        $scope.changeClass = function(){
            debugger;
          if ($scope.sideBarClass === "main-sidebar")
            $scope.sideBarClass = "main-sidebar-hide";
          else
            $scope.sideBarClass = "main-sidebar";
        };
    }
})(angular.module('InitiativeManagement')); 