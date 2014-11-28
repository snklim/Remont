(function() {
    String.format = function() {
        var s = arguments[0];
        for (var i = 0; i < arguments[1].length; i++) {
            var reg = new RegExp("\\{" + i + "\\}", "gm");
            s = s.replace(reg, arguments[1][i] ? arguments[1][i] : '');
        }

        return s;
    }
})();