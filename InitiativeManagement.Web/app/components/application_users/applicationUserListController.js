(function (app) {
    'use strict';

    app.controller('applicationUserListController', applicationUserListController);

    applicationUserListController.$inject = ['$scope', '$http','apiService','authenticationService', 'notificationService', '$ngBootbox'];

    function applicationUserListController($scope,$http, apiService,authenticationService, notificationService, $ngBootbox) {
        $scope.gridInstance = {};

        $scope.textBox = {
            search: {
                width: 300,
                value:"",
                showClearButton: true,
                placeholder: "Tìm kiếm...",
                onValueChanged: function(data) {
                    $scope.textBox.search.value = data.value;
                    $scope.gridInstance.refresh();
                }
            },
        };
        // grid
        var orders = new DevExpress.data.CustomStore({
            load: function (loadOptions) {
                var parameters = {};
                parameters.skip = loadOptions.skip || 0;
                parameters.take = loadOptions.take || 10;
                parameters.filter = $scope.textBox.search.value;
                
                var config = {
                    params: parameters
                };

                authenticationService.setHeader();

                return $http.get("api/applicationUser/getlistpaging", config)
                    .then(function (response) {
                        return { data: response.data.items, totalCount: response.data.totalCount };
                    }, function (response) {
                        return { data: [], totalCount: 0 };
                    });
            }
        });
       
        
        $scope.dataGridOptions = {
            onInitialized: function(e) {
                $scope.gridInstance = e.component;
              },
            dataSource: {
                store: orders
            },
            remoteOperations: {
                sorting: false,
                paging: true
            },
            paging: {
                pageSize: 10
            },
            pager: {
                showPageSizeSelector: true,
                allowedPageSizes: [5, 10, 20]
            },
            columns: [{
                dataField: "FullName",
                caption: "Tên người dùng",
                alignment: "center"
            },{
                dataField: "UserName",
                caption: "Tài khoản",
                alignment: "center"
            },
            {
                dataField: "Email",
                caption: "Email",
                alignment: "center"
            },
            {
                dataField: "IsDeactive",
                caption: "Trạng thái",
                cellTemplate: "cellTemplate",
                alignment: "center"
            },
            {
                dataField: "Id",
                caption: "",
                cellTemplate: "actionCellTemplate",
                alignment: "center",
                width: 75
            }],
            allowColumnResizing: true,
            bindingOptions: {
                rowAlternationEnabled: 'true',
                showRowLines: 'false',
                showBorders: 'true',
                showColumnLines: 'true',
                columnResizingMode: "widget"
            }
        };

        // function loadData() {
        //     apiService.get('api/applicationUser/getlistpaging', null, dataLoadCompleted, dataLoadFailed);
        // }

        
        // function dataLoadCompleted(result) {
        //     vm.data = result.data.Items;
        //     $scope.gridInstance.refresh();
        // }
        // function dataLoadFailed(response) {
        // }

        // loadData();


    //     $scope.loading = true;
        // $scope.data = [];
    //     $scope.page = 0;
    //     $scope.pageCount = 0;
    //     $scope.search = search;
    //     $scope.clearSearch = clearSearch;
    //     $scope.deleteItem = deleteItem;

    //     function deleteItem(id) {
    //         $ngBootbox.confirm('Bạn có chắc muốn xóa?')
    //             .then(function () {
    //                 var config = {
    //                     params: {
    //                         id: id
    //                     }
    //                 }
    //                 apiService.del('/api/applicationUser/delete', config, function () {
    //                     notificationService.displaySuccess('Đã xóa thành công.');
    //                     search();
    //                 },
    //                 function () {
    //                     notificationService.displayError('Xóa không thành công.');
    //                 });
    //             });
    //     }
    //     function search(page) {
    //         page = page || 0;

    //         $scope.loading = true;
    //         var config = {
    //             params: {
    //                 page: page,
    //                 pageSize: 5,
    //                 filter: $scope.filterExpression
    //             }
    //         }

    //         apiService.get('api/applicationUser/getlistpaging', config, dataLoadCompleted, dataLoadFailed);
    //     }

    //     function dataLoadCompleted(result) {
    //         $scope.data = result.data.Items;
    //         $scope.page = result.data.Page;
    //         $scope.pagesCount = result.data.TotalPages;
    //         $scope.totalCount = result.data.TotalCount;
    //         $scope.loading = false;

    //         if ($scope.filterExpression && $scope.filterExpression.length) {
    //             notificationService.displayInfo(result.data.Items.length + ' items found');
    //         }
    //     }
    //     function dataLoadFailed(response) {
    //         notificationService.displayError(response.data);
    //     }

    //     function clearSearch() {
    //         $scope.filterExpression = '';
    //         search();
    //     }

    //     $scope.search();
    }
})(angular.module('InitiativeManagement.application_users'));