(function (app) {
    app.controller('studentListController', studentListController);
    studentListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function studentListController($scope, apiService, notificationService, $ngBootbox, $filter) {

        //Phân trang
        $scope.students = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getStudents = getStudents;
        $scope.search = search;
        $scope.keyword = '';
        $scope.deleteStudent = deleteStudent;
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
                    checkedStudents: JSON.stringify(listId)
                }
            };
            apiService.del('/api/students/deletemulti',
                config,
                (result) => {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' sinh viên');
                    search();
                },
                (failure) => {
                    notificationService.displayError('Xóa các sinh viên trên thất bại');
                });
        }

        //Select all item that you would like to delete
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.students, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.students, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        //Button delete multiple only enable when user have selected students
        $scope.$watch("students", function (n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteStudent(id) {
            $ngBootbox.confirm('Bạn có thật sự muốn xóa sinh viên này không?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };

                apiService.del('/api/students/delete',
                    config,
                    (success) => {
                        notificationService.displaySuccess('Xóa sinh viên thành công');
                        search();
                    },
                    (failure) => {
                        notificationService.displayError('Xóa sinh viên thất bại');
                    });
            });
        }

        function search() {
            getStudents();
        }

        function getStudents(page) {

            page = page || 0;

            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 3
                }
            }

            apiService.get('/api/students/getall', config, (result) => {

                if (result.data.TotalCount == 0)
                    notificationService.displayWarning('Không tìm thấy sinh viên nào trong hệ thống');

                $scope.students = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, (failure) => {
                console.log('Load danh sách sinh viên thất bại');
            });
        }

        $scope.getStudents();
    }
})(angular.module('application.students'));