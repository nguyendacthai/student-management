angular.module('myApp')
    .service('$specialized',function($http){
        this.createSpecialized = function (name) {
            var url = 'http://localhost:57701/api/specialized';
            return $http
                .post(url,{name : name})
                .then(function (x) {
                    return x.data;
                });
        }
    });