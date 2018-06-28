var myApp = angular.module("myApp");
myApp.constant('apiUrls', {
    specialized: {
        create : 'api/specialized',
        update : 'api/specialized/{id}',
        load : 'api/specialized/load-specialized'
    },

    class: {
        create : 'api/class',
        update : 'api/class/{id}',
        load : 'api/class/load-class'
    },

    account : {
        login : 'api/account/login'
    }
});