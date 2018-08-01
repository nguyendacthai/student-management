angular.module("myApp").controller("dashboardController", function ($scope, $state, $user, authenticationService) {
    //#region Properties

    $scope.model = {};

    //#endregion

    //#region Methods

    // Get personal profile
    $user.getProfile().then(function (x) {
        console.log(x)
        $scope.model.name = x.fullname;
    });

    // Log out function
    $scope.logout = function () {
        authenticationService.clearAuthenticationToken();
        $state.go('loginState');
    };

    //#endregion
});