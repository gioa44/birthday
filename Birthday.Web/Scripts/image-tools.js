function initImageReposition($images) {
    var preventShrink = false;

    $images.load(function () {
        var $img = $(this);

        setupImageTool($img, true);
    }).mousewheel(function (event) {
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

        if ($img.parent().offset().left + $img.parent().width() < $img.offset().left + newWidth) {

            $img.css('width', newWidth);

            if ($img.parent().offset().top + $img.parent().height() >= $img.offset().top + $img.height()) {
                preventShrink = true;
                $img.css('width', width);
            }
            else {
                setupImageTool($img);
            }
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