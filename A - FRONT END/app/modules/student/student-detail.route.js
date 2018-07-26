angular.module("myApp").config(['$stateProvider', function($stateProvider){
    var studentDetailState = {
        controller : 'studentDetailController',
        name: 'add-student',
        url: '/add-student',
        templateUrl: '/modules/student/student-detail.html',
        parent : 'dashboard'
    };
    $stateProvider.state(studentDetailState);
}]);