(function ($) {
    $(function () {
        var dataBooking=[],
            $jsProducer = $('.js-producer'),
            $jsPhones = $('.js-phones'),
            $jsPhone = $('.js-phone'),
            $jsImg = $('.js-img'),
            $jsTxt = $('.js-txt'),
            $jsFont = $('.js-font'),
            $jsSize = $('.js-size'),
            $jsInputTxt = $('.js-input-txt'),
            $jsMask = $('.js-mask'),
            $jsMaskTxt = $('.m-mask-txt'),
            $jsBtnOrder = $('.js-btn-order'),
            $jsMaskInner = $('.js-mask-inner'),
            $jsColor = $('.js-color');
        //Load
        $(window).on('load resize', function () {
            $jsMask.css({ width: $jsImg.width(), height: $jsImg.height(), top: 'calc((100% - ' + $jsImg.height() +'px) / 2)'});
            $jsMaskInner.css({ width: $jsImg.height() - 20, height: $jsImg.width() - 20});
            $jsTxt.text($jsInputTxt.val());
            if ($jsProducer.length) {
                var $jsPhonesSelect = $('#' + $jsProducer.val());
                if ($jsPhonesSelect.length) {
                    $jsPhonesSelect.addClass('m-phones--show');
                    $jsPhonesSelect.children('.js-phone:first-child').addClass('m-phone--active');
                    $jsImg.attr('src', $jsPhonesSelect.children('.js-phone:first-child').data('img'));
                }
            }
            if ($jsFont.length) {
                var jsFontSelect = $jsFont.val();
                if (jsFontSelect.length) {
                    $jsTxt.addClass('m-mask-txt--' + jsFontSelect);
                }
            }
            if ($jsSize.length) {
                var jsSizeSelect = $jsSize.val();
                if (jsSizeSelect.length) {
                    $jsTxt.addClass('m-mask-txt--' + jsSizeSelect);
                }
            }
        });
        //Select phone type
        $jsProducer.on('change', function () {
            var $jsPhonesSelect = $('#' + $(this).val());
            if ($jsPhonesSelect.length) {
                $jsPhones.removeClass('m-phones--show');
                $jsPhonesSelect.addClass('m-phones--show');
                $jsPhone.removeClass('m-phone--active');
                $jsPhonesSelect.children('.js-phone:first-child').addClass('m-phone--active');
                $jsImg.attr('src', $jsPhonesSelect.children('.js-phone:first-child').data('img'));
                $jsMaskInner.css({ width: $jsImg.height() - 20, height: $jsImg.width() - 20});
            }
        });
        //Select font
        $jsFont.on('change', function () {
            var jsFontSelect = $(this).val();
            if (jsFontSelect.length) {
                $(this).find('option').each(function(index, value) {
                    $jsTxt.removeClass('m-mask-txt--' + $(value).val());
                });
                $jsTxt.addClass('m-mask-txt--' + jsFontSelect);
            }
        });
        //Select size
        $jsSize.on('change', function () {
            var jsSizeSelect = $(this).val();
            if (jsSizeSelect.length) {
                $(this).find('option').each(function(index, value) {
                    $jsTxt.removeClass('m-mask-txt--' + $(value).val());
                });
                $jsTxt.addClass('m-mask-txt--' + jsSizeSelect);
            }
        });
        //Select phone
        $jsPhone.on('click', function () {
            $jsPhone.removeClass('m-phone--active');
            $(this).addClass('m-phone--active');
            $jsImg.attr('src', $(this).data('img'));
            $jsMaskInner.css({ width: $jsImg.height() - 20, height: $jsImg.width() - 20});
        });
        //Change text
        $jsInputTxt.on('change keyup paste', function () {
            $jsTxt.text($(this).val());
        });
        //Select color
        $jsColor.on('change keyup paste', function () {
            $jsTxt.css({color: '#' + $(this).val()});
        });
        //Order
        $jsBtnOrder.on('click', function (event) {
            $jsPhone.each(function( index, value ) {
                if (value.classList[2] == 'm-phone--active') {
                    dataBooking = [];
                    dataBooking.push(
                        {
                            'phone': $(value).text(),
                            'type': $jsFont.children('option:selected').text(),
                            'size': $jsSize.children('option:selected').text(),
                            'color': '#' + $jsColor.val(),
                            'text': $jsInputTxt.val(),
                        }
                    );
                    $.ajax({
                        type: "GET",
                        url: "/umbraco/surface/CartsSurface/AddToCartInOpLung",
                        contentType: "application/json",
                        dataType: "json",
                        data: { "pk": $(value)[0].id, "arr": JSON.stringify(dataBooking)},
                        success: function (data) {
                            $('.count').html(data);
                        },
                        error: function (data) { alert("error"); }
                    });                    
                }
              });
            event.preventDefault();
        });
    });
})(jQuery);