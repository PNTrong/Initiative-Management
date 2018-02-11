/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('InitiativeManagement',
        ['InitiativeManagement.application_groups',
            'InitiativeManagement.projects',
            'InitiativeManagement.application_roles',
            'InitiativeManagement.application_users',
            'InitiativeManagement.fields',
            'InitiativeManagement.field_groups',
            'dx',
            'ngAnimate',
            'InitiativeManagement.common'])
        .config(config)
        .config(configAuthentication)
        .run(['$location', '$rootScope', 'authData', 'apiService', 'permissions', function ($location, $rootScope, authData, apiService, permissions) {
            $rootScope.$on('$stateChangeStart', function (event, toState) {
                if (!authData.authenticationData.IsAuthenticated) {
                    $location.path('/login');
                }
            });
        }]);

    config.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider'];

    function config($stateProvider, $urlRouterProvider, $locationProvider) {
        $locationProvider.html5Mode(true);
        $stateProvider
            .state('base', {
                url: '',
                templateUrl: '/app/shared/views/baseView.html',
                abstract: true
            }).state('login', {
                url: "/login",
                templateUrl: "/app/components/login/loginView.html",
                controller: "loginController",
            })
            .state('home', {
                url: "/trang-chu",
                parent: 'base',
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController",
            }).state('newhome', {
                url: "/",
                parent: 'base',
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise('/');
    }

    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location, $window) {
            return {
                request: function (config) {
                    return config;
                },
                requestError: function (rejection) {
                    $window.location.reload();
                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                        $window.location.reload();
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {
                    if (rejection.status == "401") {
                        $location.path('/login');
                        $window.location.reload();
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();