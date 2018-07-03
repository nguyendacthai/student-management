angular.module("myApp").service('uiService', function ($uibModal) {
    /**
     * Display modal dialog with specific information.
     * @returns {}
     */
    this.displayModal = function (templateUrl, scope, size) {
        return $uibModal.open({
            templateUrl: templateUrl,
            scope: scope,
            size: size
        });
    };
});