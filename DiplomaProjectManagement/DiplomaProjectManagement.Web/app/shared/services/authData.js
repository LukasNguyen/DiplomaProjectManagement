(function (app) {
    'use strict';
    app.factory('authData', [function () {
        var authDataFactory = {};

        var authenticationData = {
            IsAuthenticated: false,
            userName: ""
        };
        authDataFactory.authenticationData = authenticationData;

        return authDataFactory;
    }]);
})(angular.module('application.common'));