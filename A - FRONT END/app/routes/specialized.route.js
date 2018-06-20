myApp.config(['$stateProvider', function($stateProvider){
    var specializedState = {
        controller : 'specializedController',
        name: 'specialized',
        url: '/specialized',
        templateUrl: '/views/specialized.html',
        parent : 'dashboard'
    };
    $stateProvider.state(specializedState);
}]);