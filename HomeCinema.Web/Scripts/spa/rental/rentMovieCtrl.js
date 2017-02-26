(function (app) {
    'use strict';

    app.controller('rentMovieCtrl', rentMovieCtrl);

    rentMovieCtrl.$inject = ['$scope', '$modalInstance', '$location', 'apiService', 'notificationService']

    function rentMovieCtrl($scope, $modalInstance, $location, apiService, notificationService) {

        $scope.Title = $scope.movie.Title;
        $scope.loadStockItems = loadStockItems;
        $scope.selectCustomer = selectCustomer;
        $scope.selectionChanged = selectionChanged;
        $scope.rentMovie = rentMovie;
        $scope.cancelRental = cancelRental;
        $scope.stockItems = [];
        $scope.selectedCustomer = -1;
        $scope.isEnabled = false;

        function loadStockItems() {
            notificationService.displayInfo('Loading available stock items for ' + $scope.movie.Title);
            apiService.get('/api/stocks/movie/' + $scope.movie.ID, null, stockItemsLoadCompleted, stockItemsLoadFailed);
        }

        function stockItemsLoadCompleted(result) {
            $scope.stockItems = response.data;
            $scope.selectedStockItem = $scope.stockItems[0].ID;
            console.log(response);
        }

        function stockItemsLoadFailed(error) {
            notificationService.displayError(error.data);
        }

        function selectCustomer($item) {
            if ($item) {
                $scope.selectedCustomer = $item.originalObject.ID;
                $scope.isEnabled = true;
            }
            else {
                $scope.selectedCustomer = -1;
                $scope.isEnabled = false;
            }
        }

        function selectionChanged($item) {

        }

        function rentMovie() {
            apiService.post('/api/rentals/rent/' + $scope.selectedCustomer + '/' + $scope.selectedStockItem, null, rentMovieSucceeded, rentMovieFailed);
        }

        function rentMovieSucceeded(result) {
            notificationService.displaySuccess('Rental completed successfully');
            $modalInstance.close();
        }

        function rentMovieFailed(error) {
            notificationService.displayError(error.data.Message);
        }

        function cancelRental() {
            $scope.stockItems = [];
            $scope.selectedCustomer = -1;
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }

    }
})(angular.module('homeCinema'));