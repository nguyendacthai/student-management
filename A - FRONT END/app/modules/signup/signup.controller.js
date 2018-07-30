angular.module("myApp").controller("signupController", function ($scope, genderConstant,toastr, $user, appSettings, apiUrls,) {
    //#region Properties

    // models of signup.html
    $scope.model = {};

    // List of gender ( Male & Female)
    $scope.genders = genderConstant.listGender;

    //#endregion

    //#region Methods

    $scope.clickSignup = function () {
        console.log($scope.model);
        if ($scope.signupForm.$invalid) {
            return
        }
        debugger
        $user.register($scope.model).then(function () {
            toastr.success('Register successfully');
        })

    }

    //#endregion
});