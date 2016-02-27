Imports System.Numerics
Imports System.Text
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button2.Enabled = False

    End Sub
    Private Sub ResetOldText()
        OpenFileDialog1.FileName = String.Empty
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.Title = "Find Scribblenauts.exe..."
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()
        If result = DialogResult.Cancel Then
            Exit Sub
        Else
            Button2.Enabled = True
        End If
    End Sub
    Public Function PatchScribblenautsEXE(ByVal path As String, Optional Unmasked As Boolean = False)
        'Try and Catch not used because the form should handle the errors, not the function.
        Dim addressUnlimited As Long = &H136BF1
        Dim addressUnmasked As Long = &H1BE691
        Dim binstream As IO.BinaryWriter = New IO.BinaryWriter(IO.File.Open(path, IO.FileMode.Open))
        If Unmasked = False Then
            binstream.BaseStream.Position = addressUnlimited
            binstream.Write(Encoding.ASCII.GetBytes("!"))
        Else
            binstream.BaseStream.Position = addressUnmasked
            binstream.Write(Encoding.ASCII.GetBytes("!"))
        End If
        binstream.Close()
    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            PatchScribblenautsEXE(OpenFileDialog1.FileName)
            MsgBox("Done!")
        Catch ex As Exception
            MsgBox(String.Concat("Error:  ", ex.Message))
            ResetOldText()
            Exit Sub
        End Try
        ResetOldText()
    End Sub
End Class
