angular.module("myApp").controller("studentDetailController", function ($scope, FileUploader, $uibModal, $state, $student, $upload, uiService, toastr, statusConstant, genderConstant, appSettings, apiUrls, $stateParams, $attachment) {
    //#region Properties

    $scope.img = null;

    // List of status (Active & Deleted)
    $scope.statuses = statusConstant.listStatus;

    // List of gender ( Male & Female)
    $scope.genders = genderConstant.listGender;

    // models of list-student.html
    $scope.model = {};

    $scope.fileUploader = new FileUploader({
        url : appSettings.endPoint.apiService + '/' + apiUrls.attachment.create
    });

    //#endregion

    //#region Methods

    // Get student information

    var id = $stateParams.id;
    if (id != 0 && id != undefined){
        $student.loadStudent({ids : [id]}).then(function (x) {
            $scope.model = x.records[0];

            var data = {
                studentIds : [x.records[0].id]
            };

            $attachment.loadAttachment(data).then(function (x) {
                $attachment.getAttachment(x.records[0].id).then(function (y) {
                    $scope.img = 'data:image/png;base64,' + y.content;
                })
            })
        });
    };

    $scope.fileUploader.onAfterAddingFile = function(fileItem) {
        $scope.document = fileItem._file;
        console.log(fileItem._file);
        console.info('onAfterAddingFile', fileItem);
    };

    // Reset model binding
    $scope.clickReset = function () {
        $scope.model = {
            username: null,
            fullname: null,
            password: null,
            phone: null,
            gender: null
        };
    };
    
    // Create student
    $scope.clickSave = function () {
        if ($scope.studentForm.$invalid) {
            return
        }
        $student.createStudent($scope.model).then(function (x) {
            if ($scope.document != null){
                $upload.upload(x.id, $scope.document).then(function () {
                    toastr.success('Create class successfully');
                });
            }
            else toastr.success('Create class successfully');

        });
    };

    // Edit student
    $scope.clickEdit = function () {
        if ($scope.studentForm.$invalid) {
            return
        }
        $student.editStudent(id, $scope.model).then(function (x) {
            toastr.success('Edit class successfully');
        });
    };


    //#endregion
});