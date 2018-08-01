angular.module("myApp").config(['$stateProvider', function ($stateProvider) {
    //$locationProvider.html5Mode(true);
    var dashboardState = {
        controller: 'dashboardController',
        name: 'dashboard',
        url: '/dashboard',
        templateUrl: 'modules/dashboard/dashboard.html',
    };
    $stateProvider.state(dashboardState);
}]);