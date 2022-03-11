$(function () {
    var PlaceHolderElement = $('#PlaceHolder');
    $('button[data-toggle="modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = new FormData(form[0]);
        $.ajax({
            url: actionUrl,
            type: 'POST',
            data: sendData,
            processData: false,
            contentType: false,
            success: function (data) {
                $('.modal').modal('hide');
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
            }
        });
    })
})

$(document).ready(function () {
    $(window).scroll(function(){
        if($(document).scrollTop() > 50){
            $("nav").addClass("shrink")
            $("nav").removeClass("bg-light")
        }
        else{
            $("nav").removeClass("shrink")
            $("nav").addClass("bg-light")
        }
    });
});