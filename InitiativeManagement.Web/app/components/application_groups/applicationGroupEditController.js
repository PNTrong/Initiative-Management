// (function (app) {
//     'use strict';

//     app.controller('applicationGroupEditController', applicationGroupEditController);

//     applicationGroupEditController.$inject = ['$scope', 'apiService', 'notificationService', '$location', '$stateParams'];

//     function applicationGroupEditController($scope, apiService, notificationService, $location, $stateParams) {
//         $scope.group = {};
//         $scope.selectedItems = [];
//         $scope.roles = [];
//         $scope.listInstance = {};

//         // list
//         // $scope.BindWidget = function(data){
//         //     $scope.listOptions = {
//         //         onInitialized: function(e){
//         //             $scope.listInstance = e.component;
//         //           },
//         //         dataSource: data,
//         //         height: 400,
//         //         showSelectionControls: true,
//         //         selectionMode: "multiple",
//         //         bindingOptions: {
//         //             selectedItems: "selectedItems",
//         //             selectionMode: "all",
//         //             selectAllMode: "page",
//         //         }
//         //     };
//         //   }

//         $scope.listOptions = {
//             onInitialized: function (e) {
//                 $scope.listInstance = e.component;
//             },
//             dataSource: $scope.roles,
//             height: 400,
//             showSelectionControls: true,
//             selectionMode: "multiple",
//             bindingOptions: {
//                 selectedItems: "selectedItems",
//                 selectionMode: "all",
//                 selectAllMode: "page",
//             }
//         };

//         $scope.BindText = function (data) {
//             $scope.textBox = {
//                 groupName: {
//                     value: data,
//                     disabled: true,
//                     onValueChanged: function (data) {
//                         $scope.textBox.groupName.value = data.value;
//                     }
//                 },
//             };
//         }

//         $scope.updateApplicationGroup = updateApplicationGroup;

//         function updateApplicationGroup() {
//             apiService.post('/api/applicationGroup/update', $scope.group, addSuccessed, addFailed);
//         }

//         function loadDetail() {
//             apiService.get('/api/applicationGroup/detail/' + $stateParams.id, null,
//                 function (result) {
//                     $scope.group = result.data;
//                     $scope.BindText(result.data.Name);
//                     $scope.selectedItems = result.data.Roles;
//                     // $scope.BindWidget($scope.roles, result.data.Roles);
//                     // $scope.listInstance.reload();

//                     // $scope.mySelectedItems =result.data.Roles;
//                 },
//                 function (result) {
//                     notificationService.displayError(result.data);
//                 });
//         }

//         function addSuccessed() {
//             notificationService.displaySuccess($scope.group.Name + ' đã được cập nhật thành công.');

//             $location.url('nhom-quyen');
//         }

//         function addFailed(response) {
//             notificationService.displayError(response.data.Message);
//         }

//         function loadRoles() {
//             apiService.get('/api/applicationRole/getlistall',
//                 null,
//                 function (response) {
//                     $scope.roles = response.data;
//                 }, function (response) {
//                     notificationService.displayError('Không tải được danh sách quyền.');
//                 });
//         }

//         loadRoles();

//         loadDetail();
//     }
// })(angular.module('InitiativeManagement.application_groups'));

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

            $location.url('/nhom-nguoi-dung');
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