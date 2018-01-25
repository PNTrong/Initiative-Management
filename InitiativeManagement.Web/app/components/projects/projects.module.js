/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('InitiativeManagement.projects', ['InitiativeManagement.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider, CreateField, ViewField) {
        $stateProvider.state('projects', {
            url: "/sang-kien",
            templateUrl: "/app/components/projects/projectListView.html",
            parent: 'base',
            controller: "projectListController",
            //permission: ViewField
        }).state('add_project', {
            url: "/sang-kien/nop-don",
            parent: 'base',
            templateUrl: "/app/components/projects/projectAddView.html",
            controller: "projectAddController",
            //permission: CreateField
        });
    }
})();