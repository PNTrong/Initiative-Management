(function (app) {
    app.controller('projectListController', projectListController);

    projectListController.$inject = ['authenticationService', '$scope', '$window', '$rootScope', 'apiService'];

    function projectListController(authenticationService, $scope, $window, $rootScope, apiService) {
    }
})(angular.module('InitiativeManagement.projects')); 