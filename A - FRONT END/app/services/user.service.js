angular.module('myApp')
    .service('$user',function($http){
    this.login = function (username, password) {
        var url = 'http://localhost:57701/api/account/login';
        return $http
            .post(url,{username: username, password: password})
            .then(function (x) {
                return x.data;
            });
    }
});