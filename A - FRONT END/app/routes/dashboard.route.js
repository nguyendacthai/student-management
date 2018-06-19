myApp.config(['$stateProvider', function($stateProvider){
    var dashboardState = {
        controller : 'dashboardController',
        name: 'dashboard',
        url: '/dashboard',
        templateUrl: '/views/dashboard.html'
    };
    $stateProvider.state(dashboardState);
}]);