//owl starts
$(document).ready(function myFunction() {
    $(".slideshow-panel .loader").removeClass("wrloader");
});
$('#slideshow').owlCarousel({
    items: 6,
    autoPlay: false,
    singleItem: true,
    autoplayHoverPause: true,
    navigation: false,
    navigationText: ['<i class="fa fa-angle-left "></i>', '<i class="fa fa-angle-right"></i>'],
    pagination: true
});
$('#slideshow1').owlCarousel({
    items: 6,
    autoPlay: 6000,
    singleItem: true,
    autoplayHoverPause: true,
    navigation: true,
    navigationText: ['<i class="fa fa-angle-left "></i>', '<i class="fa fa-angle-right"></i>'],
    pagination: false
});
$(document).ready(function () {
    $(".special_product").owlCarousel({
        itemsCustom: [
            [0, 2],
            [600, 2],
            [992, 3],
            [1200, 4],
            [1650, 5]
        ],
        // autoPlay: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>'],
        navigation: true,
        pagination: false
    });
});
$(document).ready(function () {
    $("#feature_product").owlCarousel({
        itemsCustom: [
            [0, 2],
            [600, 2],
            [992, 3],
            [1200, 4],
            [1600, 5]
        ],
        // autoPlay: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>'],
        navigation: true,
        pagination: false
    });
});
$(document).ready(function () {
    $("#new").owlCarousel({
        itemsCustom: [
            [0, 2],
            [600, 2],
            [992, 3],
            [1200, 3],
            [1600, 4],
        ],
        //autoPlay: 1000,
        navigationText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        navigation: true,
        pagination: false
    });
});
$(document).ready(function () {
    $("#latest-product").owlCarousel({
        itemsCustom: [
            [0, 2],
            [600, 2],
            [992, 3],
            [1200, 4],
            [1600, 5]
        ],
        navigationText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        navigation: true,
        pagination: false
    });
});
$(document).ready(function () {
    $("#on_sale").owlCarousel({
        itemsCustom: [
            [0, 1],
        ],
        // autoPlay: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>'],
        navigation: true,
        pagination: false
    });
});
$(document).ready(function () {
    $("#top_rated").owlCarousel({
        itemsCustom: [
            [0, 1],
        ],
        // autoPlay: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>'],
        navigation: true,
        pagination: false
    });
});
$(document).ready(function () {
    $("#best_seller").owlCarousel({
        itemsCustom: [
            [0, 1],
        ],
        // autoPlay: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>'],
        navigation: true,
        pagination: false
    });
});
$('#carousel0').owlCarousel({
    items: 6,
    autoPlay: 3000,
    navigation: true,
    navigationText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
    pagination: false
});
$("#testimonial").owlCarousel({
    items: 3,
    lazyLoad: true,
    navigation: false,
    navigationText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
    autoPlay: 2000,
    singleItem: true,
    pagination: true
});
$("#owl-testimonial").owlCarousel({
    items: 3,
    lazyLoad: true,
    navigation: true,
    navigationText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
    autoPlay: 2000,
    singleItem: true,
    pagination: false
});
$('#gallery_01').owlCarousel({
    itemsCustom: [
        [1, 2],
        [400, 2],
        [500, 3],
        [700, 4],
        [768, 2],
        [800, 2],
        [1000, 3]
    ],
    autoPlay: false,
    navigation: true,
    navigationText: ['<i class="fa fa-chevron-left" aria-hidden="true"></i>', '<i class="fa fa-chevron-right" aria-hidden="true"></i>'],
    pagination: false
});
$(document).ready(function () {
    $("#related-product").owlCarousel({
        itemsCustom: [
            [0, 2],
            [600, 2],
            [992, 3],
            [1200, 4],
            [1600, 5]
        ],
        //autoPlay: 1000,

        navigationText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        navigation: true,
        pagination: false
    });
});
//owl ends


//headermenu dropdown start
function headermenu() {
    if (jQuery(window).width() < 992) {
        jQuery('ul.nav li.dropdown a.main-menu').attr("data-toggle", "dropdown");
    }
    else {
        jQuery('ul.nav li.dropdown a.main-menu').attr("data-toggle", "");
    }
}
$(document).ready(function () { headermenu(); });
jQuery(window).resize(function () { headermenu(); });
jQuery(window).scroll(function () { headermenu(); });
//headermenu dropdown end

//fixed header start
$(document).ready(function () {
    if (jQuery(window).width() > 1200) {
        $('#navbar').affix({
            offset: {
                top: $('header').height()
            }
        });
    }
});
//fixed header end

//carousel start
$('.carousel').carousel();
//carousel end



//search functionality start
$(document).ready(function () {

    $('#ddlsearchmenu').on('change', function () {
        $('#txtSearch').focus();
        //$('.category-select').slideToggle("slow");
    });

});
//search functionality end

//plugin bootstrap minus and plus start
$(document).ready(function () {
    $('.btn-number').click(function (e) {
        e.preventDefault();
        var fieldName = $(this).attr('data-field');
        var type = $(this).attr('data-type');
        var input = $("input[name='" + fieldName + "']");
        var currentVal = parseInt(input.val());
        if (!isNaN(currentVal)) {
            if (type == 'minus') {
                var minValue = parseInt(input.attr('min'));
                if (!minValue) minValue = 1;
                if (currentVal > minValue) {
                    input.val(currentVal - 1).change();
                }
                if (parseFloat(input.val()) == minValue) {
                    $(this).attr('disabled', true);
                }

            } else if (type == 'plus') {
                var maxValue = parseInt(input.attr('max'));
                if (!maxValue) maxValue = 999;
                if (currentVal < maxValue) {
                    input.val(currentVal + 1).change();
                }
                if (parseInt(input.val()) == maxValue) {
                    $(this).attr('disabled', true);
                }

            }
        } else {
            input.val(0);
        }
    });
    $('.input-number').focusin(function () {
        $(this).data('oldValue', $(this).val());
    });
    $('.input-number').change(function () {

        var minValue = parseInt($(this).attr('min'));
        var maxValue = parseInt($(this).attr('max'));
        if (!minValue) minValue = 1;
        if (!maxValue) maxValue = 999;
        var valueCurrent = parseInt($(this).val());
        var name = $(this).attr('name');
        if (valueCurrent >= minValue) {
            $(".btn-number[data-type='minus'][data-field='" + name + "']").removeAttr('disabled')
        } else {
            alert('Sorry, the minimum value was reached');
            $(this).val($(this).data('oldValue'));
        }
        if (valueCurrent <= maxValue) {
            $(".btn-number[data-type='plus'][data-field='" + name + "']").removeAttr('disabled')
        } else {
            alert('Sorry, the maximum value was reached');
            $(this).val($(this).data('oldValue'));
        }
    });
    $(".input-number").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
        // Allow: Ctrl+A
                               (e.keyCode == 65 && e.ctrlKey === true) ||
        // Allow: home, end, left, right
                                       (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
});
//plugin bootstrap minus and plus end

//date time picker start
$('.date').datetimepicker({
    pickTime: false
});

$('.datetime').datetimepicker({
    pickDate: true,
    pickTime: true
});

$('.time').datetimepicker({
    pickDate: false
});
//date time picker start

//login/register toggle start
$(document).ready(function () {

    $('.guestcontainer').css('display', 'none');
    $('.guestlogin').click(function () {
        $('.guestcontainer').fadeIn("slow");
        $('.logincontainer').css('display', 'none');
    });
    $('.userlogin').click(function () {
        $('.logincontainer').fadeIn("slow");
        $('.guestcontainer').css('display', 'none');
    });
});
//login/register toggle end

//essential start
$(document).ready(function () {
    // Highlight any found errors

    // Menu
    $('#menu .dropdown-menu').each(function () {
        var menu = $('#menu').offset();
        var dropdown = $(this).parent().offset();

        var i = (dropdown.left + $(this).outerWidth()) - (menu.left + $('#menu').outerWidth());

        if (i > 0) {
            $(this).css('margin-left', '-' + (i + 10) + 'px');
        }
    });


    // tooltips on hover
    $('[data-toggle=\'tooltip\']').tooltip({ container: 'body' });

});
//essential end

//go to top start
$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('#scroll').fadeIn();
        } else {
            $('#scroll').fadeOut();
        }
    });
    $('#scroll').click(function () {
        $("html, body").animate({ scrollTop: 0 }, 600);
        return false;
    });
});

//go to top end

/* Toggle */

$("#all-category button.dropdown-toggle").click(function () { $(".m-m-c").slideToggle("fast"); $(".wrc-tlg,.wrc-acl,.hr-ct-tlg").slideUp("slow") });
/* Toggle End*/

/* Filter - Responsive*/
function optionFilter() {
    if ($(window).width() <= 767) {
        $('#column-left .refine-filter-box').appendTo('.row #content .category-refine');
        $('#column-right .refine-filter-box').appendTo('.row #content .category-refine');
    } else {
        $('.row #content .category-refine .refine-filter-box').appendTo('#column-left .refine-filter-box');
        $('.row #content .category-refine .refine-filter-box').appendTo('#column-right .refine-filter-box');
    }
}
$(document).ready(function () { optionFilter(); });
$(window).resize(function () { optionFilter(); });

function HoverWatcher(selector) {
    this.hovering = false;
    var self = this;

    this.isHoveringOver = function () {
        return self.hovering;
    }

    $(selector).hover(function () {
        self.hovering = true;
    }, function () {
        self.hovering = false;
    })
}

//alert theme
var ALERT_TITLE = "Welcome SpicyMix..!";
var ALERT_BUTTON_TEXT = "Ok";

if (document.getElementById) {
    window.alert = function (txt) {
        createCustomAlert(txt);
    }
}

function createCustomAlert(txt) {
    d = document;

    if (d.getElementById("header-shadow")) return;

    mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
    mObj.id = "header-shadow";
    mObj.style.height = d.documentElement.scrollHeight + "px";

    alertObj = mObj.appendChild(d.createElement("div"));
    alertObj.id = "alertBox";
    if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
    alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
    alertObj.style.visiblity = "visible";

    h1 = alertObj.appendChild(d.createElement("h1"));
    h1.appendChild(d.createTextNode(ALERT_TITLE));

    msg = alertObj.appendChild(d.createElement("p"));
    //msg.appendChild(d.createTextNode(txt));
    msg.innerHTML = txt;

    btn = alertObj.appendChild(d.createElement("a"));
    btn.id = "closeBtn";
    btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
    btn.href = "#";
    btn.focus();
    btn.onclick = function () { removeCustomAlert(); return false; }

    alertObj.style.display = "block";

}

function removeCustomAlert() {
    document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainer"));
}


