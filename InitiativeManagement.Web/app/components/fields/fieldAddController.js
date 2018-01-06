(function (app) {
    app.controller('fieldAddController', fieldAddController);
    fieldAddController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'commonService','$timeout'];

    function fieldAddController($scope, apiService, notificationService, $location, commonService,$timeout) {
        $scope.field = {
            Id: 0,
            FieldGroupId:0
        }

        $scope.addField = addField;

        function addField() {
            apiService.post('/api/field/add', $scope.field, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.field.FieldName + ' đã thêm thành công');

            $location.url('fields');
        }

        function addFailed(response) {
            notificationService.displayError(response.data.Message);
        }

        function loadFieldGroups() {
           
            apiService.get('api/fieldGroup/getall', null, function (result) {
                $scope.fieldGroups = result.data;
                $timeout(function () {
                    $('.load-droplist').selectpicker('destroy');
                    $('.load-droplist').selectpicker('render');
                })
            }, function () {
                console.log('Cannot get list parent');
            })
        }

        //$scope.renderUI = function () {
        //    $timeout(function () {
        //        $('.load-droplist').selectpicker('destroy');
        //        $('.load-droplist').selectpicker('render');
            
               
        //    })
        //}
        loadFieldGroups();
    }
})(angular.module('InitiativeManagement.fields'));