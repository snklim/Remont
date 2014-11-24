(function() {
    angular.module('remontApp').controller('GenericEditCtrl', function ($http, $scope, response, baseEditCtrl, extData) {
        
        var item = response.Item;

        $scope.columns = response.Bag;

        baseEditCtrl.create($scope, item, extData.pageUrl, extData.serviceUrl);

        $scope.onSave = function (newItem) {
            item.Cells.splice(0, item.Cells.length);
            $.each(newItem.Cells, function (i, cell) {
                item.Cells.push(cell);
            });
        };

        $scope.beginPeopleSelect = function(item) {
            $('#PeoplePickerModal').modal('toggle');
        }

    });
})();