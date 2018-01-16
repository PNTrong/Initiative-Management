(function (app) {
    'use strict';

    app.controller('applicationUserAddController', applicationUserAddController);

    applicationUserAddController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'commonService'];

    function applicationUserAddController($scope, apiService, notificationService, $location, commonService) {
        $scope.account = {
            Groups: []
        }

        $scope.dateBox = {
            type: "date",
            max: new Date(),
            min: new Date(1900, 0, 1),
            bindingOptions: {
                value: "account.BirthDay"
            }
        };

        $scope.addAccount = addAccount;

        function addAccount() {
            apiService.post('/api/applicationUser/add', $scope.account, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.account.Name + ' đã được thêm mới.');

            $location.url('nguoi-dung');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
        }

        function loadGroups() {
            apiService.get('/api/applicationGroup/getlistall',
                null,
                function (response) {
                    $scope.groups = response.data;
                }, function (response) {
                    notificationService.displayError('Không tải được danh sách nhóm.');
                });

        }

        loadGroups();

    }
})(angular.module('InitiativeManagement.application_users'));