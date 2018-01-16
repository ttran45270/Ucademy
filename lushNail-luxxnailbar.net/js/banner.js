$(document).ready(function () {
    var Interval;
    Bannerauto();
    function Bannerauto() {
        Interval = setInterval(function () {
            if ($("#banner").is(":hover") == false && document.hasFocus()) {
                BannerNext(3);
            }
        }, 4000);
    }

    $("#nextbt").click(function () {
        BannerNext(3);
        clearTimeout(Interval);
        Bannerauto();
    });


    $("#backbt").click(function () {
        BannerBack(3);
        clearTimeout(Interval);
        Bannerauto();
    });

    function BannerNext(count) {
        var iNext;
        iNext = $("#flag").html();
        $("#banner" + iNext.toString()).stop().animate({ left: '100%' }, 650);
        $("#banner" + iNext.toString()).fadeOut(10);
        if (iNext == count) {
            iNext = 1;
        }
        else {
            iNext = (parseInt(iNext) + 1).toString();
        }
        $("#banner" + iNext.toString()).animate({ left: '-90%' }, 10);
        $("#banner" + iNext.toString()).fadeIn(10);
        $("#banner" + iNext.toString()).animate({ left: '0px' }, 600);
        $("#flag").html(iNext.toString());

    }

    function BannerBack(count) {
        var iBack;
        iBack = $("#flag").html();
        $("#banner" + iBack.toString()).stop().animate({ left: '-100%' }, 650);
        $("#banner" + iBack.toString()).fadeOut(10);
        if (iBack == 1) {
            iBack = count;
        }
        else {
            iBack = (parseInt(iBack) - 1).toString();
        }
        $("#banner" + iBack.toString()).animate({ left: '90%' }, 10);
        $("#banner" + iBack.toString()).fadeIn(10);
        $("#banner" + iBack.toString()).animate({ left: '0px' }, 600);
        $("#flag").html(iBack.toString());

    }



});
