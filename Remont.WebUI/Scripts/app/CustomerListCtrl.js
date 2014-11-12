
angular.module('remontApp').controller('CustomerListCtrl', function ($scope, $location, response, feeder, pageService) {

    $scope.pageInfo = response.PageInfoRequest;

    $scope.customers = response.Items;

    //var feeder = dataFeeder.create('/api/customer');

    $scope.pages = pageService.getPages($scope.pageInfo.PageIndex, $scope.pageInfo.TotalPages);

    $scope.edit = function (id) {

        if (id > 0) {
            $location.path('customer/edit/' + id);
        } else {
            $location.path('customer/create');
        }

    };

    $scope.pageChange = function () {

        var pageIndex = $(event.target).attr('data-pageindex');

        if (pageIndex == 'prev') {
            pageIndex = $scope.pageInfo.PageIndex - 1;
        } else if (pageIndex == 'next') {
            pageIndex = $scope.pageInfo.PageIndex + 1;
        }

        feeder.get({
            pageIndex: pageIndex
        }).then(function(r) {
            $scope.customers = r.Items;
            $scope.pageInfo = r.PageInfoRequest;

            $scope.pages = pageService.getPages($scope.pageInfo.PageIndex, $scope.pageInfo.TotalPages);
        });

        event.preventDefault();

    };

});