﻿(function (app) {
    'use strict';

    app.controller('movieEditCtrl', movieEditCtrl);
    movieEditCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService', 'fileUploadService'];

    function movieEditCtrl($scope, $location, $routeParams, apiService, notificationService, fileUploadService) {
        $scope.pageClass = 'page-movies';
        $scope.movie = {};
        $scope.genres = [];
        $scope.loadingMovie = true;
        $scope.isReadOnly = false;
        $scope.UpdateMovie = UpdateMovie;
        $scope.prepareFiles = prepareFiles;
        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        var movieImage = null;

        function loadMovie() {
            $scope.loadingMovie = true;
            apiService.get('/api/movies/details/' + $routeParams.id, null, movieLoadCompleted, movieLoadFailed);
        }

        function movieLoadCompleted(response) {
            $scope.movie = result.data;
            $scope.loadingMovie = false;
            loadGenres();
        }

        function movieLoadFailed(error) {                   
            notificationService.displayError(error.data);
        }

        function loadGenres() {
            apiService.get('/api/genres/', null, genresLoadCompleted, genresLoadFailed);
        }

        function genresLoadCompleted(result) {
            $scope.genres = result.data;
        }

        function genresLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function UpdateMovie() {
            if (movieImage) {
                fileUploadService.uploadImage(movieImage, $scope.movie.ID, UpdateMovieModel);
            }
            else
                UpdateMovieModel();
        }

        function UpdateMovieModel() {
            apiService.post('/api/movies/update', $scope.movie, updateMovieSucceded, updateMovieFailed);
        }

        function updateMovieSucceded(result) {
            console.log(result);
            notificationService.displaySuccess($scope.movie.Title + ' has been updated');
            $scope.movie = result.data;
            movieImage = null;
        }

        function updateMovieFailed(error) {
            notificationService.displayError(error);
        }

        function prepareFiles($files) {
            movieImage = $files;
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.datepicker.opened = true;
        }

        loadMovie();
    }




})(angular.module('homeCinema'));