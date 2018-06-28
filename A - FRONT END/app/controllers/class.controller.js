angular.module("myApp").controller("classController", function ($scope, $uibModal, $state, $class, $specialized, uiService, toastr, statusConstant) {

    //#region Properties

    // List of status (Active & Deleted)
    $scope.statuses = statusConstant.listStatus;

    // models of specialized.html
    $scope.model = {};

    $scope.classes = [];

    $scope.modals = {
        class : null
    };

    //#endregion

    //#region Methods

    var info = {};

    // Load specialized from db
    $specialized.loadSpecialized(info).then(function (x) {
        $scope.specializeds = x;
    });

    // Search class
    $scope.search = function () {
        var info = {
            names : $scope.model.name != null ? [$scope.model.name] : null,
            statuses : $scope.model.status != null ? [$scope.model.status] : null,
        };

        $class.loadClass(info).then(function (x) {
            $scope.classes = x;
            $scope.totalItems = $scope.classes.records.length;
            $scope.maxSize = 3;
            $scope.currentPage = 1;
            $scope.itemsPerPage = 3;
            $scope.loading = false;

            $scope.$watch("currentPage", function() {
                setPagingData($scope.currentPage);

            });
            function setPagingData(page) {
                var pagedData = $scope.classes.records.slice((page - 1) * $scope.itemsPerPage, page * $scope.itemsPerPage);
                $scope.items = pagedData;
            }
        });
    };

    // Reset model binding
    $scope.reset = function () {
        $scope.model = {
            name: null,
            status: null
        };
    };

    // Show popup when click 'Add' button
    $scope.displayModal = function (templateUrl, size) {
        return uiService.displayModal(templateUrl, $scope, size);
    };

    //#endregion
});