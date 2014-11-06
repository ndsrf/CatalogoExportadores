(function () {
    'use strict';
    var controllerId = 'dashboard';
    angular.module('app').controller(controllerId, ['common', 'datacontext', dashboard]);

    function dashboard(common, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.news = {
            title: 'Hot Towel Angular',
            description: 'Hot Towel Angular is a SPA template for Angular developers.'
        };
        vm.messageCount = 0;
        vm.people = [];
        vm.exportadores = [];
        vm.title = 'Dashboard';

        activate();

        function activate() {
            var promises = [getMessageCount(), getPeople(), getExportadores(), getExportadoresFiltrados(), getLookups()];
            common.activateController(promises, controllerId)
                .then(function () { log('Activated Dashboard View'); });
        }

        function getLookups() {
            datacontext.getLookups();
        }

        function getMessageCount() {
            return datacontext.getMessageCount().then(function (data) {
                return vm.messageCount = data;
            });
        }

        function getPeople() {
            return datacontext.getPeople().then(function (data) {
                console.log(data);
                return vm.people = data;
            });
        }

        function getExportadores() {
            return datacontext.getExportadores().then(function (data) {
                console.log(data);
                return vm.exportadores = data.results;
            });
        }

        function getExportadoresFiltrados() {
            return datacontext.getExportadoresFiltrados("Javier").then(function (data) {
                console.log(data);
                return vm.exportadores = data.results;
            });
        }


    }
})();