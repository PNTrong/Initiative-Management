(function (app) {
    app.controller('initiativeAddController', initiativeAddController);

    initiativeAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function initiativeAddController(apiService, $scope, notificationService, $state, commonService) {
        // ckeditor config
        $scope.editorOptions = {
            languague: 'vi',
            height: '50px'
        }

        // author
        $scope.authors = [{
            'fname': '',
            'birthDay': '',
            'organizationId': '',
            'position':'',
            'qualitification': '',
            'contributionRate':''
        }];

        $scope.addNew = function (personalDetail) {
            $scope.authors.push({
                'fname': '',
                'birthDay': '',
                'organizationId': '',
                'position':'',
                'qualitification': '',
                'contributionRate':''
            });
        };

        $scope.remove = function () {
            var newDataList = [];
            $scope.selectedAll = false;
            angular.forEach($scope.authors, function (selected) {
                if (!selected.selected) {
                    newDataList.push(selected);
                }
            });
            $scope.authors = newDataList;
        };

        $scope.checkAll = function () {
            angular.forEach($scope.authors, function (author) {
                author.selected = $scope.selectedAll;
            });
        };

        // end-author

    }
})(angular.module('InitiativeManagement.initiatives')); 