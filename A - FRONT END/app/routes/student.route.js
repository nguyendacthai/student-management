angular.module("myApp").config(['$stateProvider', function($stateProvider){
    var studentState = {
        controller : 'studentController',
        name: 'student',
        url: '/student',
        templateUrl: '/views/student.html',
        parent : 'dashboard'
    };
    $stateProvider.state(studentState);
}]);