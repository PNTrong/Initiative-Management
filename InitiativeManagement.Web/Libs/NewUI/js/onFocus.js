$('.form-control').focus(function () {
    $(this).parent().addClass('focused');
});

//On focusout event
$('.form-control').focusout(function () {
    var $this = $(this);
    if ($this.parents('.form-group').hasClass('form-float')) {
        if ($this.val() == '') { $this.parents('.form-line').removeClass('focused'); }
    }
    else {
        $this.parents('.form-line').removeClass('focused');
    }
});
