Imports System.Net
Imports System.IO
Imports System.Web

Public Class SmsService

    Public Shared Function SendTokenSingle(TemplateKey As String, Destination As String, param1 As String, param2 As String, param3 As String) As String
        Dim ApiKey As String = "e883424d-d70f-4e58-8ee3-4e21ea390ff1"
        Dim sender As String = "30007546464646" ' اگر نیاز بود استفاده شود

        ' ساخت پارامترهای کوئری با UrlEncode
        Dim queryParams As String = String.Format("ApiKey={0}&TemplateKey={1}&Destination={2}&p1={3}&p2={4}&p3={5}",
                                                  HttpUtility.UrlEncode(ApiKey),
                                                  HttpUtility.UrlEncode(TemplateKey),
                                                  HttpUtility.UrlEncode(Destination),
                                                  HttpUtility.UrlEncode(param1),
                                                  HttpUtility.UrlEncode(param2),
                                                  HttpUtility.UrlEncode(param3))

        Dim url As String = "http://api.sms-webservice.com/api/V3/SendTokenSingle?" & queryParams

        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.Method = "GET"
        request.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate

        Dim responseText As String = ""

        Try
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using stream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(stream)
                        responseText = reader.ReadToEnd()
                    End Using
                End Using
            End Using
        Catch ex As WebException
            ' مدیریت خطا در صورت نیاز
            responseText = ex.Message
        End Try

        Return responseText
    End Function

End Class
