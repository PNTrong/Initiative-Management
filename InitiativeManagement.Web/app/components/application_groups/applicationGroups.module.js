/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('InitiativeManagement.application_groups', ['InitiativeManagement.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('application_groups', {
            url: "/nhom-nguoi-dung",
            templateUrl: "/app/components/application_groups/applicationGroupListView.html",
            parent: 'base',
            controller: "applicationGroupListController"
        })
            .state('add_application_group', {
                url: "/nhom-nguoi-dung/tao-moi",
                parent: 'base',
                templateUrl: "/app/components/application_groups/applicationGroupAddView.html",
                controller: "applicationGroupAddController"
            })
            .state('edit_application_group', {
                url: "/nhom-nguoi-dung/cap-nhap/:id",
                templateUrl: "/app/components/application_groups/applicationGroupEditView.html",
                controller: "applicationGroupEditController",
                parent: 'base',
            });
    }
})();