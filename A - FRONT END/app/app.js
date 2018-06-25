var myApp = angular.module("myApp");

myApp.config(function ($httpProvider) {

    // API interceptor
    $httpProvider.interceptors.push('apiInterceptor');
});