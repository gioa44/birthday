/// <reference path="jquery.js" />

$(function () {

    return;

    SetupScroll();

    $('.f-photo,.f-cover').css("background-size", "cover");

    $('.f-details-to-popup').click(function (e) {
        e.stopPropagation();
        e.preventDefault();
        var data = $(this).data();
        partialLoadToPopup(updateQueryStringParameter(location.href, 'rowid', data.id), '.f-details-form', data.name, 'x-form-details-popup');
    });

    $('.f-search input').keypress(function (e) {
        if (e.which == 13) {
            e.stopPropagation();
            e.preventDefault();

            eval($(this).next().attr('href'));
        }
    });

    $(document).on("click", '.f-iframe-popup', showPageInFramePopup);
    $(document).on('click', '.f-collapsor', function (e) {
        e.preventDefault();
        if (!$(this).hasClass('collapsed')) {
            $(this).addClass('collapsed');
            $(this).parent().parent().find('.f-content').slideUp('fast');
        }
        else {
            $(this).removeClass('collapsed');
            $(this).parent().parent().find('.f-content').slideDown('fast');
        }
    });

    var $dateBoxes = $('.x-date-box');
    if (typeof ($dateBoxes.datepicker) !== 'undefined') {
        $dateBoxes.datepicker({
            showOn: "both",
            buttonImage: "../../../content/images/new/calendar.png",
            buttonImageOnly: true,
            dateFormat: 'dd/mm/yy'
        });
    }
});

var JSON = JSON || {};

// implement JSON.stringify serialization
JSON.stringify = JSON.stringify || function (obj) {

    var t = typeof (obj);
    if (t != "object" || obj === null) {

        // simple data type
        if (t == "string") obj = '"' + obj + '"';
        return String(obj);

    }
    else {

        // recurse array or object
        var n, v, json = [], arr = (obj && obj.constructor == Array);

        for (n in obj) {
            v = obj[n]; t = typeof (v);

            if (t == "string") v = '"' + v + '"';
            else if (t == "object" && v !== null) v = JSON.stringify(v);

            json.push((arr ? "" : '"' + n + '":') + String(v));
        }

        return (arr ? "[" : "{") + String(json) + (arr ? "]" : "}");
    }
};

if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (searchElement /*, fromIndex */) {
        'use strict';
        if (this == null) {
            throw new TypeError();
        }
        var n, k, t = Object(this),
            len = t.length >>> 0;

        if (len === 0) {
            return -1;
        }
        n = 0;
        if (arguments.length > 1) {
            n = Number(arguments[1]);
            if (n != n) { // shortcut for verifying if it's NaN
                n = 0;
            } else if (n != 0 && n != Infinity && n != -Infinity) {
                n = (n > 0 || -1) * Math.floor(Math.abs(n));
            }
        }
        if (n >= len) {
            return -1;
        }
        for (k = n >= 0 ? n : Math.max(len - Math.abs(n), 0) ; k < len; k++) {
            if (k in t && t[k] === searchElement) {
                return k;
            }
        }
        return -1;
    };
}

var urlParams;
(window.onpopstate = function () {
    var match,
        pl = /\+/g,  // Regex for replacing addition symbol with a space
        search = /([^&=]+)=?([^&]*)/g,
        decode = function (s) { return decodeURIComponent(escape(s.replace(pl, " "))); },
        query = window.location.search.substring(1);

    urlParams = {};
    while (match = search.exec(query))
        urlParams[decode(match[1]).toLowerCase()] = decode(match[2]);
})();

function updateQueryStringParameter(uri, key, value) {
    var re = new RegExp("([?|&])" + key + "=.*?(&|#|$)", "i");
    if (uri.match(re)) {
        return uri.replace(re, '$1' + key + "=" + value + '$2');
    } else {
        var hash = '';
        var separator = uri.indexOf('?') !== -1 ? "&" : "?";
        if (uri.indexOf('#') !== -1) {
            hash = uri.replace(/.*#/, '#');
            uri = uri.replace(/#.*/, '');
        }
        return uri + separator + key + "=" + value + hash;
    }
}

function formatString(str, replacements) {
    for (var i = 0; i < replacements.length; i++) {
        var token = '{' + i + '}';
        do {
            str = str.replace(token, replacements[i]);
        } while (str.indexOf(token) !== -1)
    }

    return str;
}

function showPersonPopup(id) {
    //var w = 890;
    //var h = 580;
    //var l = (screen.width - w) / 2;
    //var t = (screen.height - h) / 2;
    //winprops = 'scrollbars=1, resizable=0, status=0, toolbar=0, height=' + h + ',width=' + w + ',top=' + t + ',left=' + l;
    //var f = window.open('/_SelfService/Controls/Employee/EmployeeInfoDialog.aspx' + '?personid=' + id, "Persons", winprops);
    //f.focus();
    $('.x-person-info-popup').fadeOut('fast', function () { $(this).remove(); });
    partialLoadToPopup('/_SelfService/Controls/Employee/EmployeeInfoDialog.aspx' + '?personid=' + id, '.f-person-info', null, 'x-person-info-popup', function () {
        initViewable();
        $('.f-photo').css("background-size", "cover");
        $.getScript('/system/scripts/selectivizr.js');
        $.getScript('/scripts/jquery.avatarhoverWidget.js', function () {
            $('.f-substitution-cont').avatarhoverwidget({
                target: '.f-substitute-person-card'
            });
        });
    });
}

function showPagePopup(url, title, width, height) {
    var w = width || 890;
    var h = height || 580;
    var l = (screen.width - w) / 2;
    var t = (screen.height - h) / 2;
    winprops = 'scrollbars=1, resizable=0, status=0, toolbar=0, height=' + h + ', width=' + w + ', top=' + t + ', left=' + l;
    var f = window.open(url, '_blank', winprops);
    f.document.title = title;
    f.focus();
}

function showReportPopup(query) {
    var w = 900;
    var h = 700;
    var l = (screen.width - w) / 2;
    var t = (screen.height - h) / 2;
    winprops = 'scrollbars=1, resizable=1, status=0, toolbar=0, height=' + h + ',width=' + w + ',top=' + t + ',left=' + l;
    var f = window.open('/_SelfService/Pages/Intelligence/ReportPopup.aspx?' + query, "Report", winprops);
    f.focus();
}

//WaterMark
function initWaterMarks() {
    $('input.watermark, textarea.watermark').not('.rc_readonly').each(function () {        
        var $elem = $(this);
        var wText = $elem.attr('title');
        var $wMark = getWatermarkDiv($elem);

        if ($wMark.length == 0) {
            $wMark = $('<div>');
            $wMark.addClass('watermark');
            $wMark.text(wText);
            $wMark.attr('data-target', $elem.attr('id'));
            $wMark.css({
                'font-family': $elem.css('font-family'),
                'font-size': $elem.css('font-size'),
                'display': 'none'
            });
            positionWaterMark($elem, $wMark);
            $elem.before($wMark);
            toggleWaterMark($elem, true);
        }
        else {
            $wMark.text(wText);
        }
    });

    $('div.watermark').click(function () {
        var targetID = $(this).attr('data-target');
        $('#' + targetID).focus();
        $(this).hide();
    });

    $(window).resize(function () {
        $('input.watermark, textarea.watermark').not('.rc_readonly').each(function () {
            var $elem = $(this);
            var $wMark = getWatermarkDiv($elem);
            if ($wMark.length > 0) {
                positionWaterMark($elem, $wMark);
            }
        });
    });

    function getWatermarkDiv($elem) {
        return $('div.watermark[data-target="' + $elem.attr('id') + '"]')
    }

    function bindWaterMarkEvents($elements) {
        unbindWaterMarkEvents($elements);
        $elements.bind({
            'blur.WaterMark change.WaterMark': function () {
                toggleWaterMark($(this), true);
            },
            'focus.WaterMark': function () {
                toggleWaterMark($(this), false);
            }
        });
    }

    function unbindWaterMarkEvents($elements) {
        $elements.unbind('.WaterMark');
    }

    function toggleWaterMark($element, addWaterMark, unbindEvents) {
        var $wMark = getWatermarkDiv($element);

        if ($wMark.length > 0) {
            if (addWaterMark == true && $element.val() == '') {
                $wMark.show();
            }
            else {
                $wMark.hide();
            }
            if (typeof unbindEvents != 'undefined' && unbindEvents == true) {
                unbindWaterMarkEvents($element);
            }
            else {
                bindWaterMarkEvents($element);
            }
        }
    }

    function positionWaterMark($elem, $wMark) {
        var pos = $elem.position();
        var heightFix;

        if ($wMark.height() > 0) {
            heightFix = ($wMark.outerHeight(true) / 2);
        }
        else {
            var $copied_elem = $wMark.clone()
                          .attr("id", false)
                          .css({
                              visibility: "hidden",
                              display: "block",
                              position: "absolute"
                          });

            $("body").append($copied_elem);

            heightFix = ($copied_elem.outerHeight(true) / 2);

            $copied_elem.remove();
        }

        $wMark.css({
            'left': pos.left
            + (parseInt($elem.css('margin-left'), 10) || 0)
            + (parseInt($elem.css('border-left-width'), 10) || 0)
            + (parseInt($elem.css('padding-left'), 10) || 0) + 1,
            'top': pos.top
                   + (parseInt($elem.css('margin-top'), 10) || 0)
                   + (parseInt($elem.css('border-top-width'), 10) || 0)
                   + (parseInt($elem.css('padding-top'), 10) || 0)
                   + ($elem.height() / 2)
                   - heightFix
        });
    }
}
//~WaterMark

//Error window

function showError(errorText, errorTitle) {
    var popup = new XPopup(true);

    popup.setTitle(errorTitle);
    popup.setContent(errorText);
    popup.addClass('x-error-window');

    popup.show();
}

//~Error window

//Ajax call

function ajaxCall(url, data, onSuccess, onError, overrideError) {
    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            onSuccess(data.d);
        }
        , error: function (xhr, textStatus, error) {
            overrideError = overrideError || false;

            if (!overrideError && xhr.responseText) {
                showError($.parseJSON(xhr.responseText).Message, "Error");
            }

            if (typeof onError !== 'undefined') {
                onError();
            }
        }
    });
}

//~Ajax call

//Popup

var XPopupActiveIndex = 1000;
var XPopup = (function () {

    var Res = {
        ModalOpenedClass: 'x-modal-opened',
        PopupClassSel: '.x-popup'
    };

    function XPopup(newInstance) {
        this.__init(newInstance);
    }

    function getOpenedPopupCount() {
        return $(Res.PopupClassSel + ':visible').length;
    }

    function setActive($popup) {
        XPopupActiveIndex++;
        $popup.css('z-index', XPopupActiveIndex);
        $popup[0].focus();
    }

    function activateNearest() {
        var $popups = $(Res.PopupClassSel + ':visible');

        var $nearestPopup = $popups.first();

        $popups.each(function (index, elem) {

            var $elem = $(elem);

            if ($elem.css('z-index') > $nearestPopup.css('z-index')) {
                $nearestPopup = $elem;
            }
        });

        setActive($nearestPopup);
    }

    XPopup.prototype = {
        __init: function (newInstance) {
            var that = this;

            this.$popup = $(Res.PopupClassSel);
            this.$overlay = $('.x-overlay');

            if (this.$popup.length == 0 || newInstance == true) {
                this.$popup = $('<div class="x-popup" style="display: none;">' +
                                '<div class="title-bar no-title"><span class="title"></span>' +
                                '<span class="close-icon"></span></div>' +
                                '<div class="content"></div>' +
                                '<div class="action-bar"></div>' +
                                '</div>');

                $('form').append(this.$popup);

                this.$popup.draggable({ handle: '.header, .title-bar' });

                this.$popup.mousedown(function (e) {
                    if (navigator.userAgent.indexOf('MSIE 8.0') === -1 || e.srcElement.tagName !== 'SELECT') {
                        setActive(that.$popup);
                        e.stopPropagation();
                    }                 
                });

                this.$popup.find('.close-icon').click(function () {
                    that.hide();
                });

                this.$popup.bind('keydown', function (event) {
                    switch (event.which) {
                        case 27/* escape */:
                            that.hide();
                            break;
                    }
                });

                this.$title = this.$popup.find('.title');
                this.$content = this.$popup.find('.content');
                this.$actionBar = this.$popup.find('.action-bar');
            }

            if (this.$overlay.length == 0) {
                this.$overlay = $('<div class="x-overlay"></div>');
                $('body').append(this.$overlay);

                this.$overlay.click(function (e) {
                    e.stopPropagation();
                });
            }

            this.clear();
        },
        __setPositions: function () {
            var height = document.documentElement.clientHeight;
            var width = document.documentElement.clientWidth;
            var popupHeight = this.$popup.height();
            var popupWidth = this.$popup.width();

            this.$popup.css({ top: (height - popupHeight) / 2, left: (width - popupWidth) / 2 });
        },
        addClass: function (className) {
            this.$popup.addClass(className);
        },
        setWidth: function (width) {
            this.$popup.width(width);
            this.$popup.css('min-width', width);
        },
        setTitle: function (title) {
            this.$title.text(title)
                .parent().removeClass('no-title');
        },
        setContent: function ($html) {
            this.$content.append($html);
        },
        setActions: function (actions) {
            for (var i in actions) {
                this.$actionBar.append('&nbsp;&nbsp;');
                this.$actionBar.append(actions[i]);
            }
        },
        clear: function () {
            this.$popup.attr('class', 'x-popup');
            this.$title.text('');
            this.$content.children().remove();
            this.$actionBar.children().remove();
        },
        show: function () {
            this.__setPositions();

            var that = this;

            $(window).resize(function () {
                that.__setPositions();
            });

            var openCount = getOpenedPopupCount();
            if (openCount == 0) {
                this.$overlay.show();
                $('body').addClass(Res.ModalOpenedClass);
            }

            this.$popup.attr('tabindex', -1);

            this.$popup.show();

            setActive(that.$popup);
        },
        hide: function () {
            this.$popup.hide();
            var openCount = getOpenedPopupCount();
            if (openCount == 0) {
                this.$overlay.hide();
                $('body').removeClass(Res.ModalOpenedClass);
            }
            else {
                activateNearest();
            }
        },
        remove: function () {
            this.$popup.remove();
            var openCount = getOpenedPopupCount();
            if (openCount == 0) {
                this.$overlay.hide();
                $('body').removeClass(Res.ModalOpenedClass);
            }
        },
        addActionButton: function (text, onclick, type) {
            type = type || '';
            if (type == 1) {
                type = ' x-sec';
            }

            var $actionButton = $('<a class="x-button' + type + '" />');
            $actionButton.text(text);

            $actionButton.click(function (e) {
                onclick(e);
            });

            this.$actionBar.append($actionButton);

            return $actionButton;
        }
    };

    XPopup.constructor = XPopup;

    return XPopup;
})();

//~Popup

//Simple Slider

function initSimpleSlider($slider, opened) {
    var $trigger = $('<span class="simple-slider-trigger open"></span>');

    $slider.prepend($trigger);

    if (typeof opened !== 'undefined') {
        if (opened) {
            toggleSlider($trigger);
        }
    }

    $trigger.click(function () {
        toggleSlider($(this));
    });

    $trigger.next('.simple-slider-title').click(function () {
        toggleSlider($(this).prev());
    });

    function toggleSlider($trigger) {

        if ($trigger.hasClass('open')) {
            $trigger.removeClass('open');
            $trigger.addClass('close');
            $slider.find('> .simple-slider-content').show();
        }
        else {
            $trigger.removeClass('close');
            $trigger.addClass('open');
            $slider.find('> .simple-slider-content').hide();
        }
    }
}

//~Simple Slider

//Serialize json 

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

//~Serialize json 

function tooltipize() {
    $('.tooltipize').tooltip({
        position: {
            my: "center bottom-12",
            at: "center+5 top",            
            using: function (position, feedback) {
                $(this).css(position);
                $("<div>")
                  .addClass("x-tooltip-arrow")
                  .addClass(feedback.vertical)
                  .addClass(feedback.horizontal)
                  .appendTo(this);
            }
        }
    });
}

function tabify() {
    $('.tabs').tabs();

    $('.x-tabs > ul > li > a').click(function (e) {
        location.href = $(this).attr('href');
    });

    $('.x-tabs').tabs({
        active: urlParams['t'],
        beforeActivate: function (event) {
            event.preventDefault();
            return false;
        }
    });
}

function selectify() {
    $('.form-container select').not('.f-inline-table-cont select').chosen({ disable_search_threshold: 10, placeholder_text_single: ' ' });

    $('#search-in, .fg-search select').chosen({
        disable_search_threshold: 10,
        placeholder_text_single: ' ',
        width: '100%'
    });

    $('select.selectify').chosen({ disable_search_threshold: 10, placeholder_text_single: ' ' });
}

function partialLoad(url, sourceSelector, $targetObject, onComplete) {
    $targetObject.load(url + ' ' + sourceSelector, function () {

        if (typeof onComplete !== 'undefined') {
            onComplete();
        }
    });

    return $targetObject;
}

function partialLoadToPopup(url, sourceSelector, popupName, cssClass, onComplete) {
    var $content = $('<div>');

    partialLoad(url, sourceSelector, $content, function () {
        var popup = new XPopup(true);

        popup.addClass(cssClass);
        if ((popupName || '') != '') {
            popup.setTitle(popupName);
        }
        popup.setContent($content);

        popup.show();

        if (typeof onComplete !== 'undefined') {
            onComplete();
        }
    });
}

function initViewable(callback) {

    $('.f-viewable-trigger').click(function (e) {
        e.preventDefault();
        e.stopPropagation();

        var $t = $(this).parents('.f-viewable');
        var $trigger = $t.find('.f-viewable-trigger');

        var text = $trigger.data('alt-text');
        $trigger.data('alt-text', $trigger.text());
        $trigger.text(text);

        var $items = $t.find('.f-viewable-item');
        if ($items.hasClass('hid')) {
            $items.removeClass('hid');
        } else {
            $items.addClass('hid');
        }
        if (typeof callback != 'undefined') {
            callback(e, this);
        }
    });
}

function showPageInFramePopup(e) {
    e.preventDefault();
    e.stopPropagation();

    var width = $(this).data("width") || 500;
    var height = $(this).data("height") || 500;
    var title = $(this).data("title") || "";
    var url = $(this).data("href");

    var $frame = $('<iframe>', {
        src: url,
        id: 'myFrame',
        frameborder: 0,
        width: '100%',
        height: height + "px"
    });

    var popup = new XPopup(true);
    if (title != '') {
        popup.setTitle(title);
    }
    popup.setWidth(width);
    popup.setContent($frame.show());
    popup.show();
}

function SetupScroll() {
    $(document).on('mousewheel', '.f-scrollable', function (e) {
        var delta = e.originalEvent.wheelDelta;
        this.scrollTop += (delta < 0 ? 1 : -1) * 30;
        e.preventDefault();
    });
    $(document).on('mousewheel', '.f-scrollable-x', function (e) {
        var delta = e.originalEvent.wheelDelta;
        this.scrollLeft += (delta < 0 ? 1 : -1) * 50;
        e.preventDefault();
    });

    //$("div, ul").on('mousewheel', function (e) {        
    //    if ($(this).has_scrollbar()) {         
    //        var delta = e.originalEvent.wheelDelta;
    //        this.scrollTop += (delta < 0 ? 1 : -1) * 30;
    //        e.preventDefault();
    //    }
    //    else if ($(this).has_H_scrollbar()) {            
    //        var delta = e.originalEvent.wheelDelta;
    //        this.scrollLeft += (delta < 0 ? 1 : -1) * 50;
    //        e.preventDefault();
    //    }
    //});  
}

//$.fn.has_scrollbar = function () {
//    var divnode = this.get(0);   
//    if (divnode.scrollHeight > divnode.clientHeight) {        
//        return true;
//    }
//}
//$.fn.has_H_scrollbar = function () {    
//    var divnode = this.get(0);    
//    if (divnode.scrollWidth > divnode.clientWidth)
//        return true;
//}