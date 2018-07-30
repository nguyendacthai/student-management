angular.module('myApp')
    .service('$userRole',function(appSettings, apiUrls, $http) {
        this.loadUserRole = function (info) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.userRole.load;
            return $http
                .post(url, info)
                .then(function (x) {
                    return x.data;
                });
        };
    });
