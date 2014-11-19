(function() {
    angular.module('remontApp').controller('GenericListCtrl', function ($location, $scope, baseListCtrl, response, tableId) {

        $scope.rows = response.Bag;

        $scope.item = response.Items[0];

        baseListCtrl.create($scope, response, 'generic', 'api/generic', tableId);

        //$scope.edit = function (id) {

        //    if (id > 0) {
        //        $location.path('generic/edit/' + tableId + '/' + id);
        //    } else {
        //        $location.path('generic/create/' + tableId);
        //    }

        //};

        $scope.paginationVisible = function() {
            return response.PageInfoRequest.TotalPages.length > 0;
        };

        $scope.getCellValue = function(cell, index) {
            return cell[index].Value;
        };

    });
})();