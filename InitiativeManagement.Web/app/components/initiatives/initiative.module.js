/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('InitiativeManagement.initiatives', ['InitiativeManagement.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('initiatives', {
            url: "/sang-kien",
            templateUrl: "/app/components/initiatives/initiativeListUIView.html",
            parent: 'base',
            controller: "initiativeListViewController",
        }).state('add_initiative', {
            url: "/sang-kien/nop-don",
            parent: 'base',
            templateUrl: "/app/components/initiatives/initiativeAddUIView.html",
            controller: "initiativeAddController",
        });
    }
})();