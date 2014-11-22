(function() {
    angular.module('remontApp').controller('GenericEditCtrl', function ($http, $scope, response, baseEditCtrl, extData) {
        
        var item = response.Item;

        $scope.columns = response.Bag;

        baseEditCtrl.create($scope, item, extData.pageUrl, extData.serviceUrl);

    });
})();