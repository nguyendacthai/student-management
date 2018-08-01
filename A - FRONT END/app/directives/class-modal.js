angular.module("myApp").directive('classModal', function () {
    return {
        templateUrl: '/directives/class-modal.html',
        restrict: 'E',
        transclude: {
            panelHeading: '?panelHeading'
        },
        scope: {
            ngTitle: '@',
            ngInfo: '<',
            ngCloseModal: '&',

            ngClickOk: '&',
            ngClickCancel: '&'
        },
        controller: function ($scope, $specialized, statusConstant) {
            // List of status (Active & Deleted)
            $scope.statuses = statusConstant.listStatus;

            // Array of specialized
            $scope.specializeds = [];

            var info = {};

            // Load specialized from db
            $specialized.loadSpecialized(info).then(function (x) {
                $scope.specializeds = x;
            });

            // Model which is for 2-way data binding.
            $scope.model = {
                id: $scope.ngInfo.id,
                name: $scope.ngInfo.name,
                status: $scope.ngInfo.status,
                specialized: $scope.ngInfo.specialized,
                specializedId: null
            };

            /*
            * Callback which is fired when cancel button is clicked.
            * */
            $scope.clickCancel = function () {
                $scope.ngClickCancel();
            };

            /*
            * Callback which is raised when ok button is clicked.
            * */
            $scope.clickOk = function () {

                if ($scope.classForm.$invalid) {
                    return
                }

                $scope.ngClickOk({class: $scope.model});
            }
        }
    }
});