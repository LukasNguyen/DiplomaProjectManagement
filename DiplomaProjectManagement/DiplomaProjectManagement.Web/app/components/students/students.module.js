(function () {
    angular.module('application.students', ['application.common']).config(config);

    config.$inject = ['$stateProvider'];

    function config($stateProvider) {
        $stateProvider.state('students',
            {
                url: '/students',
                templateUrl: '/app/components/students/studentListView.html',
                parent: 'base',
                controller: 'studentListController'
            }).state('student_add',
            {
                url: '/student_add',
                templateUrl: '/app/components/students/studentAddView.html',
                parent: 'base',
                controller: 'studentAddController'
            }).state('student_edit',
            {
                url: '/student_edit/:id',
                templateUrl: '/app/components/students/studentEditView.html',
                parent: 'base',
                controller: 'studentEditController'
            });
    }
})();