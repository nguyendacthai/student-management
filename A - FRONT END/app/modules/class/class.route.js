angular.module("myApp").config(['$stateProvider', function($stateProvider){
    var classState = {
        controller : 'classController',
        name: 'class',
        url: '/class',
        templateUrl: 'modules/class/class.html',
        parent : 'dashboard'
    };
    $stateProvider.state(classState);
}]);