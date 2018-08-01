angular.module("myApp").config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $stateProvider.state('loginState', {
        controller: 'loginController',
        name: 'login',
        url: '/login',
        templateUrl: '/modules/login/login.html',
    });
    $urlRouterProvider.otherwise('/login')
}]);