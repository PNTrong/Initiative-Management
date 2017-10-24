/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('InitiativeManagement.initiatives', ['InitiativeManagement.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('initiatives', {
            url: "/initiatives",
            templateUrl: "/app/components/initiatives/initiativeListView.html",
            parent: 'base',
            controller: "initiativeListViewController"
        }).state('add_initiatives', {
                url: "/add_initiatives",
                parent: 'base',
                templateUrl: "/app/components/initiatives/initiativeAddView.html",
                controller: "initiativeAddController",
                css: '/app/components/initiatives/initiativeListView.css'
            });
    }
})();