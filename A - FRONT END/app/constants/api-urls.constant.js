var myApp = angular.module("myApp");
myApp.constant('apiUrls', {
    specialized: {
        create : 'api/specialized',
        update : 'api/specialized/{id}',
        load : 'api/specialized/load-specialized'
    },
    account : {
        login : 'api/account/login'
    }
});