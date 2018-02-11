(function (app) {
    app.controller('homeController', homeController); 
    homeController.$inject = ['authenticationService','$scope', '$window', '$rootScope', 'apiService']; 
    function homeController(authenticationService, $scope, $window, $rootScope, apiService) {
        $rootScope.$on('$locationChangeStart', function (event, current, previous) {
            //reload when user is logged in.
            // if (previous.includes('login')) {
            //     $window.location.reload(); 
            // }
        }); 

        $scope.doneButtonOptions = {
            text: "Done",
            type: "default",
            onClick: function(e) { 
                DevExpress.ui.notify("The Done button was clicked");
            }
        };
    }
})(angular.module('InitiativeManagement')); 