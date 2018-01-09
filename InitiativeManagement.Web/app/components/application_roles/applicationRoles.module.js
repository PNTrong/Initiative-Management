/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('InitiativeManagement.application_roles', ['InitiativeManagement.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('application_roles', {
            url: "/quyen",
            templateUrl: "/app/components/application_roles/applicationRoleListView.html",
            parent: 'base',
            controller: "applicationRoleListController"
        })
            .state('add_application_role', {
                url: "/quyen/tao-moi",
                parent: 'base',
                templateUrl: "/app/components/application_roles/applicationRoleAddView.html",
                controller: "applicationRoleAddController"
            })
            .state('edit_application_role', {
                url: "/quyen/cap-nhap/:id",
                templateUrl: "/app/components/application_roles/applicationRoleEditView.html",
                controller: "applicationRoleEditController",
                parent: 'base',
            });
    }
})();