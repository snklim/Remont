(function() {
    angular.module('remontApp').filter('formatValue', function() {
        return function (cell) {
            if (cell.DataSourceRow) {

                if (cell.Column.DataSourceValueFormat) {
                    var values = [];
                    cell.DataSourceRow.Cells.forEach(function (c) {
                        values.push(c.Value);
                    });
                    var ret = String.format(cell.Column.DataSourceValueFormat, values);

                    return ret;
                }

                return cell.DataSourceRow.Cells[0].Value;
            }

            return cell.Value;
        }
    });
})();