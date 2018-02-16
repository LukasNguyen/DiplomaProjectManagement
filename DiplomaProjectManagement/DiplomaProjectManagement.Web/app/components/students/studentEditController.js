(function (app) {
    app.controller('studentEditController', studentEditController);
    studentEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state','$stateParams'];

    function studentEditController($scope, apiService, notificationService, $state, $stateParams) {

        $scope.student = {};
        $scope.updateStudent = updateStudent;

        function updateStudent() {
            apiService.put('/api/students/update', $scope.student, (result) => {
                notificationService.displaySuccess('Sinh viên: ' + result.data.Name + ' được cập nhật thành công.');
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