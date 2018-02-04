(function (app) {
    app.controller('studentAddController', studentAddController);
    studentAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function studentAddController($scope, apiService, notificationService, $state) {


        $scope.student = {
            CreatedDate: new Date(),
            Status: true
        };
        $scope.AddStudent = AddStudent;

        function AddStudent() {

            apiService.post('/api/students/create', $scope.student, (result) => {
                notificationService.displaySuccess('Student name: ' + result.data.Name + ' are added to system.');
                $state.go('students');
            }, (failure) => {
                notificationService.displayError('Add new student are failed');
            });
        }
    }
})(angular.module('application.students'));