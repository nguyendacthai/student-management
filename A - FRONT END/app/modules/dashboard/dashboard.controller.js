angular.module("myApp").controller("dashboardController", function ($scope, $state, authenticationService) {
    $scope.logout = function () {
        authenticationService.clearAuthenticationToken();
        $state.go('loginState');
    }
});