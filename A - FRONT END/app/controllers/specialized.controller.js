var myApp = angular.module("myApp");
myApp.controller("specializedController", function ($scope, $uibModal, $state, $specialized, statusConstant) {

    $scope.statuses = statusConstant.listStatus;

    $scope.model = {};

    $scope.specializedModel = { };

    // Search specialized
    $scope.search = function () {
        var info = {
            names : $scope.model.name != null ? [$scope.model.name] : null,
            statuses : $scope.model.status != null ? [$scope.model.status] : null,
        }
        debugger
        $specialized.loadSpecialized(info).then(function (x) {
            console.log(x);
        })
    };

    // Reset model binding
    $scope.reset = function () {
        $scope.model = {
            name: null,
            status: null
        };
    };

    // Show popup
    $scope.add = function(){
        $scope.cancelModal = $uibModal.open({
            size: 'md',
            scope : $scope,
            templateUrl : 'specialized-modal.html',
        });
    };

    // Create new specialized
    $scope.createSpecialized = function (form) {
        if(form.$invalid){
            return;
        }
        $specialized.createSpecialized($scope.specializedModel).then(function (x) {
            console.log(x);

            $scope.specializedModel.name = '';

            $scope.cancelModal.dismiss();
        });
    }
});