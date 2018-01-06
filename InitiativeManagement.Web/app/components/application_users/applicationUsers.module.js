/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('InitiativeManagement.application_users', ['InitiativeManagement.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('application_users', {
            url: "/nguoi-dung",
            templateUrl: "/app/components/application_users/applicationUserListUIView.html",
            parent: 'base',
            controller: "applicationUserListController"
        })
            .state('add_application_user', {
                url: "/nguoi-dung/tao-moi",
                parent: 'base',
                templateUrl: "/app/components/application_users/applicationUserAddUIView.html",
                controller: "applicationUserAddController"
            })
            .state('edit_application_user', {
                url: "/nguoi-dung/cap-nhap/:id",
                templateUrl: "/app/components/application_users/applicationUserEditUIView.html",
                controller: "applicationUserEditController",
                parent: 'base',
            });
    }
})();