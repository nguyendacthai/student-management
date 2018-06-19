myApp.config(['$stateProvider', '$urlRouterProvider', function($stateProvider, $urlRouterProvider){
    $stateProvider.state('default', {
        controller : 'loginController',
        name : 'login',
        url : '/login',
        templateUrl : '/views/login.html'
    })
    $urlRouterProvider.otherwise('/login')
}]);