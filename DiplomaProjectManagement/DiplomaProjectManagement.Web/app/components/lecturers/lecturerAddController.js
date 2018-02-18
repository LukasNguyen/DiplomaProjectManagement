(function (app) {
    app.controller('lecturerAddController', lecturerAddController);
    lecturerAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function lecturerAddController($scope, apiService, notificationService, $state) {

        $scope.lecturer = {
            Status: true
        };
        $scope.facilities = [];

        $scope.addLecturer = addLecturer;

        function addLecturer() {

            apiService.post('/api/lecturers/create', $scope.lecturer, (result) => {
                notificationService.displaySuccess('Giảng viên: ' + result.data.Name + ' được thêm vào hệ thống.');
                $state.go('lecturers');
            }, (failure) => {
                notificationService.displayError(failure.data.Message);
            });
        }

        function getFacilities() {

            apiService.get('/api/facilities/getallnotpagination', null, (result) => {
                $scope.facilities = result.data;
                $scope.lecturer.FacilityId = $scope.facilities[0].ID;
            }, (failure) => {
                console.log('Load danh sách khoa thất bại');
            });
        }

        getFacilities();
    }
})(angular.module('application.lecturers'));