Imports System.Net.Http
Imports System.Web

Module SmsApi

    Async Function Send(recipients As String, text As String) As Threading.Tasks.Task(Of String)
        Dim ApiKey As String = "e883424d-d70f-4e58-8ee3-4e21ea390ff1"
        Dim sender As String = "30007546464646"

        ' ساخت پارامترهای کوئری استرینگ
        Dim query As String = String.Format("ApiKey={0}&Text={1}&Sender={2}&Recipients={3}",
                                            HttpUtility.UrlEncode(ApiKey),
                                            HttpUtility.UrlEncode(text),
                                            HttpUtility.UrlEncode(sender),
                                            HttpUtility.UrlEncode(recipients))

        Dim url As String = "http://api.sms-webservice.com/api/V3/Send?" & query

        Using client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync(url)
            response.EnsureSuccessStatusCode()
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()
            Return responseBody
        End Using
    End Function

End Module
