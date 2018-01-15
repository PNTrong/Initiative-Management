(function (app) {
    'use strict';

    app.controller('applicationGroupListController', applicationGroupListController);

    applicationGroupListController.$inject = ['$scope', 'authenticationService', 'apiService', 'notificationService', '$ngBootbox', '$filter', '$http'];

    function applicationGroupListController($scope, authenticationService, apiService, notificationService, $ngBootbox, $filter, $http) {

        var gridInstance = null;
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

        // grid
        var orders = new DevExpress.data.CustomStore({
            load: function (loadOptions) {
                var parameters = {};

                parameters.skip = loadOptions.skip || 0;
                parameters.take = loadOptions.take || 10;
                parameters.filter = $scope.textBox.search.value;
                
                console.log(parameters);

                var config = {
                    params: parameters
                };

                authenticationService.setHeader();

                return $http.get("api/applicationGroup/getlistpaging", config)
                    .then(function (response) {
                        return { data: response.data.items, totalCount: response.data.totalCount };
                    }, function (response) {
                        return { data: [], totalCount: 0 };
                    });
            }
        });

        $scope.dataGridOptions = {
            onInitialized: function(e) {
                gridInstance = e.component;
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
                caption: "Tên nhóm quyền",
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
            },
            {
                dataField: "ID",
                caption: "",
                cellTemplate: "actionCellTemplate",
                alignment: "center",
                width: 75
            }
        ],
            bindingOptions: {
                rowAlternationEnabled: 'true',
                showRowLines: 'false',
                showBorders: 'true',
                showColumnLines: 'true'
            }
        };

    }
})(angular.module('InitiativeManagement.application_groups'));