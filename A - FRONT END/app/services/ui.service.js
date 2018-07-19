angular.module("myApp").service('uiService', function ($uibModal, blockUI) {
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

    /**
     * Block UI
     * @returns {}
     */
    this.blockAppUI = function () {
        // Find application block UI.s
        let appBlockUI = blockUI.instances.get('uiBlocker');
        if (!appBlockUI)
            return;

        appBlockUI.start();
    };

    /**
     * Unblock UI
     * @returns {}
     */
    this.unblockAppUI = function () {
        // Find application block UI.
        let appBlockUI = blockUI.instances.get('uiBlocker');
        if (!appBlockUI)
            return;

        appBlockUI.stop();
    };
});