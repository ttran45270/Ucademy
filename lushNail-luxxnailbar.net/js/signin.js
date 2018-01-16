$(document).ready(function () {
    $("#txtPhone").mask("999-999-9999");
    $("#datepicker").mask("99-99", { placeholder: "MM-DD" });

    $("#bSubmit").click(function () {
        fSignIn();
    });

    function fSignIn() {
        $("#bSubmit").fadeOut(10);
        $("#waitdiv").fadeIn(10);
        var sErr = "";
        var sFirstName = $("#txtFirstName").val();
        var sLastName = $("#txtLastName").val();
        var sEmail = $("#txtEmail").val();
        var sPhone = $("#txtPhone").val();
        var sBirthDate = $("#datepicker").val();

        if (sFirstName == "") {
            sErr += "+ Please enter your First Name";
        }

        if (sEmail == "") {
            if (sErr != "") {
                sErr += "<br><br>";
            }
            sErr += "+ Please enter your Email";
        }
        else {
            if (!validateEmail(sEmail)) {
                if (sErr != "") {
                    sErr += "<br><br>";
                }
                sErr += "+ Invalid Email"
            }
        }

        if (sBirthDate != "") {
            if (validateDate(sBirthDate) == 1) {
                if (sErr != "") {
                    sErr += "<br><br>";
                }
                sErr += "+ Invalid Birthdate"
            }
        }

        if (sErr != "") {
            $("#bSubmit").fadeIn(10);
            $("#waitdiv").fadeOut(10);
            $("#formnotifycontent").html(sErr);
            $("#formnotify").fadeIn(10);
        }
        else {
            $.ajax(
            {
                type: "POST",
                url: "process.aspx/sSign",
                data: "{'sFirstName':'" + sFirstName + "','sLastName':'" + sLastName + "','sEmail':'" + sEmail + "','sPhone':'" + sPhone + "','sBirthDate':'" + sBirthDate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: "true",
                cache: "false",

                success: function (msg) {
                    if (msg.d == "0") {
                        $("#formnotifycontent").html("Your infomation has been submitted. Thank you!");
                        $("#formnotify").fadeIn(50);
                        $("#waitdiv").fadeOut(10);
                        $("#bSubmit").fadeIn(10);
                        fClear();
                    }
                    else {
                        $("#formnotifycontent").html("This email has been added. Please enter another email!");
                        $("#formnotify").fadeIn(50);
                        $("#bSubmit").fadeIn(10);
                        $("#waitdiv").fadeOut(10);
                    }
                },

                Error: function (x, e) {
                    $("#formnotifycontent").html("Some Error! Please try again later");
                    $("#formnotify").fadeIn(50);
                }
            });
        }

    }

    //Ham check Email
    function validateEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    //Ham check Date
    function validateDate(date) {
        var iErr = 0;
        var cDate = date.split("-");
        if (parseInt(cDate[0]) > 12 || parseInt(cDate[1]) > 31) {
            iErr = 1;
        }
        return iErr;
    }

    function fClear() {
        $("#txtFirstName").val("");
        $("#txtLastName").val("");
        $("#txtEmail").val("");
        $("#txtPhone").val("");
        $("#txtFirstName").val("");
        $("#datepicker").val("");
    }

});