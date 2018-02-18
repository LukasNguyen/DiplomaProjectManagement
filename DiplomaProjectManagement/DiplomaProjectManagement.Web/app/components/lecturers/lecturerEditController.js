(function (app) {
    app.controller('lecturerEditController', lecturerEditController);
    lecturerEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams'];

    function lecturerEditController($scope, apiService, notificationService, $state, $stateParams) {

        $scope.lecturer = {};
        $scope.updateLecturer = updateLecturer;

        function updateLecturer() {
            apiService.put('/api/lecturers/update', $scope.lecturer, (result) => {
                notificationService.displaySuccess('Giảng viên: ' + result.data.Name + ' được cập nhật thành công.');
                $state.go('lecturers');
            }, (failure) => {
                notificationService.displayError(failure.data.Message);
            });
        }

        function getFacilities() {

            apiService.get('/api/facilities/getallnotpagination', null, (result) => {
                $scope.facilities = result.data;
            }, (failure) => {
                console.log('Load danh sách khoa thất bại');
            });
        }

        function loadLecturerDetail() {
            apiService.get('/api/lecturers/getbyid/' + $stateParams.id, null, (result) => {
                $scope.lecturer = result.data;
            }, (error) => {
                notificationService.displayError(error.data);
            });
        }

        getFacilities();
        loadLecturerDetail();
    }
})(angular.module('application.lecturers'));