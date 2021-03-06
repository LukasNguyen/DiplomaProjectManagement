﻿(function () {
    angular.module('application', [
        'application.students',
        'application.facilities',
        'application.registrationTimes',
        'application.lecturers',
        'application.common'])
        .config(config)
        .config(configAuthentication)
        .config(['$qProvider', function ($qProvider) {
        $qProvider.errorOnUnhandledRejections(false);
    }]);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('base',
            {
                url: '',
                templateUrl: '/app/shared/views/baseView.html',
                abstract: true
            }).state('login',
            {
                url: '/login',
                templateUrl: '/app/components/login/loginView.html',
                controller: 'loginController'
            }).state('home',
            {
                url: '/admin',
                parent: 'base',
                templateUrl: '/app/components/home/homeView.html',
                controller: 'homeController'
            });

        $urlRouterProvider.otherwise('/login');
    }

    //Config check login mỗi
    function configAuthentication($httpProvider) {
        //interceptors : quản trị sự tương tác giữa client và server
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();