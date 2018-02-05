(function (app) {
    app.controller('studentEditController', studentEditController);
    studentEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state','$stateParams'];

    function studentEditController($scope, apiService, notificationService, $state, $stateParams) {

        $scope.student = {};
        $scope.UpdateStudent = UpdateStudent;

        function UpdateStudent() {
            apiService.put('/api/students/update', $scope.student, (result) => {
                notificationService.displaySuccess('Student name: ' + result.data.Name + ' are updated.');
                $state.go('students');
            }, (failure) => {
                notificationService.displayError(failure.data.Message);
            });
        }

        function loadStudentDetail() {
            apiService.get('/api/students/getbyid/' + $stateParams.id, null, (result) => {
                $scope.student = result.data;
            }, (error) => {
                notificationService.displayError(error.data);
            });
        }
        loadStudentDetail();
    }
})(angular.module('application.students'));