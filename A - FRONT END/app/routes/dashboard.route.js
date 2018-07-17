angular.module("myApp").config(['$stateProvider', function($stateProvider){
    //$locationProvider.html5Mode(true);
    var dashboardState = {
        controller : 'dashboardController',
        name: 'dashboard',
        url: '/dashboard',
        templateUrl: '/views/dashboard.html',
    };
    $stateProvider.state(dashboardState);
}]);