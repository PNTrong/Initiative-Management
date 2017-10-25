(function (app) {
    app.controller('initiativeAddController', initiativeAddController); 
    
    initiativeAddController.$inject = ['apiService', '$scope', 'notificationService', '$state','commonService'];    

    function initiativeAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.editorOptions =  {
            languague:'vi', 
            height:'50px'
        }
        
    }
})(angular.module('InitiativeManagement.initiatives')); 