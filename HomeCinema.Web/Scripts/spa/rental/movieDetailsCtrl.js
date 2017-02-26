(function (app) {
    'use strict';

    app.controller('movieDetailCtrl', movieDetailCtrl);

    movieDetailCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function movieDetailCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-movies';
        $scope.movie = {};
        $scope.loadingMovie = true;
        $scope.loadingRentals = true;
        $scope.isReadOnly = true;
        $scope.openRentDialog = openRentDialog;
        $scope.returnMovie = returnMovie;
        $scope.rentalHistory = [];
        $scope.getStatusColor = getStatusColor;
        $scope.clearSearch = clearSearch;
        $scope.isBorrowed = isBorrowed;

        function openRentDialog() {
            $modal.open({
                templateUrl: 'scripts/spa/rental/rentMovieModal.html',
                controller: 'rentMovieCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                loadMovieDetails();
            }, function () {
            });
        }

        function returnMovie(rentalId) {
            apiService.post('api/rental/return/' + rentalId, null, returnMovieSuccess, returnMovieFailed);
        }

        function returnMovieSuccess(result) {
            notificationService.displaySuccess('Movie returned to HomeCinema succeesfully');
            loadMovieDetails();
        }

        function returnMovieFailed(error) {
            notificationService.displayError(response.data);
        }

        function getStatusColor(status) {
            if (status == 'Borrowed')
                return 'red'
            else {
                return 'green';
            }
        }

        function clearSearch() {
            $scope.filterRentals = '';
        }

        function isBorrowed(rental) {
            return rental.Status == 'Borrowed';
        }

        function loadMovieDetails() {
            loadMovie();
            loadRentalHistory();
        }

        function loadMovie() {
            $scope.loadingMovie = true;
            apiService.get('/api/movies/details/' + $routeParams.id, null, movieLoadCompleted, movieLoadFailed);
        }

        function movieLoadCompleted(result) {
            $scope.movie = result.data;
            $scope.loadingMovie = false;
        }

        function movieLoadFailed(error) {
            notificationService.displayError(response.data);
        }

        function loadRentalHistory() {
            apiService.get('/api/rentals/' + $routeParams.id + '/rentalhistory', null, rentalHistoryLoadCompleted, rentalHistoryLoadFailed);
        }

        function rentalHistoryLoadCompleted(result) {
            onsole.log(result);
            $scope.rentalHistory = result.data;
            $scope.loadingRentals = false;
        }

        function rentalHistoryLoadFailed(error) {
            notificationService.displayError(response);
        }

        loadMovieDetails();
    }

})(angular.module('homeCinema'));