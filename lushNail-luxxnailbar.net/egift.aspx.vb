Imports System.Data.SqlClient
Imports System.Data

Partial Class egift
    Inherits System.Web.UI.Page
    Dim cnString As String
    Dim cn As New SqlConnection
    Public crypt As New crypt

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    cnString = ConfigurationSettings.AppSettings("conn")
    '    cn.ConnectionString = cnString
    '    If (Not IsPostBack = True) Then
    '        TextBox5.Text = DateTime.Now.ToString("MM/dd/yyyy")
    '    End If
    'End Sub

    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
    '    sAddtocart(1)
    'End Sub

    'Private Sub sAddtocart(ByVal iType As Integer)
    '    'iType=1: add to cart and view cart , iType=2: add to cart and pay now
    '    Dim sCart As String

    '    Dim sBackID As String
    '    sBackID = TextBox7.Text

    '    Dim sCurCart As String
    '    Try
    '        sCurCart = Request.Cookies("giftcart")("mygift")
    '    Catch
    '    End Try
    '    Dim sPrint As String
    '    sPrint = "0"
    '    'If (sCurCart = Nothing) Then
    '    sCart = sBackID + "|" + TextBox2.Text + "|" + TextBox3.Text + "|" + TextBox4.Text + "|" + TextBox1.Text + "|" + TextBox6.Text + "|" + TextArea1.Value.Replace("|", "").Replace("#", "") + "|" + sPrint + "|" + TextBox5.Text + "|" + TextBox8.Text
    '    'Else
    '    'sCart = sCurCart + "#" + sBackID + "|" + TextBox2.Text + "|" + TextBox3.Text + "|" + TextBox4.Text + "|" + TextBox1.Text + "|" + TextBox6.Text + "|" + TextArea1.Value.Replace("|", "").Replace("#", "") + "|" + sPrint + "|" + TextBox5.Text
    '    'End If
    '    sAddCartCook(sCart)
    '    If (iType = 1) Then
    '        Response.Redirect("egift.aspx")
    '    ElseIf (iType = 2) Then
    '        sCreateOrder(sCart)
    '    End If
    'End Sub

    'Private Sub sAddCartCook(ByVal sContent As String)
    '    Dim newCookie As HttpCookie = New HttpCookie("giftcart")
    '    'newCookie.Domain = ConfigurationSettings.AppSettings("domain")
    '    newCookie.Values.Add("mygift", sContent)
    '    newCookie.Expires = DateTime.Now.AddHours(24)
    '    Response.Cookies.Add(newCookie)
    'End Sub

    'Private Sub sCreateOrder(ByVal sCart As String)
    '    If (sCart <> Nothing) Then
    '        Dim cCart As String()
    '        Dim sQuery As String
    '        Dim i As Integer = 0
    '        cCart = sCart.Split("#")


    '        While i < cCart.Length
    '            Dim cProInfo As String() = cCart(i).Split("|")
    '            If (i = 0) Then
    '                sQuery += "insert into tbEGiftGroup(iUSID,dDate) values (@iUSID,getDate());"
    '                sQuery += "Declare @iIDGiftGroup int;"
    '                sQuery += "set @iIDGiftGroup=(SELECT SCOPE_IDENTITY());"
    '            End If
    '            sQuery += "insert into tbEGift(iIDGiftGroup,iUSID,iIDBackGround,fAmount,nSenderName,nSenderEmail,nReceiverName,nReceiverEmail,nMessage,iPrint,dDeliverDate,iPaid,iReceived,iSent,iCompleted,iNew,nReceiverPhone) "
    '            sQuery += "select @iIDGiftGroup,@iUSID," + crypt.psRemoveInject(cProInfo(0)) + "," + crypt.psRemoveInject(cProInfo(1)) + ",N'" + crypt.psRemoveInject(cProInfo(2)) + "',N'" + crypt.psRemoveInject(cProInfo(3)) + "',N'" + crypt.psRemoveInject(cProInfo(4)) + "',N'" + crypt.psRemoveInject(cProInfo(5)) + "',N'" + crypt.psRemoveInject(cProInfo(6)) + "'," + crypt.psRemoveInject(cProInfo(7)) + ",'" + crypt.psRemoveInject(cProInfo(8)) + "',0,0,0,0,1,'" + crypt.psRemoveInject(cProInfo(9)) + "';"
    '            i = i + 1
    '        End While
    '        sQuery += "select @iIDGiftGroup as iIDGiftGroup;"
    '        Dim cmString As String = sQuery
    '        Dim cm As New SqlCommand(cmString, cn)
    '        cm.Parameters.Add(New SqlParameter("@iUSID", SqlDbType.Int)).Value = ConfigurationSettings.AppSettings("USID")
    '        Dim drd As SqlDataReader
    '        Dim sIDGroup As String
    '        cn.Open()
    '        drd = cm.ExecuteReader
    '        While drd.Read
    '            sIDGroup = drd("iIDGiftGroup").ToString
    '        End While
    '        drd.Close()
    '        cn.Close()

    '        'Clear Cookies    
    '        Dim myCookie As HttpCookie
    '        myCookie = New HttpCookie("giftcart")
    '        myCookie.Expires = DateTime.Now.AddDays(-1D)
    '        Response.Cookies.Add(myCookie)

    '        'Redirect to payment page
    '        Session("giftpayid") = sIDGroup
    '        Session("giftamount") = TextBox2.Text
    '        Response.Redirect("ipn/giftpay.aspx")

    '        'Dim script As String = "window.top.location.href = 'ipn/giftpay.aspx';"
    '        'If Not Page.ClientScript.IsStartupScriptRegistered(Me.GetType(), "alertscript") Then
    '        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "openscript", script, True)
    '        'End If
    '    End If
    'End Sub

    'Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
    '    sAddtocart(2)
    'End Sub
End Class
