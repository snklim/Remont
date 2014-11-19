(function() {
    angular.module('remontApp').controller('GenericListCtrl', function ($location, $scope, baseListCtrl, response, extData) {

        $scope.rows = response.Bag;

        $scope.item = response.Items[0];

        baseListCtrl.create($scope, response, extData.pageUrl, extData.serviceUrl);
        
        $scope.paginationVisible = function() {
            return response.PageInfoRequest.TotalPages.length > 0;
        };

        $scope.getCellValue = function(cell, index) {
            return cell[index].Value;
        };

    });
})();