angular.module("myApp").config(['$stateProvider', function($stateProvider){
    var specializedState = {
        controller : 'specializedController',
        name: 'specialized',
        url: '/specialized',
        templateUrl: '/modules/specialized/specialized.html',
        parent : 'dashboard'
    };
    $stateProvider.state(specializedState);
}]);