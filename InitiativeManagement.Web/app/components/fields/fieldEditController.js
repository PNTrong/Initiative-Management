(function (app) {
    app.controller('fieldEditController', fieldEditController);
    fieldEditController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'commonService', '$stateParams'];

    function fieldEditController($scope, apiService, notificationService, $location, commonService, $stateParams) {
        $scope.field = {
            Id: 0,
            FieldGroupId: 0
        }

        $scope.editField = editField;

        function editField() {
            apiService.put('/api/field/update', $scope.field, editSuccessed, editFailed);
        }

        function editSuccessed() {
            notificationService.displaySuccess($scope.field.FieldName + ' đã cập nhật thành công');

            $location.url('fields');
        }

        function editFailed(response) {
            notificationService.displayError(response.data.Message);
        }
        function loadDetail() {
            apiService.get('/api/field/getbyid/' + $stateParams.id, null,
            function (result) {
                $scope.field = result.data;
            },
            function (result) {
                notificationService.displayError(result.data);
            });
        }

        function loadFieldGroups() {
            apiService.get('api/fieldGroup/getall', null, function (result) {
                $scope.fieldGroups = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadFieldGroups();
        loadDetail();
    }
})(angular.module('InitiativeManagement.fields'));