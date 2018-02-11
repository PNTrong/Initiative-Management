(function (app) {
    app.controller('loginController', ['$scope', '$rootScope', 'loginService', '$injector', 'notificationService', '$http', 'authenticationService',
        function ($scope,$rootScope, loginService, $injector, notificationService, $http, authenticationService) {
            
            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.year = new Date().getFullYear();

            $scope.loginSubmit = function () {
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    if (response != null && response.data.error != undefined) {
                        DevExpress.ui.notify("Tên hoặc mật khẩu không đúng","error",5000);
                    }
                    else {
                        DevExpress.ui.notify("Đăng nhập thành công","success",3000);
                        var stateService = $injector.get('$state');
                        stateService.go('home');
                    }
                });
            }
        }]);
})(angular.module('InitiativeManagement')); 