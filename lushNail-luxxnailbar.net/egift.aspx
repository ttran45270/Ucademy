<%@ Page Language="VB" AutoEventWireup="false" CodeFile="egift.aspx.vb" Inherits="egift" %>
<%@Import Namespace="System.Data"%>
<%@Import Namespace="System.Data.SqlClient"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E-Gift - Luxx Nail Bar of Woodbridge, VA</title>
    <meta name="Description" content="E-Gift - Luxx Nail Bar of Woodbridge, VA"/>
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.0/jquery.min.js"></script>
	<link href="css/style.css" rel="stylesheet" type="text/css"/>
    <link href="images/icon.png" rel="shortcut icon"/>
	<!--responsive web-->
	<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
	<!--[if lt IE 9]>
	<script src="http://css3-mediaqueries-js.googlecode.com/svn/trunk/css3-mediaqueries.js"></script><![endif]-->
	<link href="css/style.css" rel="stylesheet" type="text/css" media="all" />
	<link href='https://fonts.googleapis.com/css?family=Noto+Sans' rel='stylesheet' type='text/css'/>
	<link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro" rel="stylesheet"/>
		
	<script type="text/javascript" src="js/animation.js"></script>
	<script type="text/javascript" src="js/banner.js"></script>
	<!--end responsive web-->

    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-111498287-1"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-111498287-1');
    </script>

    <script type="text/javascript">
        function changebackground(id, img, backid) {
            var cCount = "";
            cCount = document.getElementsByName("backsmallimg").length;
            var chk;
            chk = document.getElementsByName("backsmallimg");
            for (i = 0; i < cCount; i++) {
                if (i == id) {
                    chk[i].style.border = "2px solid #553102";
                    document.getElementById("egiftImg").src = "http://myesalon.com/image/egiftbackground/" + img;
                }
                else {
                    chk[i].style.border = "2px solid #e5e5e5";
                }
            }
            document.getElementById("TextBox7").value = backid;
        }

        function countChars(textbox, max) {
            var count = max - document.getElementById(textbox).value.length;
            if (count < 0) {
                document.getElementById(textbox).value = document.getElementById(textbox).value.substring(0, max);
            }
        }

        function preview() {
            document.getElementById("from").innerHTML = "<strong>From:</strong> " + document.getElementById("TextBox3").value;
            document.getElementById("to").innerHTML = "<strong>To:</strong> " + document.getElementById("TextBox1").value;
            document.getElementById("amount").innerHTML = "<strong>Amount: $</strong> " + document.getElementById("TextBox2").value;
            document.getElementById("message").innerHTML = "<strong>Message:</strong> " + document.getElementById("TextArea1").value;
            document.location.href = "#preview";
        }
    </script>
    
    <script src="js/jquery.maskedinput.js" type="text/javascript"></script>
    <script type="text/javascript">
        //Check so phone
        jQuery(function ($) {
            $("#TextBox8").mask("999-999-9999");
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    	<a href="stay-connected" id="subbar"><img src="images/subbar.png" alt="Subscribe"/></a>

	<div>
	    <div class="grouplogo">
		    <div id="logo"><h1><a href="index.html"><img src="images/logo.png" alt="logo"/></a></h1></div>
            <div class="header_contact_w">
			    <div>2063 Central Plaza #111, New Braunfels TX 78130</div> 
			    <div><a href="tel:8303581580">Call Us: 830-358-1580</a></div>            
		    </div>
		    <div class="header_m">
			    <div class="groupmenu_m">
				    <ul>
					    <li id="call_m"><a href="tel:8303581580"><span>Call</span></a></li>                
					    <li id="email_m"><a href="contact"><span>Email</span></a></li>
					    <li id="direction_m"><a name="direction" onclick="myNavFunc();"><span>Direction</span></a></li>
					    <li id="subbar_m"><a href="stay-connected"><span>Stay Connected</span></a></li>
				    </ul>
                    <ul>
                        <li id="egift_m"><a href="egift"><span>E-Gift</span></a></li>  
                        <li id="promo_m"><a href="promotions"><span>Promotions</span></a></li>    
                    </ul>
			    </div>

		    </div>
	    </div>

        <div class="groupheader">
		    <div class="header">
			    <div id="menu">
				    <ul class="menu_m">
					    <div class="menuimg">Menu</div>  
					    <li><a href="index.html">Home</a></li>
						
					    <li class="service_menu"><a href="services">Services</a>
						    <ul>
							    <li><a href="services#blow-dry-bar">Blow Dry Bar</a></li>
                                <li><a href="services#solar-nails">Solar Nails</a></li>
                                <li><a href="services#spa-massage">Spa &amp; Massage</a></li>
							    <li><a href="services#pedicure-manicure">Pedicure / Manicure</a></li>
						    </ul>
					    </li>
                        <li><a href="our-pledge">Our Pledge</a></li>
					    <li><a href="gallery">Gallery</a></li>
					    <li><a href="egift" class="selected">E-Gift</a></li>
					    <li><a href="promotions">Promotions</a></li>                    
					    <li><a href="reviews">Reviews</a></li>
					    <li><a href="contact">Contact</a></li>
                        <li><a href="http://asiananailslounge.com/">Sister Salon</a></li>
				    </ul>
			    </div>
		    </div>
	    </div>
	    <div class="groupbanner2"></div>
	</div>	
		
	<div class="wrapper">
		<div class="about_title">E-Gift</div>
		<div class="content">
			Coming soon...

            <%--<%
                Dim sCart As String
                Try
                    sCart = Request.Cookies("giftcart")("mygift")
                Catch
                End Try
                Dim strCart As String
                If (sCart <> Nothing) Then
                    Dim cCart As String() = sCart.Split("#")
                    strCart += "<div id='productcart'>"
                    If (cCart.Length = 1) Then
                        strCart += "Your Cart: " + cCart.Length.ToString + " Item"
                    Else
                        strCart += "Your Cart: " + cCart.Length.ToString + " Items"
                    End If
                    strCart += "<div style='margin-top: 10px;'>"
                    strCart += "<a href='giftcart.aspx'><img src='images/view-cart.png' alt='view cart'/></a>"
                    strCart += "</div>"
                    strCart += "</div>"
                    strCart += "<div style='clear: both;'></div>"
                    Response.Write(strCart)
                End If
           
                 Dim sUserID As String = ConfigurationSettings.AppSettings("UserID")
                 Dim cnString As String = ConfigurationSettings.AppSettings("conn")
                 Dim cn As New SqlConnection
                    cn.ConnectionString = cnString
                %>

                    <div class="egiftrow" style="margin-bottom: 10px;">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    ForeColor="#ff0000"></asp:ValidationSummary>
                            </div>
                            <div id="egifttopnote">
                                Please choose a design and add input e-gift information:
                            </div>
                            <div class="egiftrow">
                                <%
                                    Dim sCount As String
                                    Dim sCurImg As String
                                    Dim sCurTextColor As String
                                    Dim cmString As String = "select top 1 nImage,nTextColor from tbEgiftBackGround where iUSID=0 and iIDBackground>=98 order by iIDBackGround ASC;"
                                    Dim cm As New SqlCommand(cmString, cn)
                                    Dim drd As SqlDataReader
                                    cn.Open()
                                    drd = cm.ExecuteReader
                                    While drd.Read
                                        sCurImg = drd("nImage").ToString
                                        sCurTextColor = drd("nTextColor").ToString
                                    End While
                                    drd.Close()
                                    cn.Close()
                                %>
                                    <a name="preview"></a> 
                                    <div class="card_m">
                                        <div style="text-align: center; background: #f1f1f1;">
                                                <div id="egiftsalonname" style="color: #000; font-size: 18pt;
                                                    font-family: 'Cantora One', sans-serif;">
                                                    <%=ConfigurationSettings.AppSettings("salonname")%></div>
                                                <div id="egifadd" style="color: #000; font-size: 10pt;">
                                                    <%=ConfigurationSettings.AppSettings("salonaddress")%></div>
                                                <div id="egiftphone" style="color: #000; font-size: 10pt;">
                                                    <%=ConfigurationSettings.AppSettings("salonphone")%></div>
                                                <div style="color: #000; font-size: 12pt; margin-top: 10px;">
                                                    <b>Serial Number: XXXXXX</b></div>
                                        </div>
                                        <div id="cardimg">
                                            <img id="egiftImg" src="http://myesalon.com/image/egiftbackground/<%=sCurImg%>" alt="egift"/>
                                        </div>
                                        <div id="egiftmessagegroup">
                                                <div style="min-height: 120px; padding: 2px;">
                                                    <div style="margin-bottom: 5px; clear: both;">
                                                        <div id="from"><strong>From</strong>:</div>
                                                    </div>
                                                    <div style="margin-bottom: 5px; clear: both;">
                                                        <div id="to"><strong>To</strong>:</div>
                                                    </div>
                                                    <div style="margin-bottom: 5px; clear: both;">
                                                        <div id="amount"><strong>Amount</strong>: $</div>
                                                    </div>
                                                    <div style="margin-bottom: 5px; clear: both;">
                                                        <div id="message"><strong>Message</strong>: </div>
                                                    </div>
                                                </div>
                                      </div>
                                </div>
                            </div>
                            <div id="egiftlibrary">
                                <%
                                    Dim sFirstID As String = "0"
                                    Dim str As String
                                    cmString = "select iIDBackGround,nTitle,nImage,nTextColor from tbEGiftBackGround where iUSID=0 and iIDBackground>=98 order by iIDBackGround ASC;"
                                    cm.CommandText = cmString
                                    cn.Open()
                                    drd = cm.ExecuteReader
                                    Dim iItem As Integer = 0
                                    While drd.Read
                                        If (iItem = 0) Then
                                                                  str += "<img style='border: 2px solid #553102;' src='http://myesalon.com/image/egiftbackground/N_" + drd("nImage").ToString + "' alt='" + drd("nTitle").ToString + "' name='backsmallimg' onclick='changebackground(" + iItem.ToString + ",""" + drd("nImage").ToString + """," + drd("iIDBackGround").ToString + ",""" + drd("nTextColor").ToString + """);'/>"
                                            sFirstID = drd("iIDBackGround").ToString
                                        Else
                                            str += "<img src='http://myesalon.com/image/egiftbackground/N_" + drd("nImage").ToString + "' alt='" + drd("nTitle").ToString + "' name='backsmallimg' onclick='changebackground(" + iItem.ToString + ",""" + drd("nImage").ToString + """," + drd("iIDBackGround").ToString + ",""" + drd("nTextColor").ToString + """);'/>"
                                        End If
                                        iItem = iItem + 1
                                    End While
                                    drd.Close()
                                    cn.Close()
                                    Response.Write(str)
                                %>
                            </div>
                            <div class="egiftrow" style="border-bottom: 1px dashed #e5e5e5;">
                            </div>
                            <h3>Information:</h3>
                            <div class="egiftrow">
                                Amount ($)*:<br /><asp:TextBox ID="TextBox2" runat="server" class="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox2"
                                        Display="Static" ErrorMessage="*Please input the amount" 
                                    ForeColor="#FF9933">*Please input the amount</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                    ControlToValidate="TextBox2" ErrorMessage="Invalid price" 
                                    ValidationExpression="\d+([-+.]\d+)?" ForeColor="#FF9933">*Invalid amount</asp:RegularExpressionValidator>
                            </div>
                            <div class="egiftrow">
                                Your name (First &amp; Last Name)*:<br /><asp:TextBox ID="TextBox3" runat="server" class="textbox"></asp:TextBox>
                                <asp:requiredfieldvalidator id="Requiredfieldvalidator1" Runat="server" 
                                    Display="Static" ErrorMessage="*Please enter your name."
					                        ControlToValidate="TextBox3" 
                                    ForeColor="#FF9933">*Please enter your name.</asp:requiredfieldvalidator>
                            </div>
                            <div class="egiftrow">
                                Your Email*:<br /><asp:TextBox ID="TextBox4" runat="server" class="textbox"></asp:TextBox>
                                <asp:requiredfieldvalidator id="Requiredfieldvalidator3" Runat="server" 
                                    Display="Static" ErrorMessage="*Please enter your email adress."
					                                ControlToValidate="TextBox4" 
                                    ForeColor="#FF9933">*Please enter your email adress.</asp:requiredfieldvalidator>
                                <asp:regularexpressionvalidator id="valRegular" Runat="server" 
                                    Display="dynamic" ErrorMessage="*Please enter a valid email adress."
					                                ControlToValidate="TextBox4" 
                                    ValidationExpression="^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$" 
                                    ForeColor="#FF9933">*Invalid Email Address.</asp:regularexpressionvalidator>
                            </div>
                            <div class="egiftrow">
                                Recipient (First &amp; Last Name):<br />
                                <asp:TextBox ID="TextBox1" runat="server" class="textbox"></asp:TextBox>
                            </div>
                            <div class="egiftrow">
                            Recipient's Email*:<br /><asp:TextBox ID="TextBox6" runat="server" class="textbox"></asp:TextBox>
                            <asp:requiredfieldvalidator id="Requiredfieldvalidator2" Runat="server" 
                                    Display="Static" ErrorMessage="*Please enter your email adress."
					                            ControlToValidate="TextBox6"
                                    ForeColor="#FF9933">*Please enter recipient's email adress.</asp:requiredfieldvalidator>
                                <asp:regularexpressionvalidator id="Regularexpressionvalidator1" Runat="server" 
                                    Display="dynamic" ErrorMessage="*Please enter a valid email adress."
					                            ControlToValidate="TextBox6" 
                                    ValidationExpression="^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$" 
                                    ForeColor="#FF9933">*Invalid Email Address.</asp:regularexpressionvalidator>
                            </div>
                            <div class="egiftrow">
                                Recipient (First &amp; Last Name):<br />
                                <asp:TextBox ID="TextBox8" runat="server" class="textbox"></asp:TextBox>
                            </div>
                            <div class="egiftrow">
                                    Message (160 characters):<br />
                                <textarea runat="server" id="TextArea1" class="textbox2"
                                    onkeyup="countChars('TextArea1',160);" onblur="countChars('TextArea1',160);"></textarea>
                            </div>
                            <div class="egiftrow" style="height: 250px;">
                                        Deliver this gift on:<br />
                                        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" 
                                                runat="server"></asp:ToolkitScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <ContentTemplate>
                                            <asp:CalendarExtender CssClass="calendar" ID="CalendarExtender1" TargetControlID="TextBox5" runat="server">
                                                                        </asp:CalendarExtender>
                                            <asp:TextBox ID="TextBox5" runat="server" class="textbox"></asp:TextBox>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                <div style="margin-top: 20px;">
                                    <input type="button" class="searchbutton" onclick ="preview();" value="Preview"/><br /><br />
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/paypal-checkout.png"></asp:ImageButton>
                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                        ImageUrl="~/images/add-cart2.png" style="margin-top: 10px;" Visible="False"></asp:ImageButton>
                                        <div style="display: none;">
                                                <asp:TextBox ID="TextBox7" runat="server" ForeColor="Black"></asp:TextBox>
                                        </div>
                                </div>
                                <div class="egiftrow" style="border-bottom: 1px dashed #e5e5e5;">
                                </div>
                                <div id="previewdiv" runat="server"></div>
                            </div>--%>



		</div>
	</div>
		
	<div class="groupfbye">
        <a href="https://www.facebook.com/Luxxnailbar.nb/" target="_blank"><img src="images/facebookicon.png" /></a>
        <div class="line"></div>
        <a href="https://www.yelp.com/biz/luxx-nail-bar-new-braunfels" target="_blank"> <img src="images/yelp_icon1.png" /></a>  
        <div class="line"></div>
        <a href="https://plus.google.com/102968780264322460398" target="_blank"><img src="images/gp_icon.png" /></a>
    </div>
		
	<div id="linktop"></div>
		
	<div class="footer">
		<div class="wrapper">
			<div class="aboutbox">
				<div class="about_title">CONTACT</div>
				<div class="about_txt footer_txt">
					2063 Central Plaza #111, New Braunfels TX 78130<br/>
					Tel:<a href="tel:8303581580"> 830-358-1580</a><br/><br/>
					<strong>Mon - Sat:</strong> 9:30 am - 7:30 pm<br/>
				    <strong>Sun:</strong> 11:00 am - 6:00 pm<br/>
				</div>
				<div class="social-icons">
				    <ul>
					    <li><a href="https://www.facebook.com/Luxxnailbar.nb/" target="_blank"><img src="images/facebookicon.png" alt=""/></a></li>
					    <li><a href="https://plus.google.com/102968780264322460398" target="_blank"><img src="images/gp_icon.png" alt=""/></a></li>
					    <li><a href="https://www.yelp.com/biz/luxx-nail-bar-new-braunfels" target="_blank"><img src="images/yelp_icon1.png" alt=""/></a></li>
				    </ul>
			    </div>
				<div class="copyright">Copyright &copy Luxx Nail Bar</div>
			</div>
		</div>
	</div>

    <%--<script type="text/javascript">
        document.getElementById("TextBox7").value = <%response.write(sFirstID)%>;
    </script>--%>

    </form>
</body>
</html>
