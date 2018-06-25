angular.module('myApp')
    .service('$user',function(appSettings, apiUrls, $http){
    this.login = function (username, password) {
        var url = appSettings.endPoint.apiService + '/' + apiUrls.account.login;
        return $http
            .post(url,{username: username, password: password})
            .then(function (x) {
                return x.data;
            });
    }
});