angular.module('myApp')
    .service('$specialized', function (appSettings, apiUrls, $http) {
        this.createSpecialized = function (s) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.specialized.create;
            return $http
                .post(url, s)
                .then(function (x) {
                    return x.data;
                });
        };

        this.editSpecialized = function (id, info) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.specialized.update;
            url = url.replace('{id}', id);
            return $http.put(url, info).then(function (x) {
                return x.data;
            });
        };

        this.loadSpecialized = function (info) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.specialized.load;
            return $http
                .post(url, info)
                .then(function (x) {
                    return x.data;
                });
        };

    });