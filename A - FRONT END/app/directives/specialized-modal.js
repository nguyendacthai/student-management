var myApp = angular.module("myApp");

myApp.directive('specializedModal', function () {
    return {
        templateUrl: '/directives/specialized-modal.html',
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
        controller: function ($scope, statusConstant) {
            // List of status (Active & Deleted)
            $scope.statuses = statusConstant.listStatus;

            // Model which is for 2-way data binding.
            $scope.model = {
                id: $scope.ngInfo.id,
                name: $scope.ngInfo.name,
                status : $scope.ngInfo.status
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
            $scope.clickOk = function(){

                if ($scope.specializedForm.$invalid) {
                    return
                }

                $scope.ngClickOk({specialized: $scope.model});
            }
        }
    }
});