(function (app) {
    app.controller('loginController', ['$scope', '$rootScope', 'loginService', '$injector', 'notificationService', 'apiService', 'authenticationService',
        function ($scope,$rootScope, loginService, $injector, notificationService, apiService, authenticationService) {
            
            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.year = new Date().getFullYear();

            $rootScope.bodylayout = 'login-page ls-closed';
            
            $scope.loginSubmit = function () {
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    if (response != null && response.data.error != undefined) {
                        notificationService.displayError("Tên hoặc mật khẩu không đúng.");
                    }
                    else {
                        var stateService = $injector.get('$state');
                        stateService.go('home');
                    }
                });
            }
        }]);
})(angular.module('InitiativeManagement')); 