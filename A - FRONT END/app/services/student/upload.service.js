angular.module('myApp')
    .service('$upload', function (appSettings, apiUrls, $http) {
        this.upload = function (studentId, document) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.attachment.create;
            let f = new FormData();
            f.append('studentId', studentId);
            f.append('document', document);

            let options = {
                headers: {
                    'Content-Type': undefined
                }
            };
            return $http
                .post(url, f, options)
                .then(function (x) {
                    return x.data;
                });
        }
    });