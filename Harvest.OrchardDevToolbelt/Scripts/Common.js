(function($) {
    $(function() {
        $("a.submit").click(function(e) {
            e.preventDefault();
            $(this).parents("form:first").submit();
        });
    });
})(jQuery);