$(function () {

    var processingImageID = 0;
    $('.f-img').hide();
    initImageReposition($('.f-img'));

    $('.f-imgupload').click(function (e) {
        e.preventDefault();

        processingImageID = $(this).data("img-id");

        var $uploadSection = $('#upload-section');

        $uploadSection.find('#ImageIndex').val(processingImageID);

        var popup = new XPopup(true);

        popup.setTitle("Upload");
        popup.setContent($uploadSection.show());

        registerAjaxForm($('#img-upload-form'), function (data) {
            var $img = $('.f-img[data-id="' + processingImageID + '"]');
            var src = $img.attr('src');

            $img.attr('src', src);

            getImgProps($img);

            popup.hide();
        });

        popup.show();
    });
});

var ImgPropsLeft = "#ImageProps_{0}__Left";
var ImgPropsTop = "#ImageProps_{0}__Top";
var ImgPropsWidth = "#ImageProps_{0}__Width";

function initImageReposition($images) {
    var preventShrink = false;

    $images.each(function (index, elem) {
        var $img = $(elem);
        var id = $img.data('id');

        var $span = $('<span>');
        $span.addClass("f-imgupload x-btn-imgupload");
        $span.data('img-id', id);
        $span.text('Upl');
        $img.after($span);
    });

    $images.one('load', function () {
        var $img = $(this);

        var pos = $img.position();

        setImgProps($img);

        setupImageTool($img, false);

        $img.show();
    })
    .each(function () {
        if (this.complete) $(this).load();
    })
    .mousewheel(function (event) {

        event.preventDefault();

        var $img = $(this);

        //loghandle(event);

        var step = 20;

        if (event.deltaY < 0) {
            if (preventShrink) {
                return;
            }

            step = -step;
        }
        else {
            preventShrink = false;
        }

        var width = $img.width();
        var height = $img.height();

        var newWidth = width + step;

        if (step > 0 || $img.parent().offset().left + $img.parent().width() < $img.offset().left + newWidth) {

            $img.css('width', newWidth);

            if (step < 0 && $img.parent().offset().top + $img.parent().height() >= $img.offset().top + $img.height()) {
                preventShrink = true;
                newWidth = width;
                $img.css('width', newWidth);
            }
            else {
                setupImageTool($img);
            }

            $(formatString(ImgPropsWidth, [$img.data('id')])).val(newWidth);
        }
    });

    function setupImageTool($img, moveToCenter) {
        var width = $img.width();
        var height = $img.height();

        var $parent = $img.parent();

        var pWidth = $parent.outerWidth();
        var pHeight = $parent.outerHeight();

        var pOffset = $parent.offset();

        console.log("pWidth: " + pWidth + ", pHeight: " + pHeight);

        var x = width - pWidth;
        var y = height - pHeight;

        console.log("Coord: " + x + " " + y);

        if ((typeof moveToCenter !== 'undefined') && moveToCenter == true) {
            $img.css({ 'left': -x / 2, 'top': -y / 2 });
        }

        $img.draggable({
            containment: [Math.floor(pOffset.left - x), Math.floor(pOffset.top - y), pOffset.left, pOffset.top], drag: function () {
                preventShrink = false;
            },
            drag: function () {
                var pos = $img.position();
                var id = $img.data('id');

                $(formatString(ImgPropsLeft, [id])).val(pos.left);
                $(formatString(ImgPropsTop, [id])).val(pos.top);
            }
        });
    }

    var loghandle = function (event) {
        var o = '', id = event.currentTarget.id;

        o = '#' + id + ':';

        if (event.delta > 0)
            o += ' up (' + delta + ')';
        else if (event.delta < 0)
            o += ' down (' + delta + ')';

        if (event.deltaY > 0)
            o += ' north (' + event.deltaY + ')';
        else if (event.deltaY < 0)
            o += ' south (' + event.deltaY + ')';

        if (event.deltaX > 0)
            o += ' east (' + event.deltaX + ')';
        else if (event.deltaX < 0)
            o += ' west (' + event.deltaX + ')';

        o += ' deltaFactor (' + event.deltaFactor + ')';

        console.log(o);
    };
}

function setImgProps($img) {
    var id = $img.data('id');

    $img.css({
        'left': $(formatString(ImgPropsLeft, [id])).val() + 'px',
        'top': $(formatString(ImgPropsTop, [id])).val() + 'px',
        'width': $(formatString(ImgPropsWidth, [id])).val() + 'px'
    });
}

function getImgProps($img) {
    var id = $img.data('id');
    var pos = $img.position();
    var width = $img.width();

    $(formatString(ImgPropsLeft, [id])).val(pos.left);
    $(formatString(ImgPropsTop, [id])).val(pos.top);
    $(formatString(ImgPropsWidth, [id])).val(width);
}