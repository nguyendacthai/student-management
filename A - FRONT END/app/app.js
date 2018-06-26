// var myApp = angular.module("myApp");

angular.module("myApp").config(function ($httpProvider) {

    // API interceptor
    $httpProvider.interceptors.push('apiInterceptor');
});