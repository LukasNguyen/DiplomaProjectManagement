(function (app) {
    app.controller('studentAddController', studentAddController);
    studentAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function studentAddController($scope, apiService, notificationService, $state) {

        $scope.student = {
            Status: true
        };
        $scope.AddStudent = AddStudent;

        function AddStudent() {

            apiService.post('/api/students/create', $scope.student, (result) => {
                notificationService.displaySuccess('Student name: ' + result.data.Name + ' are added to system.');
                $state.go('students');
            }, (failure) => {
                notificationService.displayError(failure.data.Message);
            });
        }
    }
})(angular.module('application.students'));