/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('InitiativeManagement.field_groups', ['InitiativeManagement.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('field_groups', {
            url: "/nhom-linh-vuc",
            templateUrl: "/app/components/field_groups/fieldGroupListUIView.html",
            parent: 'base',
            controller: "fieldGroupListController",
            css: '/app/components/field_groups/fieldGroups.css'
        }).state('add_field_groups', {
            url: "/nhom-linh-vuc/tao-moi",
            parent: 'base',
            templateUrl: "/app/components/field_groups/fieldGroupAddUIView.html",
            controller: "fieldGroupAddController",
            css: '/app/components/field_groups/fieldGroups.css'
        }).state('edit_field_groups', {
            url: "/nhom-linh-vuc/cap-nhat/:id",
            parent: 'base',
            templateUrl: "/app/components/field_groups/fieldGroupEditUIView.html",
            controller: "fieldGroupEditController",
            css: '/app/components/field_groups/fieldGroups.css'
        })
        ;
    }
})();