angular.module('myApp')
    .service('authenticationService',function($window, appSettings){
        return {
            /*
            * Getting authentication token from localStorage.
            * */
            getAuthenticationToken: () => {
                return $window.localStorage.getItem(appSettings.identityStorage);
            },

            /*
            * Initiate authentication token into local storage.
            * */
            initAuthenticationToken: (accessToken) => {
                $window.localStorage.setItem(appSettings.identityStorage, accessToken);
            },

            /*
            * Remove authentication token from localStorage.
            * */
            clearAuthenticationToken: () => {
                $window.localStorage.removeItem(appSettings.identityStorage);
            },

            // /*
            // * Check whether role is in roles list or not.
            // * */
            // bIsInRole: (profileRoles, roles) => {
            //     if (!profileRoles || profileRoles.length < 1 || !roles || roles.length < 1) {
            //         return false;
            //     }
            //
            //     // Filter roles.
            //     let filters = profileRoles.filter(function (x) {
            //         return roles.indexOf(x) !== -1;
            //     });
            //
            //     if (!filters || filters.length < 1) {
            //         return false;
            //     }
            //
            //     return true;
            // },
            //
            // /*
            // * Check whether user has only one specific role or not.
            // * */
            // bHasOnlyRole: (profileRoles, role) => {
            //     if (profileRoles == null)
            //         return false;
            //
            //     if (profileRoles.length !== 1)
            //         return false;
            //
            //     return profileRoles[0] == role;
            // }
        }
    });