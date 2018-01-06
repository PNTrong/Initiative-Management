(function (app) {
    app.controller('initiativeListViewController', initiativeListViewController);
    initiativeListViewController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter', 'authData', '$timeout'];

    function initiativeListViewController($scope, apiService, notificationService, $ngBootbox, $filter, authData, $timeout) {
        $scope.loading = true;
        $scope.data = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.search = search;
        $scope.deleteItem = deleteItem;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;
       
        $scope.isAdminShow = authData.authenticationData.Role == 'ADMIN' || authData.authenticationData.Role == 'ADVANCEDROLE';
        function deleteMultiple() {
            //
            //Todo:
            return;

            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.Id);
            });
            var config = {
                params: {
                    checkedList: JSON.stringify(listId)
                }
            }
            apiService.del('api/initiative/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.data, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.data, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("data", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteItem(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?')
                .then(function () {
                    var config = {
                        params: {
                            id: id
                        }
                    }
                    apiService.del('/api/initiative/deactive', config, function (res) {
                        if (res.data) {
                            notificationService.displaySuccess('Đã xóa thành công.');
                            search();
                        } else {
                            notificationService.displayError('Xóa không thành công.');
                        }
                    },
                        function () {
                            notificationService.displayError('Xóa không thành công.');
                        });
                });
        }

        $scope.filter = {
            Keyword: '',
            Field: null,
            Time: ''
        }

        function search(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 5,
                    filter: JSON.stringify($scope.filter)
                }
            }
            apiService.get('api/initiative/getlistpaging', config, dataLoadCompleted, dataLoadFailed);
        }

        $scope.reloadGrid = function(){
            $scope.filter = {
                Keyword: '',
                Field: null,
                Time: ''
            }
            search(0);
        }

        $scope.export = function () {
            $scope.loading = true;
            var config = {
                responseType: 'blob',
                params: {
                    filter: JSON.stringify($scope.filter)
                }
            }
            apiService.get('api/initiative/export',config,downloadFileComplete,downloadFileFailed);
        }

        $scope.loading = false;
        function downloadFileComplete(response){
                $scope.loading = false;
                var blob = response.data;
                var contentType = response.headers("content-type");
                var fileURL = URL.createObjectURL(blob);
                window.open(fileURL,"_self");
        }

        function downloadFileFailed(){
            $scope.loading = false;
            notificationService.displayError("Xảy ra sự cố khi tải tệp.");
        }

        $scope.isDownloadDisable = true;
        $scope.isSaveDisable = true;
        function dataLoadCompleted(result) {
            $scope.data = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loading = false;
            $scope.isDownloadDisable = result.data.TotalCount == 0;
            if ($scope.filterExpression && $scope.filterExpression.length) {
                notificationService.displayInfo(result.data.Items.length + ' được tìm thấy');
            }
        }
        function dataLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.saveGPA = function(id,value){
            if(!value){
                notificationService.displayInfo("Điểm cao nhất là 100 và thấp nhất là điểm 0.");
                return 0;
            }

            apiService.post('api/initiative/savegpa/'+id+'/'+value, $scope.initiative, function (result) {
                notificationService.displaySuccess("Lưu thành công.");
            }, function () {
                notificationService.displayError('Lưu điểm trung bình không thành công.');
            });
        }

        function loadFields() {
            apiService.get('api/field/getall', null, function (result) {
                $scope.fields = result.data;
                $timeout(function () {
                    $('.load-droplist').selectpicker('destroy');
                    $('.load-droplist').selectpicker('render');
                })
            }, function () {
            });
        }

        loadFields();

        $scope.search();
    }
})(angular.module('InitiativeManagement.initiatives'));