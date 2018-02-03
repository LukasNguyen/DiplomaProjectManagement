(function (app) {
    app.controller('studentEditController', studentEditController);
    studentEditController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function studentEditController($scope, apiService, notificationService, $ngBootbox, $filter) {

    }
})(angular.module('application.students'));