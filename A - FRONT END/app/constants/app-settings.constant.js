var myApp = angular.module("myApp");
myApp.constant('appSettings', {
    endPoint: {
        apiService : 'http://localhost:57701'
    },

    // Key in local storage to store token.
    identityStorage: 'identity'
});