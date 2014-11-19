(function() {
    angular.module('remontApp').controller('GenericListCtrl', function ($scope, baseListCtrl, response, tableId) {

        $scope.rows = response.Bag;

        $scope.item = response.Items[0];

        baseListCtrl.create($scope, response, 'generic', 'api/generic', tableId);

        $scope.paginationVisible = function() {
            return response.PageInfoRequest.TotalPages.length > 0;
        };

    });
})();