angular.module("myApp").controller("listStudentController", function ($scope, $window,$uibModal, $state, $student, uiService, toastr, statusConstant, genderConstant) {
    //#region Properties

    // List of status (Active & Deleted)
    $scope.statuses = statusConstant.listStatus;

    // List of gender ( Male & Female)
    $scope.genders = genderConstant.listGender;

    // models of list-student.html
    $scope.model = {};

    $scope.students = [];

    //#endregion

    //#region Methods

    // Reset model binding
    $scope.reset = function () {
        $scope.model = {
            username: null,
            fullname: null,
            phone: null,
            gender: null,
            status: null
        };
    };

    // Search student
    $scope.search = function () {
        var data = {
            usernames : $scope.model.username != null ? [$scope.model.username] : null,
            fullnames : $scope.model.fullname != null ? [$scope.model.fullname] : null,
            phones : $scope.model.phone != null ? [$scope.model.phone] : null,
            genders : $scope.model.gender != null ? [$scope.model.gender] : null,
            statuses : $scope.model.status != null ? [$scope.model.status] : null
        };

        $student.loadStudent(data).then(function (x) {
            $scope.students = x;
            $scope.totalItems = $scope.students.records.length;
            $scope.maxSize = 3;
            $scope.currentPage = 1;
            $scope.itemsPerPage = 3;
            $scope.loading = false;

            $scope.$watch("currentPage", function() {
                setPagingData($scope.currentPage);

            });
            function setPagingData(page) {
                var pagedData = $scope.students.records.slice((page - 1) * $scope.itemsPerPage, page * $scope.itemsPerPage);
                $scope.items = pagedData;
            }
        });
    };

    // Edit Student
    $scope.editStudent = function (id) {
        $state.go('edit-student', {
            id: id //selectedItem and id is defined
        });
    };

    //#endregion
});