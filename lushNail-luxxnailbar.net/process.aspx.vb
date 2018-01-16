Imports System.Data.SqlClient
Imports System.Data

Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Collections.Generic
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization

Partial Class process
    Inherits System.Web.UI.Page
    Public Shared mailservice As New com.myesalon.mailservice.mailservice

    <WebMethod()> _
    Public Shared Function optemail(ByVal sFirstName As String, ByVal sLastName As String, ByVal sEmail As String) As String
        Return sAddEmail(sFirstName, sLastName, sEmail)
    End Function

    Public Shared Function sAddEmail(ByVal sFirstName As String, ByVal sLastName As String, ByVal sEmail As String) As String
        Dim sRe As String = ""
        Dim cnString As String
        Dim cn As New SqlConnection
        cnString = "Data Source=myesalon.com,1433;Initial Catalog=myesalon;User ID=myesalonaccess;Password=rOOTSalon83979;"
        cn.ConnectionString = cnString
        Dim cmString As String = ""
        cmString += "declare @iCo int;"
        cmString += "set @iCo=0;"
        cmString += "set @iCo=(select count(*) from tbEmailCoupon where iUSID=1302 and nEmail=@nEmail);"
        cmString += " if(@iCo=0) "
        cmString += " Begin "
        cmString += "insert into tbEmailCoupon(nFirstName,nLastName,nEmail,dDate,iUnsubscribe,iUSID) values(@nFirstName,@nLastName,@nEmail,getDate(),0,1302);"
        cmString += " End "
        cmString += "select @iCo as iCo;"
        Dim cm As New SqlCommand(cmString, cn)
        cm.Parameters.Add(New SqlParameter("@nFirstName", SqlDbType.NVarChar, 50)).Value = sFirstName
        cm.Parameters.Add(New SqlParameter("@nLastName", SqlDbType.NVarChar, 50)).Value = sLastName
        cm.Parameters.Add(New SqlParameter("@nEmail", SqlDbType.NVarChar, 100)).Value = sEmail
        Dim drd As SqlDataReader
        cn.Open()
        drd = cm.ExecuteReader
        While drd.Read
            sRe = drd("iCo").ToString
        End While
        cn.Close()
        Return sRe
    End Function
    <WebMethod()> _
    Public Shared Function sSign(ByVal sFirstName As String, ByVal sLastName As String, ByVal sEmail As String, ByVal sPhone As String, ByVal sBirthDate As String) As String
        Dim sRe As String = ""
        Dim cnString As String
        Dim cn As New SqlConnection
        cnString = "Data Source=myesalon.com,1433;Initial Catalog=myesalon;User ID=myesalonaccess;Password=rOOTSalon83979;"
        cn.ConnectionString = cnString
        Dim cmString As String = ""
        cmString += "declare @iCo int;"
        cmString += "set @iCo=0;"
        cmString += "set @iCo=(select count(*) from tbEmailCoupon where iUSID=1302 and nEmail=@nEmail);"
        cmString += " if(@iCo=0) "
        cmString += " Begin "
        If (sBirthDate <> Nothing) Then
            cmString += "insert into tbEmailCoupon(nFirstName,nLastName,nEmail,nPhone,dBirthDay,dDate,iUnsubscribe,iUSID) values(@nFirstName,@nLastName,@nEmail,@nPhone,@dBirthDay,getDate(),0,1302);"
        Else
            cmString += "insert into tbEmailCoupon(nFirstName,nLastName,nEmail,nPhone,dDate,iUnsubscribe,iUSID) values(@nFirstName,@nLastName,@nEmail,@nPhone,getDate(),0,1302);"
        End If
        cmString += " End "
        cmString += "select @iCo as iCo;"
        Dim cm As New SqlCommand(cmString, cn)
        cm.Parameters.Add(New SqlParameter("@nFirstName", SqlDbType.NVarChar, 50)).Value = sFirstName
        cm.Parameters.Add(New SqlParameter("@nLastName", SqlDbType.NVarChar, 50)).Value = sLastName
        cm.Parameters.Add(New SqlParameter("@nEmail", SqlDbType.NVarChar, 100)).Value = sEmail
        cm.Parameters.Add(New SqlParameter("@nPhone", SqlDbType.NVarChar, 15)).Value = sPhone
        If (sBirthDate <> Nothing) Then
            cm.Parameters.Add(New SqlParameter("@dBirthDay", SqlDbType.DateTime)).Value = "2000-" + sBirthDate
        End If
        Dim drd As SqlDataReader
        cn.Open()
        drd = cm.ExecuteReader
        While drd.Read
            sRe = drd("iCo").ToString
        End While
        cn.Close()
        Return sRe
    End Function



    <WebMethod()> _
    Public Shared Function contactus(ByVal sName As String, ByVal sEmail As String, ByVal sPhone As String, ByVal sContent As String) As String
        Dim sRe As String = "0"

        Dim sBody As String = ""
        sBody += "Hi Owner,<br><br>"
        sBody += "You have a new message from customer on website: " + ConfigurationSettings.AppSettings("domain") + "<br><br>"
        sBody += "+ Name: " + sName + "<br>"
        sBody += "+ Email: " + sEmail + "<br>"
        sBody += "+ Phone Number: " + sPhone + "<br><br>"
        sBody += "Content: " + sContent + "<br><br><br>"
        sBody += ("Best regards,") + "<br>"
        sBody += ConfigurationSettings.AppSettings("domain")

        mailservice.fSendSMTP(ConfigurationSettings.AppSettings("salonname"), ConfigurationSettings.AppSettings("email"), ConfigurationSettings.AppSettings("email"), "New message from customer", sBody)

        Return sRe
    End Function
End Class
