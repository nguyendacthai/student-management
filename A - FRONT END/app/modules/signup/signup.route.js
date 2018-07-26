angular.module("myApp").config(['$stateProvider', function($stateProvider){
    var signupState = {
        controller : 'signupController',
        name: 'signup',
        url: '/signup',
        templateUrl: 'modules/signup/signup.html',
    };
    $stateProvider.state(signupState);
}]);