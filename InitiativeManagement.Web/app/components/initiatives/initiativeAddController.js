(function (app) {
    app.controller('initiativeAddController', initiativeAddController);

    initiativeAddController.$inject = ['apiService', '$scope', 'notificationService', '$state','$location', 'commonService'];

    function initiativeAddController(apiService, $scope, notificationService, $state, $location, commonService) {
        // ckeditor config
        $scope.editorOptions = {
            languague: 'vi',
            height: '50px'
        }

        // author
        $scope.authors = [{
            'FullName': '',
            'BirthDay': null,
            'OrganizationID': '',
            'Position': '',
            'Qualitification': '',
            'ContributionRate': ''
        }];

        $scope.addNewAuthor = function () {
            debugger;
            $scope.authors.push({
                'FullName': '',
                'BirthDay': null,
                'OrganizationId': 0,
                'Position': '',
                'Qualitification': '',
                'ContributionRate': ''
            });
        };

        $scope.removeAuthor = function () {
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

        //Field
        function loadFields() {
            apiService.get('api/field/getall', null, function (result) {
                $scope.fields = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadFields();
        //end Field

        //initiative
        $scope.initiative = {
            Id: 0,
            FielId: 0,
            AuthorGroupId: 0,
            AppraisalBoardCommnetId: 0,
            Authors: []
        }

        $scope.addInitiative = addInitiative;

        function addInitiative() {
            $scope.initiative.Authors = $scope.authors;
            apiService.post('api/initiative/add', $scope.initiative, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess('đã thêm thành công');

            $location.url('initiatives');
        }

        function addFailed(response) {
            notificationService.displayError(response.data.Message);
        }
        //end-initiative

    }
})(angular.module('InitiativeManagement.initiatives')); 