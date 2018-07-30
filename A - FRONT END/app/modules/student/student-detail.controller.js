angular.module("myApp").controller("studentDetailController", function ($scope, filterFilter, FileUploader, $uibModal, $state, $student, $upload, uiService, toastr, statusConstant, genderConstant, roleConstant, appSettings, apiUrls, $stateParams, $attachment, $userRole) {
    //#region Properties

    $scope.img = null;

    // List of status (Active & Deleted)
    $scope.statuses = statusConstant.listStatus;

    // List of gender ( Male & Female)
    $scope.genders = genderConstant.listGender;

    // List of role ( Admin & Normal)
    // $scope.listRoles = roleConstant.listRole;

    // models of list-student.html
    $scope.model = {};

    $scope.roles = [];

    $scope.fileUploader = new FileUploader({
        url : appSettings.endPoint.apiService + '/' + apiUrls.attachment.create
    });

    //#endregion

    //#region Methods


    //init
    $scope.init = function(){
        $scope.listRoles = angular.copy(roleConstant.listRole);
    }
    // Checkbox for roles

    // Helper method to get selected roles
    // $scope.selectedRoles = function selectedRoles() {
    //     return filterFilter($scope.listRoles, { selected: true });
    // };

    // Watch roles for changes
    $scope.$watch('listRoles|filter:{selected:true}', function (nv) {
        $scope.model.roles = nv.map(function (role) {
            return role.id;
        });
    }, true);

    // Get student information

    var id = $stateParams.id;

    if (id != 0 && id != undefined){
        $student.loadStudent({ids : [id]}).then(function (x) {
            $scope.model = x.records[0];

            var data = {
                studentIds : [x.records[0].id]
            };

            // Load user role
            $userRole.loadUserRole(data).then(function (x) {
                if (x.records.length > 0){
                    for (var i = 0; i < x.records.length; i++){
                        if (x.records[i].roleId == $scope.listRoles[i].id){

                            // Helper method to get selected roles
                            $scope.listRoles[i].selected = true;

                            //$scope.listRoles[i].selected = false;
                        }
                    }
                }
            });

            // Load attachment
            $attachment.loadAttachment(data).then(function (x) {
                if (x.records.length > 0){
                    $attachment.getAttachment(x.records[0].id).then(function (y) {
                        $scope.imgName = y.name + "." + y.type;
                        $scope.img = 'data:image/jpg;base64,' + y.content;
                    });
                }
            })
        });

        // Download img
        $scope.onclick = function() {
            var img = document.getElementById('picture').getAttribute('src');
            // atob to base64_decode the data-URI
            var image_data = atob(img.split(',')[1]);
            // Use typed arrays to convert the binary data to a Blob
            var arraybuffer = new ArrayBuffer(image_data.length);
            var view = new Uint8Array(arraybuffer);
            for (var i=0; i<image_data.length; i++) {
                view[i] = image_data.charCodeAt(i) & 0xff;
            }
            try {
                // This is the recommended method:
                var blob = new Blob([arraybuffer], {type: 'application/octet-stream'});
            } catch (e) {
                // The BlobBuilder API has been deprecated in favour of Blob, but older
                // browsers don't know about the Blob constructor
                // IE10 also supports BlobBuilder, but since the `Blob` constructor
                //  also works, there's no need to add `MSBlobBuilder`.
                var bb = new (window.WebKitBlobBuilder || window.MozBlobBuilder);
                bb.append(arraybuffer);
                var blob = bb.getBlob('application/octet-stream'); // <-- Here's the Blob
            }

            // Use the URL object to create a temporary URL
            var url = (window.webkitURL || window.URL).createObjectURL(blob);

            var a = document.createElement("a");
            document.body.appendChild(a);

            a.href = url;
            a.download = $scope.imgName;
            a.click();
            window.URL.revokeObjectURL(url);
        };

        // Edit student
        $scope.clickEdit = function () {
            if ($scope.studentForm.$invalid) {
                return
            }
            $student.editStudent(id, $scope.model).then(function (x) {
                toastr.success('Edit student successfully');
            });
        };
    };

    $scope.fileUploader.onAfterAddingFile = function(fileItem) {
        $scope.document = fileItem._file;
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
                    toastr.success('Create student successfully');
                });
            }
            else toastr.success('Create student successfully');

        });
    };

    //#endregion
});