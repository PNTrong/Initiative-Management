(function (app) {
    app.controller('fieldGroupEditController', fieldGroupEditController);
    fieldGroupEditController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'commonService', '$stateParams'];
    function fieldGroupEditController($scope, apiService, notificationService, $location, commonService, $stateParams) {
        $scope.fieldGroup = {
            Id: 0
        }

        $scope.editFieldGroup = editFieldGroup;

        function editFieldGroup() {
            apiService.put('/api/fieldGroup/update', $scope.fieldGroup, editSuccessed, editFailed);
        }

        function editSuccessed() {
            notificationService.displaySuccess($scope.fieldGroup.Name + ' đã được cập nhật.');

            $location.url('field_groups');
        }

        function editFailed(response) {
            notificationService.displayError(response.data.Message);
        }
        function loadDetail() {
            apiService.get('/api/fieldGroup/getbyid/' + $stateParams.id, null,
            function (result) {
                $scope.fieldGroup = result.data;
            },
            function (result) {
                notificationService.displayError(result.data);
            });
        }
        loadDetail();
    }
})(angular.module('InitiativeManagement.field_groups'));