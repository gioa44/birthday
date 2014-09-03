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
            var src = $img.attr('src') + '?' + new Date().getTime();

            $img.one('load', function () {
                setImgProps($img, data.ImageLeft, data.ImageTop, data.ImageWidth);
                setImgPropsToHidden($img);
                setupDraggable($img);
            }).attr('src', src);

            popup.hide();
            $uploadSection.find('input[type="file"]').val('');
        });

        popup.show();
    });

    $('#TemplateID').change(function () {
        var id = $(this).val() || 0;

        if (id > 0) {
            ajaxCall('/visualization/SetTemplate', { TemplateID: id }, function () {
                window.location.href = window.location.href;
            });
        }
    });

    function initImageReposition($images) {

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

            setupDraggable($img);
            console.log($img.data('id') + " loaded");
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
                if ($img.data("prevent-shrink") == true) {
                    return;
                }

                step = -step;
            }
            else {
                $img.data("prevent-shrink", false);
            }

            var width = $img.width();
            var height = $img.height();

            var newWidth = width + step;

            if (step > 0 || $img.parent().offset().left + $img.parent().width() < $img.offset().left + newWidth) {

                $img.css('width', newWidth);

                if (step < 0 && $img.parent().offset().top + $img.parent().height() >= $img.offset().top + $img.height()) {
                    $img.data("prevent-shrink", true);
                    newWidth = width;
                    $img.css('width', newWidth);
                }
                else {
                    setupDraggable($img);
                }

                $(formatString(ImgPropsWidth, [$img.data('id')])).val(newWidth);
            }
        });

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

    function setupDraggable($img, moveToCenter) {
        var width = $img.width();
        var height = $img.height();

        var $parent = $img.parent();

        var pWidth = $parent.outerWidth();
        var pHeight = $parent.outerHeight();

        var pOffset = $parent.offset();

        var x = width - pWidth;
        var y = height - pHeight;

        if ((typeof moveToCenter !== 'undefined') && moveToCenter == true) {
            $img.css({ 'left': -x / 2, 'top': -y / 2 });
        }

        $img.draggable({
            containment: [Math.floor(pOffset.left - x), Math.floor(pOffset.top - y), pOffset.left, pOffset.top], drag: function () {
                $img.data("prevent-shrink", false);
            },
            drag: function (event, ui) {
                var id = $img.data('id');

                $(formatString(ImgPropsLeft, [id])).val(ui.position.left);
                $(formatString(ImgPropsTop, [id])).val(ui.position.top);
            }
        });
    }

    function setImgPropsToHidden($img) {
        var id = $img.data('id');
        var pos = $img.position();
        var width = $img.width();

        $(formatString(ImgPropsLeft, [id])).val(pos.left);
        $(formatString(ImgPropsTop, [id])).val(pos.top);
        $(formatString(ImgPropsWidth, [id])).val(width);
    }

    $('.txt').click(function () {
        var $t = $(this);
        var index = $t.data('id');
        var id = 'txt-' + index;
        var $textarea = $('textarea#' + id);

        if ($textarea.length == 0) {
            $textarea = $('<textarea></textarea>').text($t.text());
            $textarea.attr('id', '_' + id);
            $textarea.addClass('txt');
            $textarea.addClass(id);
            $t.after($textarea);
        }

        $textarea.blur(function () {
            var $t2 = $(this);
            var text = $t2.val();

            $t.text(text)
            $(formatString(TextsText, [index])).val(text)

            $t.show();
            $t2.hide();
        });

        $t.hide();
        $textarea.show().focus();
    });
});