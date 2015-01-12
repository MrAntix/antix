'use strict';

var app = angular.module('antix.angularjs', [
    'ngAnimate',
    'ngCookies',
    'ngSanitize',
    'ui.bootstrap',
    'ui.router',
    'ui.utils',
    'antix.angularjs.app-controller',
    'antix.angularjs.cell-layout'
]);

app
    .config([
        '$stateProvider', '$urlRouterProvider',
        function(
            $stateProvider, $urlRouterProvider
        ) {

            $urlRouterProvider.otherwise("/");

            var baseUrl = '/angularjs-components';

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: baseUrl + '/home/home.cshtml'
                })
                .state('cell-layout', {
                    url: '/cell-layout/',
                    templateUrl: baseUrl + '/cell-layout/cell-layout.cshtml'
                });

        }
    ]);