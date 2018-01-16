Imports System.Net
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.Mail
Imports System.Threading

Partial Class ipn_giftipn
    Inherits System.Web.UI.Page

    Dim cn As New SqlConnection
    Public crypt As New crypt
    Dim myesalonservices As New myesalonservices.myesalonservices
    Dim threadSendmail As Thread
    Dim mailservice As New com.myesalon.mailservice.mailservice
    Dim smsservices As New smsservices.WebService


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Connection
        Dim cnString As String
        cnString = ConfigurationSettings.AppSettings("conn")
        cn.ConnectionString = cnString
        threadSendmail = New Thread(AddressOf sSendmailSeller)

        'Post back to either sandbox or live
        Dim strSandbox As String = "https://www.sandbox.paypal.com/cgi-bin/webscr"
        Dim strLive As String = "https://www.paypal.com/cgi-bin/webscr"
        Dim req As HttpWebRequest = CType(WebRequest.Create(strSandbox), HttpWebRequest)

        'Set values for the request back
        req.Method = "POST"
        req.ContentType = "application/x-www-form-urlencoded"
        Dim Param() As Byte = Request.BinaryRead(HttpContext.Current.Request.ContentLength)
        Dim strRequest As String = Encoding.ASCII.GetString(Param)
        strRequest = strRequest + "&cmd=_notify-validate"
        req.ContentLength = strRequest.Length

        'for proxy
        'Dim proxy As New WebProxy(New System.Uri("http://url:port#"))
        'req.Proxy = proxy

        'Send the request to PayPal and get the response
        Dim streamOut As StreamWriter = New StreamWriter(req.GetRequestStream(), Encoding.ASCII)
        streamOut.Write(strRequest)
        streamOut.Close()
        Dim streamIn As StreamReader = New StreamReader(req.GetResponse().GetResponseStream())
        Dim strResponse As String = streamIn.ReadToEnd()
        streamIn.Close()

        If strResponse = "VERIFIED" Then
            'check the payment_status is Completed
            'check that txn_id has not been previously processed
            'check that receiver_email is your Primary PayPal email
            'check that payment_amount/payment_currency are correct
            'process payment
            If (Request("payment_status") = "Completed") Then
                sVerified(Request("custom"), Request("txn_id"))
            ElseIf (Request("payment_status") = "Pending") Then
                'Pyament pending
                'Code for pending case
            Else
                'Code for other case
            End If
        ElseIf strResponse = "INVALID" Then
            'log for manual investigation
        Else
            'Response wasn't VERIFIED or INVALID, log for manual investigation
        End If
    End Sub

    Private Sub sVerified(ByVal sCustom As String, ByVal sTnx As String)
        Dim cmString As String
        cmString += " Declare @iCo int; "
        cmString += " set @iCo=(select count(*) from tbEGiftGroup where nTnx is null and iIDGiftGroup=@iIDGiftGroup); "
        cmString += " if @iCo>0 "
        cmString += " begin "
        cmString += " update tbEgiftGroup set nTnx=@nTnx where iIDGiftGroup=@iIDGiftGroup; "
        cmString += " update tbEgift set iPaid=1 where iIDGiftGroup=@iIDGiftGroup; "
        cmString += " end "
        cmString += " select @iCo as iCo; "
        Dim cm As New SqlCommand(cmString, cn)
        cm.Parameters.Add(New SqlParameter("@nTnx", SqlDbType.NVarChar, 150)).Value = sTnx
        cm.Parameters.Add(New SqlParameter("@iIDGiftGroup", SqlDbType.Int)).Value = sCustom
        Dim drd As SqlDataReader

        Dim sCo As String

        cn.Open()
        drd = cm.ExecuteReader
        While drd.Read
            sCo = drd("iCo").ToString
        End While
        drd.Close()
        cn.Close()

        If (Integer.Parse(sCo) > 0) Then
            'Send notify e-mail to seller
            threadSendmail.Start()

            'Send gift if immediately
            Dim sGiftString As String
            Dim cGiftString As String()
            cmString = "select iIDEGift from tbEGift where iIDGiftGroup=@iIDGiftGroup and iSent=0 and CONVERT(VARCHAR(10),dDeliverDate,111)=CONVERT(VARCHAR(10),GETDATE(),111);"
            cm.CommandText = cmString

            cn.Open()
            drd = cm.ExecuteReader
            While drd.Read
                If (sGiftString = Nothing) Then
                    sGiftString = drd("iIDEGift").ToString
                Else
                    sGiftString += "," + drd("iIDEGift").ToString
                End If
            End While
            drd.Close()
            cn.Close()
            cGiftString = sGiftString.Split(",")
            Dim i As Integer = 0
            While i < cGiftString.Length
                sSendmailGift(cGiftString(i))
                i = i + 1
            End While
        End If
    End Sub

    Private Sub sSendmailSeller()
        'Lay email cua Seller
        Dim sEmail As String = ConfigurationSettings.AppSettings("email")
        Dim sUserID As String = ConfigurationSettings.AppSettings("UserID")

        Dim mailMessage As New MailMessage
        Dim sBody As String

        sBody += "<a href='http://myesalon.com'><img style='border: none' alt='myesalon' src='http://myesalon.com/gui/logo.png'/></a><br><br>"
        sBody += "Hi " + sUserID + ",<br><br>"
        sBody += "You have a new e-gift order on " + ConfigurationSettings.AppSettings("domain") + ".<br><br>"
        sBody += "Buyer has paid by Paypal, you can login to your Paypal account to check.<br><br>"
        sBody += "You can see the detail of new order here: <a href='http://myesalon.com/myegift.aspx'>http://myesalon.com/myegift.aspx</a><br><br>"
        sBody += "Best regards<br><br>"
        sBody += "Myesalon.com Staff"

        'creating an instance of the MailMessage class
        mailMessage.From = """myesalon.com"" <contact@myesalon.com>"
        'senders email address
        mailMessage.To = sEmail
        'email address of the Bcc recipient 
        mailMessage.Subject = "You have a new e-gift order"
        'subject of the email message
        mailMessage.BodyFormat = MailFormat.Html
        'message text format. Can be text or html
        mailMessage.Body = sBody
        'message body
        mailMessage.Priority = MailPriority.High
        'email priority. Can be low, normal or high

        ''''''bao mat

        'mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2") 'basic authentication
        'mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout", "60")
        'mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1") 'basic authentication
        'mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", ConfigurationSettings.AppSettings("MailUsername")) 'set your username here
        'mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", ConfigurationSettings.AppSettings("MailPassword")) 'set your password here
        'mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "25")
        ' '''''''''''''''''


        'SmtpMail.SmtpServer = ConfigurationSettings.AppSettings("MailSerIP")
        ''mail server used to send this email. modify this line based on your mail server
        'SmtpMail.Send(mailMessage)

        mailservice.fSendMailGun("""myesalon.com"" <contact@myesalon.com>", sEmail, "You have a new e-gift order", sBody, "myesalon.com")
    End Sub

    Private Sub sSendmailGift(ByVal sIDEgift As String)
        'Lay email cua salon
        Dim sEmail As String = ConfigurationSettings.AppSettings("email")
        Dim sSalonName As String = ConfigurationSettings.AppSettings("salonname")
        Dim sAddress As String = ConfigurationSettings.AppSettings("salonaddress")
        Dim sPhone As String = ConfigurationSettings.AppSettings("salonphone")
        Dim sDomain As String = ConfigurationSettings.AppSettings("domain")

        'Get recipient info
        Dim sSenderName, sSenderEmail, sAmount, sReceiverName, sReceiverEmail, sMessage, sImage, sEgiftID, sTextColor, sReceiverPhone As String
        Dim cmString As String = "select e.nSenderName,e.nSenderEmail,e.iIDEGift,e.fAmount,e.nReceiverName,e.nReceiverEmail,e.nMessage,b.nImage,b.nTextColor,e.nReceiverPhone from tbEGiftGroup g inner join tbEgift e on g.iIDGiftGroup=e.iIDGiftGroup and e.iIDEGift=@iIDEGift inner join tbEGiftBackGround b on e.iIDBackGround=b.iIDBackGround;"
        Dim cm As New SqlCommand(cmString, cn)
        cm.Parameters.Add(New SqlParameter("@iIDeGift", SqlDbType.Int)).Value = sIDEgift
        Dim drd As SqlDataReader
        cn.Open()
        drd = cm.ExecuteReader
        While drd.Read
            sSenderName = drd("nSenderName").ToString
            sSenderEmail = drd("nSenderEmail").ToString
            sAmount = drd("fAmount").ToString
            sReceiverName = drd("nReceiverName").ToString
            sReceiverEmail = drd("nReceiverEmail").ToString
            sMessage = drd("nMessage").ToString
            sImage = drd("nImage").ToString
            sEgiftID = drd("iIDEgift")
            sTextColor = drd("nTextColor").ToString
            sReceiverPhone = drd("nReceiverPhone").ToString
        End While
        drd.Close()
        cn.Close()

        If (sReceiverName <> Nothing) Then
            Dim mailMessage As New MailMessage
            Dim sBody As String

            sBody += "Hi " + sReceiverName + "<br><br>"
            sBody += sSenderName + " has sent you a E-Gift for the services at " + sSalonName + "<br><br>"


            sBody += "<div style='max-width: 650px; border: 1px dashed #ccc; background: #f1f1f1;width: 98%;'>"
            sBody += "<div style='text-align: center; background: #f1f1f1;'>"
            sBody += "<div style='color: #000; font-size: 18pt;'>" + sSalonName + "</div>"
            sBody += "<div style='color: #000; font-size: 10pt;'>" + sDomain + "</div>"
            'sBody += "<div style='color: #000; font-size: 10pt;'>" + sPhone + "</div>"
            sBody += "<div style='color: #000; font-size: 12pt; margin-top: 10px;'><strong>Serial Number: " + sEgiftID + "</strong></div>"
            sBody += "</div>"
            sBody += "<div>"
            sBody += "<img src='http://myesalon.com/image/egiftbackground/" + sImage + "' alt='egift' style='width: 100%;'/>"
            sBody += "</div>"
            sBody += "<div style='font-family: Arial; font-size: 10pt; line-height: 140%; overflow: hidden; background: #f1f1f1; color: #000;'>"
            sBody += "<div style='min-height: 120px; padding: 2px;'>"
            sBody += "<div style='margin-bottom: 5px; clear: both;'>"
            sBody += "<div><strong>From</strong>:" + sSenderName + "</div>"
            sBody += "</div>"
            sBody += "<div style='margin-bottom: 5px; clear: both;'>"
            sBody += "<div><strong>To</strong>:" + sReceiverName + "</div>"
            sBody += "</div>"
            sBody += "<div style='margin-bottom: 5px; clear: both;'>"
            sBody += "<div><strong>Amount</strong>: $" + crypt.fPrice(sAmount) + "</div>"
            sBody += "</div>"
            sBody += "<div style='margin-bottom: 5px; clear: both;'>"
            sBody += "<div><strong>Message</strong>: " + sMessage + "</div>"
            sBody += "</div>"
            sBody += "</div>"
            sBody += "</div>"
            sBody += "</div>"

            sBody += "</br></br>"
            sBody += "<a href='http://myesalon.com/egiftprint.aspx?id=" + myesalonservices.encrypt(ConfigurationSettings.AppSettings("webservicepass"), sEgiftID) + "' target='_blank' style='font-size: 12pt; font-weight: bold;'>Print this E-Gift!</a><br><br>"
            sBody += "*You can print this e-gift and bring to " + sSalonName + " and just mention to the front desk the serial #: " + sEgiftID + ".<br><br>"
            sBody += "Best Regards<br><br>"
            sBody += sSalonName + "<br><br>"
            Dim sVerifybody As String = ""
            sVerifybody += "<img src='http://myesalon.com/ipn/egiftreceived.aspx?id=" + sIDEgift + "' style='width: 1px; height: 1px;'/>"


            'Send to egift receiver
            mailservice.fSendMailGun(sSalonName + " <" + sEmail + ">", sReceiverEmail, "An E-Gift For You From " + sSenderName, sBody + sVerifybody, sDomain)



            'Send copy to egift buyer
            sBody = "Hi, Your E-Gift has been sent to the recipient. If for some reason your recipient did not receive the E-Gift, you can forward this email to them. Thank You!<br><br><br>" + sBody
            mailservice.fSendMailGun(sSalonName + " <" + sEmail + ">", sSenderEmail, "A Copy of Your E-Gift Purchase", sBody, sDomain)

            'Update sent
            cmString = "update tbEgift set iSent=1 where iIDEGift=" + sIDEgift + ";"
            cm.CommandText = cmString
            cn.Open()
            cm.ExecuteNonQuery()
            cn.Close()

            'Send Text Message if phone is not null
            If (sReceiverPhone <> "") Then
                Dim sTextMessage As String = ""
                sTextMessage = "Hi " + sReceiverName + ", " + sSenderName + " has sent you an E-Gift for the services at " + sSalonName + ". You can bring this E-gift to " + sSalonName + " and just mention to the front desk the serial #: " + sEgiftID + ". Please open the following url to see your E-Gift: https://myesalon.com/egiftprint.aspx?id=" + myesalonservices.encrypt("bigmyesalon79", sEgiftID)
                smsservices.sendSMS("bigmyesalon79", sReceiverPhone, sTextMessage)
            End If
        End If
    End Sub

End Class
