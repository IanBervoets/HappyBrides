$(document).ready(function(){
    $(document).on('click', '.newItem', function(){
        $(".list-group").append('<li class="list-group-item"><input class="form-control" name="itemInput" type="text"><button class="btn btn-info btn-lg confirm" type="button">Confirm item name</button><button class="btn icon delete" type="button"><i class="fas fa-times-circle"></i></button><button class="btn icon down" type="button"><i class="fas fa-arrow-alt-circle-down"></i></button><button type="button" class="btn icon up"><i class="fas fa-arrow-alt-circle-up"></i></button></li>');
    })

    $(document).on('click', '.delete', function(){
        $(this).parent().remove();
    })

    function moveUp($item) {
        $before = $item.prev();
        $item.insertBefore($before);
    }

    function moveDown($item) {
        $after = $item.next();
        $item.insertAfter($after);
    }

    $(document).on('click', '.up', function(){
        moveUp($(this).parent());
    })

    $(document).on('click', '.down', function(){
        moveDown($(this).parent());
    })

    $(document).on('click', '.confirm', function(){
        var value = $('input[name="itemInput"]').val();
        $(this).parent().remove();
        $(".list-group").append('<li class="list-group-item">' + value + '<button class="btn icon delete" type="button"><i class="fas fa-times-circle"></i></button><button class="btn icon down" type="button"><i class="fas fa-arrow-alt-circle-down"></i></button><button type="button" class="btn icon up"><i class="fas fa-arrow-alt-circle-up"></i></button></li>');

    })

    $("#codeModal").modal('show');
});