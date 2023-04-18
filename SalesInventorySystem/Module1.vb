Imports System.Data.OleDb
Module Module1
    Public con As New System.Data.OleDb.OleDbConnection("Provider = Microsoft.jet.OleDB.4.0;Data Source =  " & Application.StartupPath & "\InventorySystem.mdb;")




    Public sqL As String
    Public cmd As OleDbCommand
    Public dr As OleDbDataReader

    Public conn As OleDbConnection
    Public connStr As String = System.Environment.CurrentDirectory.ToString & "\InventorySystem.mdb"

    Public Sub ConnDB()
        Try
            conn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & connStr & "")
            conn.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Module
