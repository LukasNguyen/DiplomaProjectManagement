(function (app) {
    app.service('apiService', apiService);
    apiService.$inject = ['$http', 'notificationService', 'authenticationService'];
    function apiService($http, notificationService, authenticationService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }
        function del(url, params, success, failure) {

            authenticationService.setHeader();

            $http.delete(url, params).then(
                (result) => {
                    success(result);
                }, (error) => {
                    if (error.status === 401)
                        notificationService.displayError('Authenticate is required');
                    else if (failure != null)
                        failure(error);
                });
        }
        function get(url, params, success, failure) {

            authenticationService.setHeader();

            $http.get(url, params).then(
                (result) => {
                    success(result);
                }, (error) => {
                    failure(error);
                });
        }
        function post(url, data, success, failure) {

            authenticationService.setHeader();

            $http.post(url, data).then(
                (result) => {
                    success(result);
                }, (error) => {
                    if (error.status === 401)
                        notificationService.displayError('Authenticate is required');
                    else if (failure != null)
                        failure(error);
                });
        }
        function put(url, data, success, failure) {

            authenticationService.setHeader();

            $http.put(url, data).then(
                (result) => {
                    success(result);
                }, (error) => {
                    if (error.status === 401)
                        notificationService.displayError('Authenticate is required');
                    else if (failure != null)
                        failure(error);
                });
        }

    }
})(angular.module('application.common'));