var myApp = angular.module("myApp");

myApp.factory('apiInterceptor', function ($q, $injector) {
        return{
            // optional method
            'request': function(config) {

                var $window = $injector.get('$window');
                // do something on success
                config.headers = config.headers || {};

                var authData = $window.localStorage.getItem('authorizationData');
                if (authData) {
                    config.headers.Authorization = 'Bearer ' + authData.token;
                }
                return config;
            },

            // optional method
            'requestError': function(config) {
                // do something on error
                return config;
            },

            // optional method
            'response': function(response) {
                // do something on success
                return response;
            },

            // optional method
            'responseError': function(x) {
                // Response is invalid.
                if (!x)
                    return $q.reject(x);

                var url = x.config.url;
                if (!url || url.indexOf('/api/') === -1)
                    return $q.reject(x);

                // Find toastr notification from injector.
                var toastr = $injector.get('toastr');

                var szMessage = '';
                switch (x.status) {
                    case 400:
                        szMessage = 'Bad request';
                        break;
                    case 401:
                        szMessage = 'Credential is invalid';
                        break;
                    case 403:
                    case 404:
                        szMessage = 'Not found';
                        break;
                    case 409:
                        szMessage = 'Conflict';
                        break;
                    case 500:
                        szMessage = 'Internal server error';
                        break;
                    default:
                        szMessage = 'Unknown error';
                        break;
                }

                if (toastr)
                    toastr.error(szMessage, 'Error');
                else
                    console.log(szMessage);
                return $q.reject(x);
            }

        }
    });

// myApp.config(['$httpProvider', function ($httpProvider) {
//     $httpProvider.interceptors.push('apiInterceptor');
// }])