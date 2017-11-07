(function (app) {
    'use strict';

    app.controller('applicationGroupEditController', applicationGroupEditController);

    applicationGroupEditController.$inject = ['$scope', 'apiService', 'notificationService', '$location', '$stateParams'];

    function applicationGroupEditController($scope, apiService, notificationService, $location, $stateParams) {
        $scope.group = {}

        $scope.updateApplicationGroup = updateApplicationGroup;

        function updateApplicationGroup() {
            apiService.post('/api/applicationGroup/update', $scope.group, addSuccessed, addFailed);
        }
        function loadDetail() {
            apiService.get('/api/applicationGroup/detail/' + $stateParams.id, null,
            function (result) {
                $scope.group = result.data;
            },
            function (result) {
                notificationService.displayError(result.data);
            });
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.group.Name + ' đã được cập nhật thành công.');

            $location.url('nhom-quyen');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
        }
        function loadRoles() {
            apiService.get('/api/applicationRole/getlistall',
                null,
                function (response) {
                    $scope.roles = response.data;
                }, function (response) {
                    notificationService.displayError('Không tải được danh sách quyền.');
                });

        }

        loadRoles();
        loadDetail();
    }
})(angular.module('InitiativeManagement.application_groups'));