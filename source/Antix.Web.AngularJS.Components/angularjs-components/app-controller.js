'use strict';

var app = angular.module('antix.angularjs.app-controller', [
]);

app
       .controller(
        'AppController',
        [
            '$log', '$scope', '$state',
            function (
                $log, $scope, $state
                ) {

                $log.debug('AppController init');
            }
        ]);