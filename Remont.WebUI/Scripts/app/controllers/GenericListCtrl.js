(function() {
    angular.module('remontApp').controller('GenericListCtrl', function ($location, $scope, baseListCtrl, response, extData) {

        $scope.columns = response.Bag;
        
        baseListCtrl.create($scope, response, extData.pageUrl, extData.serviceUrl, extData.tableId);

        $scope.onRowClicked = function(row) {
            console.log(row);
        }

    });
})();