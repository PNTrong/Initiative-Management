(function (app) {
    app.controller('projectListController', projectListController);

    projectListController.$inject = ['$scope','$http','authenticationService',
    'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function projectListController($scope,$http,authenticationService, apiService, notificationService, $ngBootbox, $filter) {

        // variables
        $scope.gridInstance = {};
        $scope.fields = [];
       
        // search box
        $scope.textBox = {
            search: {
                width: 300,
                value:"",
                showClearButton: true,
                placeholder: "Tìm kiếm...",
                onValueChanged: function(data) {
                    $scope.filterOption.Keyword = data.value;
                    $scope.gridInstance.refresh();
                }
            },
        };

        // filter
        $scope.filterOption = {
            Skip: 0,
            Take: 10,
            FieldId: -1,
            FieldGroupId: -1,
            StartDate: new Date(),
            EndDate: new Date(),
            AccountId: -1
        }

        $scope.visiblePopup = false;

        $scope.filterButton = {
            icon: 'plus',
            text: 'Lọc',
            onClick: function (e) {
                $scope.visiblePopup = true;
            }
        }

        $scope.filterPopup = {
            width: 340,
            height: 435,
            contentTemplate: "info",
            showTitle: true,
            title: "Lọc danh sách",
            dragEnabled: false,
            closeOnOutsideClick: false,
            bindingOptions: {
                visible: "visiblePopup",
            }
        };

        $scope.addFilterButtonOptions = {
            icon: 'plus',
            text: 'Lọc',
            onClick: function (e) {
                $scope.visiblePopup = false;
                $scope.gridInstance.refresh();
                $scope.filterOption = {
                    Skip: 0,
                    Take: 10,
                    FieldId: -1,
                    FieldGroupId: -1,
                    StartDate: new Date(),
                    EndDate: new Date(),
                    AccountId: -1
                }
            }
        }

        $scope.isFieldDisabled = true;
        $scope.fieldGroupSelectBox = {
            displayExpr: 'Name',
            placeholder:'Chọn giá trị',
            valueExpr: 'Id',
            width: 300,
            bindingOptions: {
                dataSource: 'fieldGroups'
            }, 
            onValueChanged: function (e) {
                $scope.isFieldDisabled = true;
                loadFields(e.value);
            }
        }

        // grid
        var orders = new DevExpress.data.CustomStore({
            load: function (loadOptions) {
                var parameters = {};
                $scope.filterOption.Skip = loadOptions.skip || 0;
                $scope.filterOption.Take = loadOptions.take || 10;
                parameters.filter = $scope.filterOption;
                var config = {
                    params: parameters
                };
                console.log(loadOptions);
                
                authenticationService.setHeader();

                return $http.get("api/initiative/getlistpaging", config)
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
                dataField: "Title",
                caption: "Tên Sáng Kiến",
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

        //
        // function
        function loadFieldGroups() {
            apiService.get('api/fieldGroup/getall', null, function (result) {
                $scope.fieldGroups = result.data;
            }, function () {
                $scope.fieldGroups = [];
            });
        }

        function loadFields(id) {
            apiService.get('api/field/getbygroupid/' + id, null, function (result) {
                $scope.fields = result.data;
                $scope.isFieldDisabled = false;
            }, function () {
                $scope.fields = [];
            });
        }

        // load data
        loadFieldGroups();
    }
})(angular.module('InitiativeManagement.projects')); 