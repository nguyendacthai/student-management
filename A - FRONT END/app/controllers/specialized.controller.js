angular.module("myApp").controller("specializedController", function ($scope, $uibModal, $state, $specialized, uiService, toastr, statusConstant) {

    //#region Properties

    // List of status (Active & Deleted)
    $scope.statuses = statusConstant.listStatus;

    // models of specialized.html
    $scope.model = {};

    // Specialized's array from db
    $scope.specializeds = [];

    $scope.modals = {
        specialized : null
    };

    // Information which is for binding to initiator directive.
    $scope.info = {
        id : null,
        name : null,
        status : null
    };

    //#endregion

    //#region Methods

    // Search specialized
    $scope.search = function () {
        var info = {
            names : $scope.model.name != null ? [$scope.model.name] : null,
            statuses : $scope.model.status != null ? [$scope.model.status] : null,
        };

        $specialized.loadSpecialized(info).then(function (x) {
            $scope.specializeds = x;

            $scope.totalItems = $scope.specializeds.records.length;
            $scope.maxSize = 3;
            $scope.currentPage = 1;
            $scope.itemsPerPage = 3;
            $scope.loading = false;

            $scope.$watch("currentPage", function() {
                setPagingData($scope.currentPage);

            });
            function setPagingData(page) {
                var pagedData = $scope.specializeds.records.slice((page - 1) * $scope.itemsPerPage, page * $scope.itemsPerPage);
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

    // Show popup when edit specialized
    $scope.editSpecialized = function (id) {
        var data = {
            ids : [id]
        };

        $specialized.loadSpecialized(data).then(function (x) {
            $scope.info.id = id;
            $scope.info.name = x.records[0].name;
            $scope.info.status = x.records[0].status;

            // Display editor.
            $scope.modals.specialized = $scope.displayModal('specialized-modal.html', 'md');
        });
    }

    // Create or update specialized
    $scope.initSpecialized = function (info) {

        if (info.id == null || info.id < 1){
            $specialized.createSpecialized(info).then(function () {
                toastr.success('Create specialized successfully');

                $scope.search();
            });
        }
        else {
            $specialized.editSpecialized(info.id, info).then(function () {
                toastr.success('Edit specialized successfully');

                $scope.search();
            });
        }

        // close popup
        $scope.modals.specialized.dismiss();
        $scope.modals.specialized = null;
    };

    //#endregion
});