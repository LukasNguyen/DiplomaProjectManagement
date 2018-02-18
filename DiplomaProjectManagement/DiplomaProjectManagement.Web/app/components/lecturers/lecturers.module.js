(function () {
    angular.module('application.lecturers', ['application.common']).config(config);

    config.$inject = ['$stateProvider'];

    function config($stateProvider) {
        $stateProvider.state('lecturers',
            {
                url: '/lecturers',
                templateUrl: '/app/components/lecturers/lecturerListView.html',
                parent: 'base',
                controller: 'lecturerListController'
            }).state('lecturer_add',
            {
                url: '/lecturer_add',
                templateUrl: '/app/components/lecturers/lecturerAddView.html',
                parent: 'base',
                controller: 'lecturerAddController'
            }).state('lecturer_edit',
            {
                url: '/lecturer_edit/:id',
                templateUrl: '/app/components/lecturers/lecturerEditView.html',
                parent: 'base',
                controller: 'lecturerEditController'
            });
    }
})();