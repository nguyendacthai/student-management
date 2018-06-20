myApp.config(['$stateProvider', function($stateProvider){
    var classState = {
        controller : 'classController',
        name: 'class',
        url: '/class',
        templateUrl: '/views/class.html',
        parent : 'dashboard'
    };
    $stateProvider.state(classState);
}]);