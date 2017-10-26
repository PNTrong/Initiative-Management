/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('InitiativeManagement.fields', ['InitiativeManagement.common']).config(config); 

    config.$inject = ['$stateProvider', '$urlRouterProvider']; 

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('fields',  {
            url:"/fields", 
            templateUrl:"/app/components/fields/fieldListView.html", 
            parent:'base', 
            controller:"fieldListController", 
            css:'/app/components/fields/fields.css'
        }).state('add_field',  {
            url:"/add_field", 
            parent:'base', 
            templateUrl:"/app/components/fields/fieldAddView.html", 
            controller:"fieldAddController", 
            css:'/app/components/fields/fields.css'
        }); 
    }
})();