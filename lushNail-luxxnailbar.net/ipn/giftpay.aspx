<%@ Page Language="VB" AutoEventWireup="false" CodeFile="giftpay.aspx.vb" Inherits="ipn_giftpay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="Form1" runat="server" action="https://www.sandbox.paypal.com/cgi-bin/webscr" method="post">
    <!--https://www.sandbox.paypal.com/cgi-bin/webscr (sandbox link)-->
    
    <div>
    Please wait...
            <%
                Dim sAmount As String
                Dim sID As String
                Try
                    sAmount = Session("giftamount")
                    sID = Session("giftpayid")
                Catch
                End Try
                If (sAmount <> Nothing) Then
                    Session.Remove("giftamount")
                    Session.Remove("giftpayid")
                End If
            %>
            <input type="hidden" name="cmd" value="_xclick"/>
            <input type="hidden" name="business" value="<%=ConfigurationSettings.AppSettings("paypalemail")%>"/>
            <input type="hidden" name="item_name" value="<%=ConfigurationSettings.AppSettings("pay_gift_title")%>"/>
            <input type="hidden" name="amount" value="<%response.write(sAmount)%>"/>
            <input type="hidden" name="no_shipping" value="1"/>
            <input type="hidden" name="return" value="<%=ConfigurationSettings.AppSettings("paygiftreturn")%>"/>
            <input type="hidden" name="rm" value="1">
            <input type="hidden" name="notify_url" value="<%=ConfigurationSettings.AppSettings("gift_ipn_url")%>"/>
            <input type="hidden" name="cancel_return" value="<%=ConfigurationSettings.AppSettings("paygiftcancel")%>"/>
            <input type="hidden" name="currency_code" value="USD"/>
            <input type="hidden" name="custom" value="<%response.write(sID)%>"/>
    </div>
    </form>
    
    <script type="text/javascript">
        document.forms["Form1"].submit();
    </script>
</html>
