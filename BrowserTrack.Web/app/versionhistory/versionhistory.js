(function () {
    'use strict';
    var controllerId = 'versionhistory';
    angular.module('app').controller(controllerId, ['common', 'datacontext', versionhistory]);


    function versionhistory(common, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.isBusy = false;

        vm.versionHistory = [];
        vm.title = 'VersionHistory';

        activate();


        function activate() {
            var promises = [getVersionChangeHistory()];
            common.activateController(promises, controllerId)
                .then(function () { log('Activated Version History View'); });
        }



        function getVersionChangeHistory() {
            vm.isBusy = true;
            return datacontext.getVersionChangeHistory().then(function (payload) {
                vm.isBusy = false;
                return vm.versionHistory = payload;
            });
        }


        vm.getIcon = common.getIconPath;
    }



})();