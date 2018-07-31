angular.module("myApp").controller("loginController", function ($scope, $user, $state, toastr, authenticationService) {
    $scope.model = { username : '', password: ''}

    $scope.login = function ($event) {
        $event.preventDefault();
        if($scope.loginForm.$invalid){
            return;
        }
        $user.login($scope.model.username, $scope.model.password).then(function (x) {

            // Save token into local storage.
            authenticationService.initAuthenticationToken(x.code);
            toastr.success('Login successfully')
            $state.go('dashboard')
        });
    };

    $scope.signup = function () {
        $state.go('signup');
    }
});