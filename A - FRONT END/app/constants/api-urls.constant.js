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

    student: {
        create : 'api/student',
        update : 'api/student/{id}',
        load : 'api/student/load-student'
    },

    attachment:{
        create : 'api/attachment',
        load : 'api/attachment/load-attachment',
        get : 'api/attachment/get-attachment/{id}',
    },

    userRole:{
        load : 'api/user-role/load-user-role',
    },


    account : {
        login : 'api/account/login',
        register : 'api/account/register',
        forgotPassword : 'api/account/forgot-password'
    }
});