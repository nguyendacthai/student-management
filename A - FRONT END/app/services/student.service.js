angular.module('myApp')
    .service('$student',function(appSettings, apiUrls, $http){
        this.createStudent = function (s) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.student.create;
            return $http
                .post(url,s)
                .then(function (x) {
                    return x.data;
                });
        };

        this.editStudent = function (id, info) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.student.update;
            url = url.replace('{id}', id);
            return $http.put(url, info).then(function (x) {
                return x.data;
            });
        };

        this.loadStudent = function (info) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.student.load;
            return $http
                .post(url, info)
                .then(function (x) {
                    return x.data;
                });
        };

    });