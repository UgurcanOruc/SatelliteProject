// ---------------------- AOS INIT ---------------------------
$(document).ready(function () {
    AOS.init();
});
// ------------------- AOS INIT Bitişi------------------------



// ---------------------- Choose File JS ----------------------------

    // Add the following code if you want the name of the file appear on select
$(".custom-file-input").on("change", function() {
  var fileName = $(this).val().split("\\").pop();
  $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});

// ------------------ Choose File JS Bitişi -------------------------


// ----------------- Back To Top Button Metotları --------------------------------
$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 1200) {
            $('.back-to-top').fadeIn();
        } else {
            $('.back-to-top').fadeOut();
        }
    });
    // scroll body to 0px on click
    $('.back-to-top').click(function () {
        $('body,html').animate({
            scrollTop: 0
        }, 400);
        return false;
    });
});
// ----------------- Back To Top Button Metotları Bitişi --------------------------------


// ---------------- Kart Mouse Hareketleri Metotları--------------------------
//$(document).ready(function () {
//    // Init
//    var container = document.getElementById("container"),
//        inner = document.getElementById("inner");

//    // Mouse
//    var mouse = {
//        _x: 0,
//        _y: 0,
//        x: 0,
//        y: 0,
//        updatePosition: function (event) {
//            var e = event || window.event;
//            this.x = e.clientX - this._x;
//            this.y = (e.clientY - this._y) * -1;
//        },
//        setOrigin: function (e) {
//            this._x = e.offsetLeft + Math.floor(e.offsetWidth / 2);
//            this._y = e.offsetTop + Math.floor(e.offsetHeight / 2);
//        },
//        show: function () {
//            return "(" + this.x + ", " + this.y + ")";
//        }
//    };

//    // Track the mouse position relative to the center of the container.
//    mouse.setOrigin(container);

//    //----------------------------------------------------

//    var counter = 0;
//    var refreshRate = 10;
//    var isTimeToUpdate = function () {
//        return counter++ % refreshRate === 0;
//    };

//    //----------------------------------------------------

//    var onMouseEnterHandler = function (event) {
//        update(event);
//    };

//    var onMouseLeaveHandler = function () {
//        inner.style = "";
//    };

//    var onMouseMoveHandler = function (event) {
//        if (isTimeToUpdate()) {
//            update(event);
//        }
//    };

//    //----------------------------------------------------

//    var update = function (event) {
//        mouse.updatePosition(event);
//        updateTransformStyle(
//            (mouse.y / inner.offsetHeight / 2).toFixed(2),
//            (mouse.x / inner.offsetWidth / 2).toFixed(2)
//        );
//    };

//    var updateTransformStyle = function (x, y) {
//        var style = "rotateX(" + x + "deg) rotateY(" + y + "deg)";
//        inner.style.transform = style;
//        inner.style.webkitTransform = style;
//        inner.style.mozTranform = style;
//        inner.style.msTransform = style;
//        inner.style.oTransform = style;
//    };

//    //--------------------------------------------------------

//    container.onmousemove = onMouseMoveHandler;
//    container.onmouseleave = onMouseLeaveHandler;
//    container.onmouseenter = onMouseEnterHandler;
//});

// ---------------- Kart Mouse Hareketleri Metotları Bitişi --------------------------


//----------------- SUMMERNOTE JS ----------------------------
$(document).ready(function () {
    $('.summernote').summernote({
        placeholder: 'Makale İçeriğinizi Buraya Yazabilirsiniz...',
        tabsize: 2,
        height: 750,
    });
});
////----------------- SUMMERNOTE JS BİTİŞİ ---------------------


//$(document).ready(function () {
//    $(function () {
//        $("#btnYeniMakalePost").click(function () {
//            const categories = [];
//            for (var i = 1; i < 15; i++) {
//                if ($('#' + i.toString()).) {
//                    categories.push($('#' + i.toString()).val());
//                }
//            }
//            if (categories != null) {
//                $.ajax({
//                    type: "POST",
//                    url: "/Makale/YeniMakale",
//                    data: JSON.stringify(categories),
//                    contentType: "application/json; charset=utf-8",
//                    dataType: "json",
//                    success: function (response) {
//                        if (response != null) {
//                            alert("Alert");
//                        } else {
//                            alert("Something went wrong");
//                        }
//                    },
//                    failure: function (response) {
//                        alert(response.responseText);
//                    },
//                    error: function (response) {
//                        alert(response.responseText);
//                    }
//                });
//            }
//        });
//    });
//});