(function () {
    'use strict';
    var controllerId = 'detalle';
    angular.module('app').controller(controllerId, ['common', 'datacontext', detalle]);

    function detalle(common, datacontext) {
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
        vm.title = 'Detalle';

        activate();

        function activate() {
            var promises = [getMessageCount(), getPeople(), getExportadores(), getExportadoresFiltrados(), getLookups()];
            common.activateController(promises, controllerId)
                .then(function () { log('Activated Detalle View'); });
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