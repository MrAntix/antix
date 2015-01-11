'use strict';

var app = angular.module('antix.angularjs', [
    'ngAnimate',
    'ngCookies',
    'ngSanitize',
    'ui.bootstrap',
    'ui.router',
    'ui.utils',
    'antix.angularjs.app-controller',
    'antix.angularjs.components'
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
                .state('components-cell-layout', {
                    url: '/components/cell-layout/',
                    templateUrl: baseUrl + '/components/cell-layout/cell-layout.cshtml'
                });

        }
    ]);