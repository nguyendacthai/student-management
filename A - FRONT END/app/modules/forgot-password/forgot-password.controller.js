angular.module("myApp").controller("forgotPasswordController", function ($scope, genderConstant, toastr, $user, $state, appSettings, apiUrls,) {
    //#region Properties

    // models of forgot-password.html
    $scope.model = {};

    //#endregion

    //#region Methods

    $scope.clickSubmit = function () {
        if ($scope.forgotForm.$invalid) {
            return
        }
        $user.forgotPassword($scope.model).then(function () {
            toastr.success('Done');
            $state.go('loginState');
        })

    }

    //#endregion
});