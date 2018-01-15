(function (app) {
    'use strict';

    app.controller('applicationRoleListController', applicationRoleListController);

    applicationRoleListController.$inject = ['$scope','$http', 'authenticationService', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function applicationRoleListController($scope, $http, authenticationService , apiService, notificationService, $ngBootbox, $filter) {
        // $scope.loading = true;
        // $scope.data = [];
        // $scope.page = 0;
        // $scope.pageCount = 0;
        // $scope.search = search;
        // $scope.clearSearch = clearSearch;
        // $scope.deleteItem = deleteItem;
        // $scope.selectAll = selectAll;
        // $scope.deleteMultiple = deleteMultiple;

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

                return $http.get("api/applicationRole/getlistpaging", config)
                    .then(function (response) {
                        return { data: response.data.items, totalCount: response.data.totalCount };
                    }, function (response) {
                        return { data: [], totalCount: 0 };
                    });
            }
        });

        //
        // grid
        var gridInstance = null;

        $scope.dataGridOptions = {
            dataSource: {
                store: orders
            },
            onInitialized: function(e) {
                gridInstance = e.component;
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
                dataField: "Name",
                caption: "Tên quyền",
                alignment: "center"
            },{
                dataField: "Description",
                caption: "Mô tả",
                alignment: "center"
            },{
                dataField: "IsDeactive",
                caption: "Trạng thái",
                cellTemplate: "cellTemplate",
                alignment: "center"
            }],
            bindingOptions: {
                rowAlternationEnabled: 'true',
                showRowLines: 'false',
                showBorders: 'true',
                showColumnLines: 'true'
            }
        };

        $scope.textBox = {
            search: {
                width: 300,
                value:"",
                showClearButton: true,
                placeholder: "Tìm kiếm...",
                onValueChanged: function(data) {
                    $scope.textBox.search.value = data.value;
                    gridInstance.refresh();
                }
            },
        };


        // function deleteMultiple() {
        //     var listId = [];
        //     $.each($scope.selected, function (i, item) {
        //         listId.push(item.Id);
        //     });
        //     var config = {
        //         params: {
        //             checkedList: JSON.stringify(listId)
        //         }
        //     }
        //     apiService.del('api/applicationRole/deletemulti', config, function (result) {
        //         notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
        //         search();
        //     }, function (error) {
        //         notificationService.displayError('Xóa không thành công');
        //     });
        // }

        // $scope.isAll = false;
        // function selectAll() {
        //     if ($scope.isAll === false) {
        //         angular.forEach($scope.data, function (item) {
        //             item.checked = true;
        //         });
        //         $scope.isAll = true;
        //     } else {
        //         angular.forEach($scope.data, function (item) {
        //             item.checked = false;
        //         });
        //         $scope.isAll = false;
        //     }
        // }

        // $scope.$watch("data", function (n, o) {
        //     var checked = $filter("filter")(n, { checked: true });
        //     if (checked.length) {
        //         $scope.selected = checked;
        //         $('#btnDelete').removeAttr('disabled');
        //     } else {
        //         $('#btnDelete').attr('disabled', 'disabled');
        //     }
        // }, true);

        // function deleteItem(id) {
        //     $ngBootbox.confirm('Bạn có chắc muốn xóa?')
        //         .then(function () {
        //             var config = {
        //                 params: {
        //                     id: id
        //                 }
        //             }
        //             apiService.del('/api/applicationRole/delete', config, function () {
        //                 notificationService.displaySuccess('Đã xóa thành công.');
        //                 search();
        //             },
        //             function () {
        //                 notificationService.displayError('Xóa không thành công.');
        //             });
        //         });
        // }
        // function search(page) {
        //     page = page || 0;

        //     $scope.loading = true;
        //     var config = {
        //         params: {
        //             page: page,
        //             pageSize: 5,
        //             filter: $scope.keyword
        //         }
        //     }

        //     apiService.get('api/applicationRole/getlistpaging', config, dataLoadCompleted, dataLoadFailed);
        // }

        // function dataLoadCompleted(result) {
        //     $scope.data = result.data.Items;
        //     $scope.page = result.data.Page;
        //     $scope.pagesCount = result.data.TotalPages;
        //     $scope.totalCount = result.data.TotalCount;
        //     $scope.loading = false;

        //     if ($scope.filterExpression && $scope.filterExpression.length) {
        //         notificationService.displayInfo(result.data.Items.length + ' được tìm thấy');
        //     }
        // }
        // function dataLoadFailed(response) {
        //     notificationService.displayError(response.data);
        // }

        // function clearSearch() {
        //     $scope.filterExpression = '';
        //     search();
        // }

        // $scope.search();
    }
})(angular.module('InitiativeManagement.application_roles'));