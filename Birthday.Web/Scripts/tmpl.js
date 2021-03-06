﻿var ImgPropsLeft = "#ImageProps_{0}__Left";
var ImgPropsTop = "#ImageProps_{0}__Top";
var ImgPropsWidth = "#ImageProps_{0}__Width";
var TextsText = "#Texts_{0}__Text";

function setImgProps($img, left, top, width) {

    $img.css({
        'left': left + 'px',
        'top': top + 'px',
        'width': width + 'px'
    });
}

function initImgProps($img) {

    var id = $img.data('id');

    setImgProps($img,
        $(formatString(ImgPropsLeft, [id])).val(),
        $(formatString(ImgPropsTop, [id])).val(),
        $(formatString(ImgPropsWidth, [id])).val());
}

$(function () {

    $('.f-img').each(function (index, elem) {
        initImgProps($(elem));
    });

    var TextPropsText = "#Texts_{0}__Text";

    $('.txt').each(function (index, elem) {
        initText($(elem));
    });

    function initText($txt) {
        $txt.html($(formatString(TextsText, [$txt.data('id')])).val());
    }
});