(function (app) {
    app.controller('studentAddController', studentAddController);
    studentAddController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function studentAddController($scope, apiService, notificationService, $ngBootbox, $filter) {

    }
})(angular.module('application.students'));