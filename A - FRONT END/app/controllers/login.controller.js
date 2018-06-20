var myApp = angular.module("myApp");
myApp.controller("loginController", function ($scope, $user, $state) {
    $scope.model = { username : '', password: ''}

    $scope.login = function ($event) {
        $event.preventDefault();
        if($scope.loginForm.$invalid){
            return;
        }
        $user.login($scope.model.username, $scope.model.password).then(function (x) {
            console.log(x);
            $state.go('dashboard')
        });
    };
});