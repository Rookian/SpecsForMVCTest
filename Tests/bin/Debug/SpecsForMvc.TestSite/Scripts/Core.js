$(document).ready(function () {
    $("#searchForm").submit(function(event) {
        event.preventDefault();

        var url = this.action;
        var formdata = $(this).serialize();

        var success = function(data) {
            $("#gridContainer").html(data);
        };
        
        $.ajax({
            type: 'POST',
            url: url,
            data: formdata,
            success: success,
            dataType: "html"
        });
    });
    
    $("#gridContainer .paginationRight a").live("click", function (event) {
        event.preventDefault();
        $.ajax({
            type: "get",
            dataType: "html",
            url: this.href,
            data: {},
            success: function (response) {
                $("#gridContainer").html(response);
            }
        });
    });

});