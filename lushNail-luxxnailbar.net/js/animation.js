$(document).ready(function () {

    //Notification
    $("#closenoti").click(function () {
        $("#groupnoti").css("display", "none");
    });
    //End Notification

    //Scroll Top
    $('#linktop').click(function () {
        $("body").animate({ scrollTop: 0 }, '400');
    });

    //top an va hien   

    $(window).scroll(showDiv);
    function showDiv() {
        if ($(window).scrollTop() > 300) {
            $('#linktop').fadeIn('slow');
        }
        else {
            $('#linktop').fadeOut('slow');
        }
    }

    //top an va hien


//    $(".service_menu").on("mouseover", function () {
//        $("#menu ul ul").animate({ opacity: "1.0", height: "140px" }, 100);
//    });


    $(".service_menu").on({
        mouseenter: function () {
            $("#menu ul ul").animate({ opacity: "1.0", height: "140px", marginTop:"10px" }, 10);
        },
        mouseleave: function () {
            $("#menu ul ul").animate({ opacity: "0.0", marginTop: "-10px" }, 100);
        }
    });




    //chay menutrong mobile    

    var playmenu = "1";
    $(".menuimg").click(function () {
        if (playmenu == "1") {
            Menu_mlick1();
        }
        else {
            Menu_mlick2();
        }
    });
    function Menu_mlick1() {
        $(".menu_m").stop(true, true).animate({ height: "420px" }, 400);
        playmenu = "0";
    }
    function Menu_mlick2() {
        $(".menu_m").stop(true, true).animate({ height: "40px" }, 400);
        playmenu = "1";
    }


    // chay va tat form subscribe
    $("#subbar").click(function () {
        $("#subbox").fadeIn(200);
    });
    $("#subbar_m").click(function () {
        $("#subbox").fadeIn(200);
    });

    $("#otpcancel").click(function () {
        $("#subbox").fadeOut(200);
        $("#firstname").val("");
        $("#lastname").val("");
        $("#email").val("");
    });
    $("#subtop").click(function () {
        $("#subbox").fadeIn(200);
    });
    //End chay va tat form subscribe

    //opt bar
    $("#otpsubmit").click(function () {
        $("#otpsubmit").fadeOut(50);
        var sNotify = "";
        if ($("#firstname").val() == "") {
            sNotify += "* Please input the first name<br/>";
        }
        if ($("#lastname").val() == "") {
            sNotify += "* Please input the last name<br/>";
        }
        if ($("#email").val() == "") {
            sNotify += "* Please input the Email<br/>";
        }
        else {
            if (!checkEmail($("#email").val())) {
                sNotify += "*Invalid Email<br/>";
            }
        }

        if (sNotify != "") {
            $("#optnotifycontent").html(sNotify);
            $("#optnotify").fadeIn(50);
            $("#otpsubmit").fadeIn(50);
        }
        else {
            var sFirstName = $("#firstname").val();
            var sLastName = $("#lastname").val();
            var sEmail = $("#email").val();
            $.ajax(
            {
                type: "POST",
                url: "process.aspx/optemail",
                data: "{'sFirstName':'" + sFirstName + "','sLastName':'" + sLastName + "','sEmail':'" + sEmail + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: "true",
                cache: "false",

                success: function (msg) {
                    if (msg.d == "0") {
                        $("#optnotifycontent").html("Your infomation has been submitted. Thank you!");
                        $("#optnotify").fadeIn(50);
                        $("#firstname").val("");
                        $("#lastname").val("");
                        $("#email").val("");
                    }
                    else {
                        $("#optnotifycontent").html("This email has been added. Please enter another email!");
                        $("#optnotify").fadeIn(50);
                        $("#email").val("");
                    }
                    $("#otpsubmit").fadeIn(50);
                },

                Error: function (x, e) {
                    $("#optnotifycontent").html("Some Error! Please try again later");
                    $("#optnotify").fadeIn(50);
                }
            });

        }

    });

    $("#notifyok").click(function () {
        $("#optnotifycontent").html("");
        $("#optnotify").fadeOut(50);
    });

    function checkEmail(email) {
        var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email)) {
            return false;
        }
        else {
            return true;
        }
    }

    //opt bar

    //Contact Form
    $("#bSend").click(function () {
        $("#bSend").fadeOut(50);
        $("#formwait").fadeIn(20);

        //Check form
        var sNotify = "";

        //Name
        if ($("#txtName").val() == "") {
            sNotify += "* Please input the first name<br/>";
        }

        //Email
        if ($("#txtEmail").val() == "") {
            sNotify += "* Please input the Email<br/>";
        }
        else {
            if (!checkEmail($("#txtEmail").val())) {
                sNotify += "*Invalid Email<br/>";
            }
        }

        //Phone
        if ($("#txtPhone").val() == "") {
            sNotify += "* Please input your phone number<br/>";
        }
        else {
            if (!checkPhone($("#txtPhone").val())) {
                sNotify += "*Invalid Phone Number<br/>";
            }
        }

        //Content
        //Name
        if ($("#txtContent").val() == "") {
            sNotify += "* Please input the message<br/>";
        }

        if (sNotify != "") {
            $("#formnotifycontent").html(sNotify);
            $("#formnotify").fadeIn(50);
            $("#bSend").fadeIn(50);
            $("#formwait").fadeOut(20);
        }
        else {
            var sName = $("#txtName").val();
            var sEmail = $("#txtEmail").val();
            var sPhone = $("#txtPhone").val();
            var sContent = $("#txtContent").val();

            $.ajax(
            {
                type: "POST",
                url: "process.aspx/contactus",
                data: "{'sName':'" + sName + "','sEmail':'" + sEmail + "','sPhone':'" + sPhone + "','sContent':'" + sContent + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: "true",
                cache: "false",

                success: function (msg) {
                    if (msg.d == "0") {
                        $("#formnotifycontent").html("Your message has been sent. Thank you!");
                        $("#formnotify").fadeIn(50);
                        $("#txtName").val("");
                        $("#txtEmail").val("");
                        $("#txtPhone").val("");
                        $("#txtContent").val("");
                    }
                    $("#formwait").fadeOut(20);
                    $("#formsubmit").fadeIn(50);
                },

                Error: function (x, e) {
                    $("#formnotifycontent").html("Some Error! Please try again later");
                    $("#formnotify").fadeIn(50);
                }
            });
        }
    });

    $("#formnotifyok").click(function () {
        $("#formnotifycontent").html("");
        $("#formnotify").fadeOut(50);
    });

    function checkEmail(email) {
        var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email)) {
            return false;
        }
        else {
            return true;
        }
    }

    function checkPhone(email) {
        var filter = /\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/;
        if (!filter.test(email)) {
            return false;
        }
        else {
            return true;
        }
    }
    //Contact Form


});

function myNavFunc() {
    // If it's an iPhone..
    var p = navigator.platform;
    if (p === 'iPad' || p === 'iPhone' || p === 'iPod') {
        window.open("http://maps.apple.com/?address=2063,Central+Plaza+111,New+Braunfels,TX,78130");
    }
    else {
        window.open("http://maps.google.com/?q=2063,Central+Plaza+111,New+Braunfels,TX,78130");
    }
} 