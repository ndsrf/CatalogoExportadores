(function () {
    'use strict';

    var serviceId = 'datacontext';
    angular.module('app').factory(serviceId,
        ['common', 'entityManagerFactory', datacontext]);

    function datacontext(common, entityManagerFactory) {
        var queryParamCache = {};

        var $q = common.$q;

        var eMan = entityManagerFactory.newManager();

        var lookups;

        var service = {
            getPeople: getPeople,
            getMessageCount: getMessageCount,
            getExportadores: getExportadores,
            getExportadoresFiltrados: getExportadoresFiltrados,
            getLookups: getLookups
        };

        return service;

        function getExportadores(forceRemote) {
            if (forceRemote) {
                queryParamCache = {};
            }
            var isInCache = queryParamCache[0];
            var query = breeze.EntityQuery.from("Exportadores").expand("Provincia");

            if (isInCache && !forceRemote) {
                query = query.using(breeze.FetchStrategy.FromLocalCache);
            } else {
                queryParamCache[0] = true;
                query = query.using(breeze.FetchStrategy.FromServer);
            }

            return eMan.executeQuery(query);
        }

        function getExportadoresFiltrados(filtroNombre, forceRemote) {
            if (forceRemote) {
                queryParamCache = {};
            }
            var isInCache = queryParamCache[filtroNombre];
            var query = breeze.EntityQuery.from("ExportadoresById").withParameters({id : 17}).where("nombre", "==", filtroNombre);
            if (isInCache && !forceRemote) {
                query = query.using(breeze.FetchStrategy.FromLocalCache);
            } else {
                queryParamCache[filtroNombre] = true;
                query = query.using(breeze.FetchStrategy.FromServer);
            }
            return eMan.executeQuery(query);
        }

        function getLookups() {
            return breeze.EntityQuery
                .from('Lookups')
                .using(eMan)
                .execute()
                .then(queryLookupsSucceeded);
        };

        function queryLookupsSucceeded(data) {
            lookups = data.results[0];
            // datacontext was defined earlier in the module.
        }

        function getMessageCount() { return $q.when(72); }

        function getPeople() {
            var people = [
                { firstName: 'John', lastName: 'Papa', age: 25, location: 'Florida' },
                { firstName: 'Ward', lastName: 'Bell', age: 31, location: 'California' },
                { firstName: 'Colleen', lastName: 'Jones', age: 21, location: 'New York' },
                { firstName: 'Madelyn', lastName: 'Green', age: 18, location: 'North Dakota' },
                { firstName: 'Ella', lastName: 'Jobs', age: 18, location: 'South Dakota' },
                { firstName: 'Landon', lastName: 'Gates', age: 11, location: 'South Carolina' },
                { firstName: 'Haley', lastName: 'Guthrie', age: 35, location: 'Wyoming' }
            ];
            return $q.when(people);
        }
    }
})();