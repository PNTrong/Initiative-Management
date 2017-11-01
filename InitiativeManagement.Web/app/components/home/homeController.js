(function (app) {
    app.controller('homeController', homeController); 
    homeController.$inject = ['authenticationService', '$window', '$rootScope', 'apiService']; 
    function homeController(authenticationService, $window, $rootScope, apiService) {

        $rootScope.$on('$locationChangeStart', function (event, current, previous) {
            //reload when user is logged in.
            if (previous.includes('login')) {
                $window.location.reload(); 
            }
        }); 
    }
})(angular.module('InitiativeManagement')); 