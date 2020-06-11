Imports System.IO
Imports System.Collections.Concurrent

Class BasicFileLogger
    Private _tWriter As TextWriter
    Private _queue As BlockingCollection(Of String)
    Private _backgroundTask As Task

    Public Sub New(Path As String)
        Me._tWriter = New StreamWriter(Path, True, Text.Encoding.UTF8)
        Me._queue = New BlockingCollection(Of String)(1024)
        Me._backgroundTask = Task.Factory.StartNew(Sub()
                                                       For Each message In _queue.GetConsumingEnumerable()
                                                           WriteMessageToFile(message, _queue.Count = 0)
                                                       Next
                                                   End Sub, TaskCreationOptions.LongRunning)
    End Sub

    Private Sub WriteMessageToFile(Message As String, flush As Boolean)
        _tWriter.WriteLine(Message)
        If flush Then _tWriter.Flush()
    End Sub


    Public Sub WriteLog(LogString As String)
        If Not _queue.IsAddingCompleted Then
            Try
                _queue.Add(LogString)
                Return
            Catch ex As InvalidOperationException
            End Try
        End If
    End Sub

    Public Sub Dispose()
        _queue.CompleteAdding()

        Try
            _backgroundTask.Wait(1500)
        Catch ex As TaskCanceledException ' This exception is expected, so we want to catch it
        Catch ex As AggregateException When ex.InnerExceptions.Count = 1 AndAlso TypeOf ex.InnerExceptions(0) Is TaskCanceledException
        End Try

        _tWriter.Close()
    End Sub
End Class
