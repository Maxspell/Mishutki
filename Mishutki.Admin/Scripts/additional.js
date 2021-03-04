$(document).ready(function() {
    $('.selectAll').change(function(){
        var ch = $('input[name="' + $(this).attr('id') + '"]');
        if($(this).is(':checked')){
            ch.prop('checked',true);
        } else {
            ch.prop('checked',false);
        }
    });

    $(".delete-confirm").click(function () {
        return confirm('Удалить выбранные элементы?');
    });
});