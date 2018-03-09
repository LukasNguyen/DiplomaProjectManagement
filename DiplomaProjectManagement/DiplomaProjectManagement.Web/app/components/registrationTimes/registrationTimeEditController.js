(function (app) {
    app.controller('registrationTimeEditController', registrationTimeEditController);
    registrationTimeEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', '$filter'];

    function registrationTimeEditController($scope, apiService, notificationService, $state, $stateParams, $filter) {

        $scope.registrationTime = {};
        $scope.RegisteredDate = undefined;
        $scope.TeacherAssignGradesDate = undefined;
        $scope.ClosedDate = undefined;

        $scope.updateRegistrationTime = updateRegistrationTime;

        function updateRegistrationTime() {
            assignDataToModel();
            apiService.put('/api/registrationTimes/update', $scope.registrationTime, (result) => {
                notificationService.displaySuccess('Đợt đăng ký đồ án : ' + result.data.Name + ' cập nhật thành công.');
                $state.go('registrationTimes');
            }, (failure) => {
                notificationService.displayError(failure.data.Message);
            });
        }

        function loadRegistrationTimeDetail() {
            apiService.get('/api/registrationTimes/getbyid/' + $stateParams.id, null, (result) => {
                assignDataFromModel(result);
            }, (error) => {
                notificationService.displayError(error.data);
            });
        }

        function assignDataFromModel(result) {
            $scope.registrationTime.ID = result.data.ID;
            $scope.registrationTime.Name = result.data.Name;
            $scope.RegisteredDate = new Date(result.data.RegisteredDate);
            $scope.TeacherAssignGradesDate = new Date(result.data.TeacherAssignGradesDate);
            $scope.ClosedDate = new Date(result.data.ClosedDate);
        }

        function assignDataToModel() {
            $scope.registrationTime.RegisteredDate = $filter('date')($scope.RegisteredDate, 'MM/dd/yyyy');
            $scope.registrationTime.TeacherAssignGradesDate = $filter('date')($scope.TeacherAssignGradesDate, 'MM/dd/yyyy');
            $scope.registrationTime.ClosedDate = $filter('date')($scope.ClosedDate, 'MM/dd/yyyy');
        }

        loadRegistrationTimeDetail();
    }
})(angular.module('application.registrationTimes'));