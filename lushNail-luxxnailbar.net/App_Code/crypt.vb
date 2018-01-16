Imports Microsoft.VisualBasic

Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Text
Imports System.Security.Cryptography
Imports System.Collections.Specialized

Imports System.Web.Mail

Public Class crypt

    Public Function psAddreplace(ByVal str) As String
        Dim strTemp
        Dim intPosition
        Dim i
        Dim strTCVN3 : strTCVN3 = "àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđÀÁẠẢÃÂẦẤẬẨẪĂẰẮẶẲẴÈÉẸẺẼÊỀẾỆỂỄÌÍỊỈĨÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠÙÚỤỦŨƯỪỨỰỬỮỲÝỴỶỸĐ "
        Dim strUnicode : strUnicode = "aaaaaaaaaaaaaaaaaeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyydAAAAAAAAAAAAAAAAAEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYD-"

        Dim ch, pos, ch1 As String
        If Len(str) = 0 Then
            psAddreplace = str
        End If
        strTemp = ""

        For i = 1 To Len(str)
            ch = Mid(str, i, 1)
            pos = InStr(strTCVN3, ch)
            If pos >= 1 Then
                ch1 = Mid(strUnicode, pos, 1)
                strTemp = strTemp & ch1
            Else
                strTemp = strTemp & ch
            End If
        Next
        psAddreplace = strTemp

        'Loc ky tu dac biet
        Dim illegalChars As Char() = "!@#$%^&*(){}[]""+<>?/\:;'~`|=,.-".ToCharArray()

        For Each ch2 As Char In illegalChars
            psAddreplace = psAddreplace.Replace(ch2, CChar("-"))
        Next

        psAddreplace = psAddreplace.Replace("xp_", "xp ")

        Return psAddreplace
    End Function

    Public Function psRemoveInject(ByVal str As String) As String
        'Loc ky tu dac biet
        Dim illegalChars As Char() = ";'-*".ToCharArray()

        For Each ch2 As Char In illegalChars
            str = str.Replace(ch2, CChar(" "))
        Next

        str = str.Replace("xp_", "xp ")

        Return str
    End Function

    Public Function fPrice(ByVal sPrice As String) As String
        Dim Str As String
        'Dim cGia As String() = sPrice.Split(".")
        'If (cGia.Length > 1) Then
        '    Str = FormatNumber(Integer.Parse(cGia(0)), 2).ToString + "." + cGia(1)
        'Else
        Str = FormatNumber(sPrice, 2).ToString
        'End If
        Return Str
    End Function
End Class
