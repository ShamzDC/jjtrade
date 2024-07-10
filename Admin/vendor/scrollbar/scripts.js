$(document).ready(function () {
    "use strict";

    // custom scrollbar

    $("html").niceScroll({ styler: "fb", horizrailenabled: false, cursorcolor: "#333", cursorwidth: '4', cursorborderradius: '0', background: '#d6d6d6', spacebarenabled: false, cursorborder: '0', zindex: '1000' });

    $("#side-menu").niceScroll({cursorcolor: "rgba(51, 51, 51, 0.78)", styler: "fb", horizrailenabled: false, cursorwidth: '4', cursorborderradius: '0', background: '#d6d6d6', spacebarenabled: false, cursorborder: '0', zindex: '1000' });

    $("#scrollbar-calculation-bmi").niceScroll({ cursorcolor: "rgba(51, 51, 51, 0.78)", styler: "fb", horizrailenabled: false, cursorwidth: '4', cursorborderradius: '0', background: '#d6d6d6', spacebarenabled: false, cursorborder: '0', zindex: '1000' });
    
    $(".scrollbartab").niceScroll({ styler: "fb", cursorcolor: "rgba(31, 171, 206, 0.78)", cursorwidth: '3', cursorborderradius: '0', autohidemode: 'false', background: '#F1F1F1', spacebarenabled: false, cursorborder: '0' });
    $(".scrollbartab").getNiceScroll();
    if ($('body').hasClass('scrollbartab-collapsed')) {
        $(".scrollbartab").getNiceScroll().hide();
    }
});

function setModalMaxHeight(element) {
  this.$element     = $(element);  
  this.$content     = this.$element.find('.modal-content');
  var borderWidth   = this.$content.outerHeight() - this.$content.innerHeight();
  var dialogMargin  = $(window).width() < 768 ? 20 : 60;
  var contentHeight = $(window).height() - (dialogMargin + borderWidth);
  var headerHeight  = this.$element.find('.modal-header').outerHeight() || 0;
  var footerHeight  = this.$element.find('.modal-footer').outerHeight() || 0;
  var maxHeight     = contentHeight - (headerHeight + footerHeight);

  this.$content.css({
      'overflow': 'hidden'
  });
  
  this.$element
    .find('.modal-body').css({
      'max-height': maxHeight,
      'overflow-y': 'auto'
  });
}

$('.modal').on('show.bs.modal', function() {
  $(this).show();
  setModalMaxHeight(this);
});

$(window).resize(function() {
  if ($('.modal.in').length != 0) {
    setModalMaxHeight($('.modal.in'));
  }
});

$('.modal-content').resizable({
    //alsoResize: ".modal-dialog",
    minHeight: 300,
    minWidth: 300
});
$('.modal-dialog').draggable();
                     
     
  