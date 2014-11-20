(function() {
    angular.module('remontApp').controller('TableEditCtrl', function ($scope, item, baseEditCtrl) {

        if (!item.Columns) {
            item.Columns = [];
        }

        if (item.Columns.length == 0) {
            item.Columns.push({ IsDeleted: false });
        }

        baseEditCtrl.create($scope, item, 'table', 'api/table');
        
        $scope.addColumn = function() {
            item.Columns.push({ IsDeleted: false });
        };

        $scope.addVisible = function (index) {
            return _.filter(item.Columns, function(c) {
                return c.IsDeleted == false;
            }).length - 1 == index;
        };

        $scope.removeVisible = function (index) {
            return index > 0 && _.filter(item.Columns, function (c) {
                return c.IsDeleted == false;
            }).length - 1 == index;
        };

        $scope.removeColumn = function (column) {
            column.IsDeleted = true;
        };

        $scope.onSave = function (newItem) {
            item.Columns.splice(0, item.Columns.length);
            $.each(newItem.Columns, function(i, column) {
                item.Columns.push(column);
            });
        };

    });
})();