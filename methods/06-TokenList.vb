Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Web.Script.Serialization

Public Class SmsService

    Public Shared Function TokenList() As String
        Dim ApiKey As String = "e883424d-d70f-4e58-8ee3-4e21ea390ff1"
        Dim url As String = "http://api.sms-webservice.com/api/V3/TokenList"

        Dim data = New With {
            .ApiKey = ApiKey
        }

        Dim serializer As New JavaScriptSerializer()
        Dim jsonData As String = serializer.Serialize(data)

        Dim responseText As String = ""

        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json"
        Dim dataBytes As Byte() = Encoding.UTF8.GetBytes(jsonData)
        request.ContentLength = dataBytes.Length

        Try
            Using requestStream As Stream = request.GetRequestStream()
                requestStream.Write(dataBytes, 0, dataBytes.Length)
            End Using

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using stream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(stream)
                        responseText = reader.ReadToEnd()
                    End Using
                End Using
            End Using

        Catch ex As WebException
            ' مدیریت خطا
            Using stream As Stream = ex.Response?.GetResponseStream()
                If stream IsNot Nothing Then
                    Using reader As New StreamReader(stream)
                        responseText = reader.ReadToEnd()
                    End Using
                Else
                    responseText = ex.Message
                End If
            End Using
        End Try

        Return responseText
    End Function

End Class
