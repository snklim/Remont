(function() {
    angular.module('remontApp').controller('GenericEditCtrl', function ($http, $scope, item, baseEditCtrl, extData) {
        
        baseEditCtrl.create($scope, item, extData.pageUrl, extData.serviceUrl);

    });
})();