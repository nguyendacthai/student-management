angular.module("myApp").config(['$stateProvider', '$urlRouterProvider',function($stateProvider, $urlRouterProvider){
    $stateProvider.state('loginState', {
        controller : 'loginController',
        name : 'login',
        url : '/login',
        templateUrl : '/views/login.html',
    });

    $urlRouterProvider.otherwise('/login')
}]);