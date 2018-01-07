/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('InitiativeManagement.fields', ['InitiativeManagement.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider','CreateField','ViewField'];

    function config($stateProvider, $urlRouterProvider,CreateField,ViewField) {
        $stateProvider.state('fields', {
            url: "/linh-vuc",
            templateUrl: "/app/components/fields/fieldListView.html",
            parent: 'base',
            controller: "fieldListController",
            css: '/app/components/fields/fields.css',
            permission: ViewField
        }).state('add_field', {
            url: "/linh-vuc/tao-moi",
            parent: 'base',
            templateUrl: "/app/components/fields/fieldAddView.html",
            controller: "fieldAddController",
            css: '/app/components/fields/fields.css',
            permission: CreateField
        });
    }
})();