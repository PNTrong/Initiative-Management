(function (app) {
    app.controller('initiativeAddController', initiativeAddController);

    initiativeAddController.$inject = ['apiService', '$scope', 'notificationService', '$state','$location', 'commonService','authData'];

    function initiativeAddController(apiService, $scope, notificationService, $state, $location, commonService,authData) {
        //
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

        //load All Account
        function loadAccounts() {
            apiService.get('api/account/users', null, function (result) {
                if(result.data){
                    $scope.accounts = result.data;
                }
            }, function () {
                console.log('Cannot get users');
            });
        }

        if(authData.authenticationData.Role != "BASEROLE"){
            loadAccounts();
        }
        //initiative
        $scope.initiative = {
            Id: 0,
            FieldId: 0,
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
            notificationService.displaySuccess('Đã nộp đơn thành công');

            $location.url('sang-kien');
        }

        function addFailed(response) {
            notificationService.displayError("Nộp đơn thất bại");
        }
        //end-initiative

    }
})(angular.module('InitiativeManagement.initiatives')); 