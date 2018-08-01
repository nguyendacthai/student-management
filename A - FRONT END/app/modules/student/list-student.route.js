angular.module("myApp").config(['$stateProvider', function ($stateProvider) {
    // var listStudentState = {
    //     controller : 'listStudentController',
    //     name: 'list-student',
    //     url: '/list-student',
    //     templateUrl: '/views/student/list-student.html',
    //     parent : 'dashboard'
    // };
    //
    // $stateProvider.state(listStudentState).state();

    $stateProvider.state('list-student', {
        controller: 'listStudentController',
        url: '/list-student',
        templateUrl: '/modules/student/list-student.html',
        parent: 'dashboard'
    }).state('edit-student', {
        url: '/edit-student/{id}', //id is
        templateUrl: '/modules/student/student-detail.html',
        parent: 'dashboard',
        controller: 'studentDetailController'
    });
}]);