(function (app) {
    'use strict'; 
    app.service('authenticationService', ['$http', '$q', 'localStorageService', 'authData', 'permissions',
        function ($http, $q, localStorageService, authData, permissions ) {
            var tokenInfo; 
            var userAuthData;

            this.setTokenInfo = function (data) {
                tokenInfo = data; 
                localStorageService.set("TokenInfo", JSON.stringify(tokenInfo)); 
            }

            this.getTokenInfo = function () {
                return tokenInfo; 
            }

            this.removeToken = function () {
                tokenInfo = null; 
                localStorageService.set("TokenInfo", null); 
            }

            this.init = function () {
                var tokenInfo = localStorageService.get("TokenInfo"); 
                var userAuthData = localStorageService.get("userAuthData"); 
                
                if (tokenInfo) {
                    tokenInfo = JSON.parse(tokenInfo); 
                    authData.authenticationData.IsAuthenticated = true; 
                    authData.authenticationData.userName = tokenInfo.userName; 
                    authData.authenticationData.accessToken = tokenInfo.accessToken; 
                }

                if (userAuthData) {

                    console.log("init data...");
                    
                    userAuthData = JSON.parse(userAuthData); 
                    permissions.setPermissions(userAuthData);
                }
            }

            this.setHeader = function () {
                delete $http.defaults.headers.common['X-Requested-With']; 
                if ((authData.authenticationData != undefined) && (authData.authenticationData.accessToken != undefined) && (authData.authenticationData.accessToken != null) && (authData.authenticationData.accessToken != "")) {
                    $http.defaults.headers.common['Authorization'] = 'Bearer ' + authData.authenticationData.accessToken; 
                    $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8'; 
                }
            }

            this.validateRequest = function () {
                var url = 'api/home/TestMethod'; 
                var deferred = $q.defer(); 
                $http.get(url).then(function () {
                    deferred.resolve(null); 
                }, function (error) {
                    deferred.reject(error); 
                }); 
                return deferred.promise; 
            }
        
            this.setPermissions = function (data) {
                userAuthData = data; 
                localStorageService.set("userAuthData", JSON.stringify(userAuthData)); 
            }
            
            this.getPermissions = function () {
                return userAuthData; 
            }

            this.removePermissions = function () {
                userAuthData = null; 
                localStorageService.set("userAuthData", null); 
            }

            this.init(); 
        }
    ]); 
})(angular.module('InitiativeManagement.common')); 