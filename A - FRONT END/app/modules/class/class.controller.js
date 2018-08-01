angular.module("myApp").controller("classController", function ($scope, $uibModal, $state, $class, $specialized, uiService, toastr, statusConstant) {

    //#region Properties

    // List of status (Active & Deleted)
    $scope.statuses = statusConstant.listStatus;

    // models of specialized.html
    $scope.model = {};

    $scope.classes = [];

    $scope.modals = {
        class: null
    };

    // Information which is for binding to initiator directive.
    $scope.info = {
        id: null,
        specialized: null,
        name: null,
        status: null,
        specializedId: null
    };

    //#endregion

    //#region Methods

    var param = {};

    // Load specialized from db
    $specialized.loadSpecialized(param).then(function (x) {
        $scope.specializeds = x;
    });

    // Search class
    $scope.search = function () {
        var data = {
            names: $scope.model.name != null ? [$scope.model.name] : null,
            statuses: $scope.model.status != null ? [$scope.model.status] : null,
            specializedIds: $scope.model.specialized != null ? [$scope.model.specialized] : null,
        };

        $class.loadClass(data).then(function (x) {
            $scope.classes = x;
            $scope.totalItems = $scope.classes.records.length;
            $scope.maxSize = 3;
            $scope.currentPage = 1;
            $scope.itemsPerPage = 3;
            $scope.loading = false;

            $scope.$watch("currentPage", function () {
                setPagingData($scope.currentPage);

            });

            function setPagingData(page) {
                var pagedData = $scope.classes.records.slice((page - 1) * $scope.itemsPerPage, page * $scope.itemsPerPage);
                $scope.items = pagedData;
                // for (var i = 0 ; i < $scope.items.length; i ++){
                //     debugger
                //     var data = {
                //         ids : [$scope.items[i].specializedId]
                //     }
                //     $specialized.loadSpecialized(data).then(function (x) {
                //         console.log(x);
                //         //console.log(x.records[i].name);
                //         //$scope.items.push(x.records[i].name);
                //     })
                // }
                //console.log($scope.items);
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

    // Show popup when edit class
    $scope.editClass = function (id) {
        var data = {
            ids: [id]
        };

        $class.loadClass(data).then(function (x) {
            $scope.info.id = id;
            $scope.info.name = x.records[0].name;
            $scope.info.status = x.records[0].status;
            $scope.info.specialized = x.records[0].specializedId;

            // Display editor.
            $scope.modals.class = $scope.displayModal('class-modal.html', 'md');
        });
    };

    // Create or update class
    $scope.initClass = function (info) {
        info.specializedId = info.specialized;

        if (info.id == null || info.id < 1) {
            $class.createClass(info).then(function () {
                toastr.success('Create class successfully');

                $scope.search();
            });
        }
        else {
            $class.editClass(info.id, info).then(function () {
                toastr.success('Edit class successfully');

                $scope.search();
            });
        }

        // close popup
        $scope.modals.class.dismiss();
        $scope.modals.class = null;
    };

    //#endregion
});