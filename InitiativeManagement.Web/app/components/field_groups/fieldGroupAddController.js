(function (app) {
    app.controller('fieldGroupAddController', fieldGroupAddController);
    fieldGroupAddController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'commonService'];
    function fieldGroupAddController($scope, apiService, notificationService, $location, commonService){
        $scope.fieldGroup = {
            Id: 0
        }

        $scope.addFieldGroup = addFieldGroup;

        function addFieldGroup() {
            apiService.post('/api/fieldGroup/add', $scope.fieldGroup, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.fieldGroup.Name + ' đã được thêm mới.');

            $location.url('field_groups');
        }

        function addFailed(response) {
            notificationService.displayError(response.data.Message);
        }
    }
})(angular.module('InitiativeManagement.field_groups'));