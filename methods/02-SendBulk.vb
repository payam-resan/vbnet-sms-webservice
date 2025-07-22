Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Web.Script.Serialization

Public Class SmsService

    Public Class Recipient
        Public Property Destination As String
        Public Property UserTraceId As String
    End Class

    Public Shared Function SendBulk(Destination As String, UserTraceId As String, Text As String) As String
        Dim ApiKey As String = "e883424d-d70f-4e58-8ee3-4e21ea390ff1"
        Dim Sender As String = "30007546464646"

        Dim recipient As New Recipient With {
            .Destination = Destination,
            .UserTraceId = UserTraceId
        }
        Dim recipients As Recipient() = {recipient}

        Dim data As New Dictionary(Of String, Object) From {
            {"ApiKey", ApiKey},
            {"Text", Text},
            {"Sender", Sender},
            {"Recipients", recipients}
        }

        Dim serializer As New JavaScriptSerializer()
        Dim jsonData As String = serializer.Serialize(data)

        Dim url As String = "http://api.sms-webservice.com/api/V3/SendBulk"
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json"

        Using streamWriter As New StreamWriter(request.GetRequestStream())
            streamWriter.Write(jsonData)
            streamWriter.Flush()
            streamWriter.Close()
        End Using

        Dim responseText As String = ""
        Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using streamReader As New StreamReader(response.GetResponseStream())
                responseText = streamReader.ReadToEnd()
            End Using
        End Using

        Return responseText
    End Function

End Class
