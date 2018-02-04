(function (app) {
    app.controller('projectAddController', projectListController);

    projectListController.$inject = ['authenticationService', '$scope', '$window', '$rootScope',
        'apiService', '$location', '$timeout'];

    function projectListController(authenticationService, $scope, $window, $rootScope, apiService, $location, $timeout) {

        // variables

        var defaultMaxLength = 15352 // 1 page have 3838 characters;
        $scope.fieldGroups = [];
        $scope.fields = [];


        // load data
        loadFieldGroups();
        loadUsers();

        // name
        var filterTextTimeout;
        $scope.nameTextBox = {
            placeholder: "Nhập dữ liệu...",
            height:40,
            onChange: function (data) {
                $scope.similarNames = [];
                $scope.similarNameVisible = false;
                if (filterTextTimeout) {
                    $timeout.cancel(filterTextTimeout);
                }

                filterTextTimeout = $timeout(function () {
                    if($scope.initiative.Title.length === 0) { return; }
                    getTitleNames($scope.initiative.Title);
                }, 300); // delay 250 ms
            },
            showClearButton: true,
            bindingOptions: {
                value: 'initiative.Title'
            }
        }

        $scope.similarNameVisible = false;

        $scope.similarNamePopup = {
            width: 600,
            height: 400,
            contentTemplate: "similarNameInfo",
            showTitle: true,
            title: "CẢNH BÁO",
            dragEnabled: false,
            closeOnOutsideClick: true,
            bindingOptions: {
                visible: "similarNameVisible",
            }
        };

        $scope.confirmButton = {
            text: 'Chấp nhận',
            onClick: function (e) {
                $scope.similarNameVisible = false;
            }
        }

        $scope.cancelButton = {
            text: 'Hủy bỏ',
            onClick: function (e) {
                $scope.similarNameVisible = false;
                // reset name
                $scope.initiative.Title = "";
            }
        }

        // Find Name
        $scope.similarNames = [];
        function getTitleNames(name) {
            apiService.get('api/initiative/getnames?name=' + name, null, function (result) {
                $scope.similarNames = result.data;
                if ($scope.similarNames.length == 0){ return;}
                $scope.similarNameVisible = true;
                console.log($scope.similarNames);
            }, function () {
                $scope.similarNames = [];
            });
        }

        // Fields
        $scope.isFieldDisabled = true;

        $scope.fieldGroupSelectBox = {
            displayExpr: 'Name',
            placeholder: 'Chọn giá trị',
            valueExpr: 'Id',
            width: 300,
            height:40,
            noDataText: "Không có dữ liệu để hiển thị",
            bindingOptions: {
                dataSource: 'fieldGroups',
                value: 'initiative.FieldGroupId',
            },
            onValueChanged: function (e) {
                $scope.isFieldDisabled = true;
                loadFields(e.value);
            }
        }

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

        // Unit
        function loadUsers() {
            apiService.get('api/account/users', null, function (result) {
                if (result.data) {
                    $scope.users = result.data;
                }
            }, function () {
                console.log('Cannot get users');
            });
        }

        // author
        $scope.authors = [];
        $scope.addNewAuthor = function () {
            $scope.authors.push();
        }

        $scope.deleteAuthor = function (id) {
            $scope.authors = $scope.authors.filter(item => item.Id != id);
        }

        // popup
        $scope.visiblePopup = false;
        $scope.addAuthorButton = {
            icon: 'plus',
            text: 'Thêm mới',
            onClick: function (e) {
                $scope.visiblePopup = true;
            }
        }
        $scope.addAuthorPopup = {
            width: 600,
            height: 410,
            contentTemplate: "info",
            showTitle: true,
            title: "Tác giả",
            dragEnabled: false,
            closeOnOutsideClick: false,
            bindingOptions: {
                visible: "visiblePopup",
            }
        };

        var formInstance;
        $scope.formData = {
            'Id': 0,
            'FullName': '',
            'BirthDay': '',
            'OrganizationID': '',
            'Position': '',
            'Qualitification': '',
            'ContributionRate': ''
        };
        $scope.formOptions = {
            formData: $scope.formData,
            readOnly: false,
            showColonAfterLabel: true,
            showValidationSummary: false,
            validationGroup: "customerData",
            bindingOptions: {
                formData: "formData",
            },
            onInitialized: function (e) {
                formInstance = e.component;
            },
            items: [{
                dataField: "FullName",
                label: {
                    text: "Họ và Tên"
                },
                validationRules: [{
                    type: "required",
                    message: "Vui lòng nhập họ và tên"
                }]
            }, {
                dataField: "BirthDay",
                label: {
                    text: "Ngày sinh"
                },
                editorType: "dxDateBox",
                editorOptions: {
                    showClearButton: true,
                    pickerType: "rollers",
                    type: "date",
                    max: new Date(),
                    min: new Date(1900, 0, 1),
                },
                validationRules: [{
                    type: "required",
                    message: "Vui lòng nhập ngày sinh"
                }]
            },
            {
                dataField: "OrganizationID",
                label: {
                    text: "Nơi công tác (hoặc nơi thường trú)"
                },
                validationRules: [{
                    type: "required",
                    message: "Vui lòng nhập thông tin"
                }]
            },
            {
                dataField: "Position",
                label: {
                    text: "Chức danh"
                },
                validationRules: [{
                    type: "required",
                    message: "Vui lòng nhập chức danh"
                }]
            },
            {
                dataField: "Qualitification",
                label: {
                    text: "Trình độ chuyên môn"
                },
                validationRules: [{
                    type: "required",
                    message: "Vui lòng nhập vào thông tin"
                }]
            },
            {
                dataField: "ContributionRate",
                label: {
                    text: "Tỷ lệ đóng góp"
                },
                validationRules: [{
                    type: "required",
                    message: "Vui lòng nhập vào thông tin"
                }]
            }
            ]
        };

        $scope.addAuthorButtonOptions = {
            text: "Tạo mới",
            type: "success",
            useSubmitBehavior: true,
            validationGroup: "customerData"
        };

        $scope.onFormPopupSubmit = function (e) {
            console.log(e);
            $scope.formData.Id = $scope.authors.length + 1;
            $scope.authors.push($scope.formData);
            $scope.visiblePopup = false;
            $scope.formData = {
                'FullName': '',
                'BirthDay': '',
                'OrganizationID': '',
                'Position': '',
                'Qualitification': '',
                'ContributionRate': ''
            };
        };

        //initiative
        $scope.initiative = {
            Id: 0,
            Title: "",
            SendTo: "",
            Investor: "",
            FieldId: null,
            FieldGroupId: null,
            DeploymentTime: null,
            KnowSolutionContent: "",
            ImprovedContent: "",
            ConditionApply: "",
            ActionSteps: "",
            InitiativeApplicability: "",
            SecurityInformation: "",
            AchievedByAuthors: "",
            AchievedByAnothers: "",
            AuthorGroupId: null,
            AppraisalBoardCommnetId: null,
            AccountId: null,
            Authors: [],
        }

        $scope.addInitiativeButtonOptions = {
            width: 100,
            height: 40,
            text: "Nộp đơn",
            type: "success",
            useSubmitBehavior: true
        };

        $scope.addInitiative = function addInitiative(e) {
            if ($scope.authors.length === 0) {
                DevExpress.ui.dialog.alert('Tác giả (nhóm tác giả) đề nghị xét công nhận sáng kiến bắt buộc nhập', 'CẢNH BÁO');
                return;
            }

            var contentLength = $scope.initiative.KnowSolutionContent.length + 
            $scope.initiative.ImprovedContent.length + 
            $scope.initiative.ConditionApply.length + 
            $scope.initiative.ActionSteps.length + 
            $scope.initiative.InitiativeApplicability.length + 
            $scope.initiative.SecurityInformation.length;

            if (contentLength > defaultMaxLength) {
                DevExpress.ui.dialog.alert('Nội dung Mô Tả Bản Chất Của Sáng Kiến không được quá 8000 ký tự. Số ký tự đang nhập là '+ contentLength, 'CẢNH BÁO');
                return;
            }

            $scope.initiative.Authors = $scope.authors;
            console.log($scope.initiative);
            apiService.post('api/initiative/add', $scope.initiative, addSuccessed, addFailed);
        }

        function addInitiative(e) {
            $scope.initiative.Authors = $scope.authors;
            apiService.post('api/initiative/add', $scope.initiative, addSuccessed, addFailed);
        }

        function addSuccessed() {
            // notificationService.displaySuccess('Đã nộp đơn thành công');
            $location.url('sang-kien');
        }

        function addFailed(response) {
            // notificationService.displayError("Nộp đơn thất bại");
        }
        //end-initiative

    }
})(angular.module('InitiativeManagement.projects')); 