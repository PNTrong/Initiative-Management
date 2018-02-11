(function (app) {
    app.controller('projectListController', projectListController);

    projectListController.$inject = ['$scope','$http','authenticationService',
    'apiService', 'notificationService', '$ngBootbox', '$filter','commonService'];

    function projectListController($scope,$http,authenticationService, apiService, notificationService, $ngBootbox, $filter,commonService) {

        // variables
        $scope.gridInstance = {};
        $scope.fields = [];
        $scope.selectedRowKeys = [];
        var monthAgo = commonService.getMonthAgo();
        var today = moment();
       
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
            StartDate: monthAgo,
            EndDate: today,
            AccountId: ""
        }

        $scope.visiblePopup = false;

        $scope.filterButton = {
            icon: 'filter',
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
            icon: 'filter',
            text: 'Lọc',
            onClick: function (e) {
                $scope.visiblePopup = false;
                $scope.filterOption.StartDate = commonService.formatTime($scope.filterOption.StartDate);
                $scope.filterOption.EndDate = commonService.formatTime($scope.filterOption.EndDate);
                $scope.gridInstance.refresh();
            }
        }

        $scope.isFieldDisabled = true;
        $scope.fieldGroupSelectBox = {
            displayExpr: 'Name',
            placeholder:'Chọn giá trị',
            valueExpr: 'Id',
            width: 300,
            showClearButton: true,
            bindingOptions: {
                dataSource: 'fieldGroups',
                value: 'filterOption.FieldGroupId',
            }, 
            onValueChanged: function (e) {
                $scope.isFieldDisabled = true;
                loadFields(e.value);
            }
        }

        $scope.compareDateFrom = function (e) {
            var result = commonService.compareDateTime(e.value, $scope.filterOption.EndDate);
            console.log(result);
            if(result == 1) {
                // $scope.filterOption.StartDate = $scope.filterOption.EndDate;
            }
        }

        $scope.compareDateTo = function (e) {
            var result = commonService.compareDateTime($scope.filterOption.StartDate, e.value);
            console.log(result);
            if(result == 1) {
                // $scope.filterOption.EndDate = $scope.filterOption.StartDate;
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
                // console.log(parameters);
                
                authenticationService.setHeader();

                return $http.get("api/initiative/getlistpaging", config)
                    .then(function (response) {
                        return { data: response.data.items, totalCount: response.data.totalCount };
                    }, function (response) {
                        return { data: [], totalCount: 0 };
                    });
            },
            key:"Id",
        });

        $scope.dataGridOptions = {
            onInitialized: function(e) {
                $scope.gridInstance = e.component;
              },
            dataSource: {
                store: orders
            },
            selection: {
                mode: "multiple"
            },
            onSelectionChanged: function(selectedItems) {
                console.log(selectedItems);
                $scope.selectedRowKeys = selectedItems.selectedRowKeys;
                console.log($scope.selectedRowKeys);
                // selectedItems.currentSelectedRowKeys.forEach(item => {
                //     if(!$scope.selectedRowKeys.includes(item)) {
                //         $scope.selectedRowKeys.push(item);
                //     }
                // });

                // selectedItems.currentDeselectedRowKeys.forEach(item => {
                //     if($scope.selectedRowKeys.includes(item)) {
                //         const index = $scope.selectedRowKeys.indexOf(item);
                //         $scope.selectedRowKeys.splice(index, 1);
                //     }
                // });
            },
            remoteOperations: {
                sorting: false,
                paging: true
            },
            paging: {
                pageSize: 2
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
                dataField: "Field.FieldName",
                caption: "Lĩnh Vực",
                alignment: "center"
            },
            {
                dataField: "ApplicationUser.UserName",
                caption: "Đơn vị",
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
            allowColumnResizing: true,
            columnChooser: {
                enabled: true
            },
            columnFixing: { 
                enabled: true
            },
            allowColumnReordering: true,
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

        $scope.export = function () {
            if($scope.selectedRowKeys.length == 0) {
                DevExpress.ui.dialog.alert('Vui lòng chọn sáng kiến trước khi xuất tệp', 'CẢNH BÁO');
                return;
            }
            $scope.loading = true;
            var config = {
                responseType: 'blob',
                params: {
                    ids: JSON.stringify($scope.selectedRowKeys)
                }
            }
            apiService.get('api/initiative/export',config,downloadFileComplete,downloadFileFailed);
        }

        function downloadFileComplete(response){
            var blob = response.data;
            var contentType = response.headers("content-type");
            var fileURL = URL.createObjectURL(blob);
            window.open(fileURL,"_self");
        }

        function downloadFileFailed(){
        }

        // load data
        loadFieldGroups();
    }
})(angular.module('InitiativeManagement.projects')); 