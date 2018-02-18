(function (app) {
    app.controller('lecturerListController', lecturerListController);
    lecturerListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function lecturerListController($scope, apiService, notificationService, $ngBootbox, $filter) {

        $scope.lecturers = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getLecturers = getLecturers;
        $scope.search = search;
        $scope.keyword = '';
        $scope.deleteLecturer = deleteLecturer;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;

        //Delete all item that you selected
        function deleteMultiple() {

            var listId = [];
            $.each($scope.selected, function (index, item) {
                listId.push(item.ID);
            });

            var config = {
                params: {
                    checkedLecturers: JSON.stringify(listId)
                }
            };
            apiService.del('/api/lecturers/deletemulti',
                config,
                (result) => {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' giảng viên');
                    search();
                },
                (failure) => {
                    notificationService.displayError('Xóa các giảng viên trên thất bại');
                });
        }

        //Select all item that you would like to delete
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.lecturers, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.lecturers, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        //Button delete multiple only enable when user have selected lecturers
        $scope.$watch("lecturers", function (n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteLecturer(id) {
            $ngBootbox.confirm('Bạn có thật sự muốn xóa giảng viên này không?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };

                apiService.del('/api/lecturers/delete',
                    config,
                    (success) => {
                        notificationService.displaySuccess('Xóa giảng viên thành công');
                        search();
                    },
                    (failure) => {
                        notificationService.displayError('Xóa giảng viên thất bại');
                    });
            });
        }

        function search() {
            getLecturers();
        }

        function getLecturers(page) {

            page = page || 0;

            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 3
                }
            }

            apiService.get('/api/lecturers/getall', config, (result) => {

                if (result.data.TotalCount == 0)
                    notificationService.displayWarning('Không tìm thấy giảng viên nào trong hệ thống');

                $scope.lecturers = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, (failure) => {
                console.log('Load danh sách giảng viên thất bại');
            });
        }

        $scope.getLecturers();
    }
})(angular.module('application.lecturers'));