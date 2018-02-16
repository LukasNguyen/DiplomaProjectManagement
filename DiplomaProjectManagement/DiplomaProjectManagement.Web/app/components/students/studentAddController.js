(function (app) {
    app.controller('studentAddController', studentAddController);
    studentAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function studentAddController($scope, apiService, notificationService, $state) {

        $scope.student = {
            Status: true
        };
        $scope.addStudent = addStudent;

        function addStudent() {

            apiService.post('/api/students/create', $scope.student, (result) => {
                notificationService.displaySuccess('Sinh viên: ' + result.data.Name + ' được thêm vào hệ thống.');
                $state.go('students');
            }, (failure) => {
                notificationService.displayError(failure.data.Message);
            });
        }
    }
})(angular.module('application.students'));