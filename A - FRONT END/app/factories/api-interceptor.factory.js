angular.module("myApp").factory('apiInterceptor', function ($q, $injector, authenticationService) {
        return{
            // optional method
            'request': function(x) {
                // Find authentication token from local storage.
                var authenticationToken = authenticationService.getAuthenticationToken();

                // As authentication token is found. Attach it into the request.
                if (authenticationToken)
                    x.headers.Authorization = 'Bearer ' + authenticationToken;
                return x;
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
                    toastr.error(x.data.message, szMessage);
                else
                    console.log(szMessage);
                return $q.reject(x);
            }

        }
    });

// myApp.config(['$httpProvider', function ($httpProvider) {
//     $httpProvider.interceptors.push('apiInterceptor');
// }])