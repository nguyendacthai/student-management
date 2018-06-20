var myApp = angular.module("myApp");
myApp.controller("specializedController", function ($scope, $uibModal, $state, $specialized, statusConstant) {

    $scope.statuses = statusConstant.listStatus;

    $scope.model = {};

    $scope.specializedModel = { name: ''};
    
    $scope.search = function () {
        
    }

    // Reset model binding
    $scope.reset = function () {
        $scope.model = {
            name: null,
            status: null
        };
    }

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
        $specialized.createSpecialized($scope.specializedModel.name).then(function (x) {
            console.log(x);

            $scope.specializedModel.name = '';

            $scope.cancelModal.dismiss();
        });
    }
});