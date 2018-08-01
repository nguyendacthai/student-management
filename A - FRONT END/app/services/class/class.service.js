angular.module('myApp')
    .service('$class', function (appSettings, apiUrls, $http) {
        this.createClass = function (s) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.class.create;
            return $http
                .post(url, s)
                .then(function (x) {
                    return x.data;
                });
        };

        this.editClass = function (id, info) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.class.update;
            url = url.replace('{id}', id);
            return $http.put(url, info).then(function (x) {
                return x.data;
            });
        };

        this.loadClass = function (info) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.class.load;
            return $http
                .post(url, info)
                .then(function (x) {
                    return x.data;
                });
        };

    });