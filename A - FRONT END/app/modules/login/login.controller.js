angular.module("myApp").controller("loginController", function ($scope, $user, $state, toastr, authenticationService) {
    $scope.model = {};

    $scope.login = function ($event) {
        $event.preventDefault();
        if ($scope.loginForm.$invalid) {
            return;
        }
        $user.login($scope.model).then(function (x) {

            // Save token into local storage.
            authenticationService.initAuthenticationToken(x.code);
            toastr.success('Login successfully')
            $state.go('dashboard')
        });
    };

    $scope.signup = function () {
        $state.go('signup');
    }

    $scope.forgotPassword = function () {
        $state.go('forgotPassword');
    }
});