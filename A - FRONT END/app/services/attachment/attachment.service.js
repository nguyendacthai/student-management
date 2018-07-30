angular.module('myApp')
    .service('$attachment',function(appSettings, apiUrls, $http) {
        this.loadAttachment = function (info) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.attachment.load;
            return $http
                .post(url, info)
                .then(function (x) {
                    return x.data;
                });
        };

        this.getAttachment = function (id) {
            var url = appSettings.endPoint.apiService + '/' + apiUrls.attachment.get;
            url = url.replace('{id}', id);
            return $http.get(url).then(function (x) {
                return x.data;
            });
        };
    });
