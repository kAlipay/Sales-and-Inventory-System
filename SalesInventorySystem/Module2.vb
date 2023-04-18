
Imports System.Data.OleDb

Module Module2
    Public da As OleDb.OleDbDataAdapter
    Public cmd1 As New OleDbCommand
    Public con1 As New OleDb.OleDbConnection
    Public sql1 As String

    Public Sub connect()

        On Error GoTo err
        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Admin\Desktop\SalesInventorySystem\SalesInventorySystem\bin\Debug\SystemInventory.mdb"
        con.Open()
        Exit Sub
err:
        MsgBox("Database connection failed!", vbExclamation, "Error")
    End Sub

End Module