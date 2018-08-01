angular.module("myApp").config(['$stateProvider', function ($stateProvider) {
    var forgotPasswordState = {
        controller: 'forgotPasswordController',
        name: 'forgotPassword',
        url: '/forgot-password',
        templateUrl: 'modules/forgot-password/forgot-password.html',
    };
    $stateProvider.state(forgotPasswordState);
}]);