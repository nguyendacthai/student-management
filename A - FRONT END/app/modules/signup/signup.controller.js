angular.module("myApp").controller("signupController", function ($scope, genderConstant,toastr, $user, $state, appSettings, apiUrls,) {
    //#region Properties

    // models of signup.html
    $scope.model = {};

    // List of gender ( Male & Female)
    $scope.genders = genderConstant.listGender;

    //#endregion

    //#region Methods

    $scope.clickSignup = function () {
        if ($scope.signupForm.$invalid) {
            return
        }
        $user.register($scope.model).then(function () {
            toastr.success('Register successfully');
            $state.go('loginState');
        })

    }

    //#endregion
});