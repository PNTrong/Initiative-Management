(function (app) {
    app.controller('fieldGroupListController', fieldGroupListController);

    fieldGroupListController.$inject = ['$scope','$http','authenticationService', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function fieldGroupListController($scope,$http,authenticationService, apiService, notificationService, $ngBootbox, $filter) {
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

                return $http.get("api/fieldGroup/getlistpaging", config)
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
                dataField: "Name",
                caption: "Tên nhóm",
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
                width: 150
            }],
            allowColumnResizing: false,
            bindingOptions: {
                rowAlternationEnabled: 'true',
                showRowLines: 'false',
                showBorders: 'true',
                showColumnLines: 'true',
                columnResizingMode: "widget"
            }
        };

        $scope.deleteItem = function (id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?')
                .then(function () {
                    var config = {
                        params: {
                            id: id
                        }
                    }
                    apiService.del('/api/fieldGroup/delete', config, function () {
                        notificationService.displaySuccess('Đã xóa thành công.');
                        $scope.gridInstance.refresh();
                    },function () {
                            notificationService.displayError('Xóa không thành công.');
                            $scope.gridInstance.refresh();
                        });
                });
        }

    }
})(angular.module('InitiativeManagement.field_groups'));