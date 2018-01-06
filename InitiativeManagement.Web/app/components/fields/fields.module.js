/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('InitiativeManagement.fields', ['InitiativeManagement.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('fields', {
            url: "/linh-vuc",
            templateUrl: "/app/components/fields/fieldListUIView.html",
            parent: 'base',
            controller: "fieldListController",
            css: '/app/components/fields/fields.css'
        }).state('add_field', {
            url: "/linh-vuc/tao-moi",
            parent: 'base',
            templateUrl: "/app/components/fields/fieldAddUIView.html",
            controller: "fieldAddController",
            css: '/app/components/fields/fields.css'
        }).state('edit_field', {
            url: "/linh-vuc/cap-nhat/:id",
            parent: 'base',
            templateUrl: "/app/components/fields/fieldEditUIView.html",
            controller: "fieldEditController",
            css: '/app/components/fields/fields.css'
        });
    }
})();