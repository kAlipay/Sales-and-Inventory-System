Imports System.Data.OleDb

Public Class frmuser
    Dim adding As Boolean
    Public uSearch As Boolean

    Private Sub AddUser()
        Try
            sqL = "INSERT INTO TblUser VALUES('" & txtfullname.Text & "','" & txtusername.Text & "','" & txtconfirmpassword.Text & "','" & cmbusertype.Text & "', '" & txtuserid.Text & "')"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            Dim i As Integer
            i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("User Added Successfully", MsgBoxStyle.Information, "Add User")
            Else
                MsgBox("Failed in Adding User", MsgBoxStyle.Exclamation, "Add User")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub DeleteUser()
        Try
            sqL = "DELETE FROM TblUser Where FullName = '" & ListBox1.SelectedItem & "'"
            ConnDB()
            cmd = New OledbCommand(sqL, conn)
            Dim i As Integer
            i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("User Deleted", MsgBoxStyle.Information, "Delete User")
            Else
                MsgBox("Failed in Deleting User")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub Loadusers()
        Try
            sqL = "SELECT FullName from TblUser order by FullName"
            ConnDB()
            cmd = New OledbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            ListBox1.Items.Clear()
            Do While dr.Read = True
                ListBox1.Items.Add(dr(0))
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        If ListBox1.SelectedItem = "" Then
            MsgBox("Please select username to delete", MsgBoxStyle.Exclamation, "Delete user")
        Else
            If MsgBox("Are you sure you want delete user?", MsgBoxStyle.YesNo, "Delete User") = MsgBoxResult.Yes Then
                DeleteUser()
                Loadusers()
            Else
                Exit Sub
            End If


        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        txtfullname.Enabled = True
        txtusername.Enabled = True
        txtconfirmpassword.Enabled = True
        txtpassword.Enabled = True
        txtuserid.Enabled = True
        cmbusertype.Enabled = True
        adding = True
        txtusername.Focus()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If txtpassword.Text <> txtconfirmpassword.Text Then
            MsgBox("Password Not Match", MsgBoxStyle.Exclamation, "Add user")
            txtpassword.SelectAll()
            txtconfirmpassword.SelectAll()
            txtpassword.Focus()
            Exit Sub

        End If
        If adding = True Then
            AddUser()
            Loadusers()
            txtfullname.Enabled = False
            txtusername.Enabled = False
            txtconfirmpassword.Enabled = False
            txtpassword.Enabled = False
            txtuserid.Enabled = False
            cmbusertype.Enabled = False
            txtconfirmpassword.Text = ""
            txtpassword.Text = ""
            txtusername.Text = ""
            txtuserid.Text = ""
            cmbusertype.Text = ""
            txtfullname.Text = ""
            adding = False
        Else
            MsgBox("Nothing to Save: Click [Add Button] to Add User", MsgBoxStyle.Information, "Add User")
        End If


    End Sub

    Private Sub frmUsers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtfullname.Enabled = False
        txtusername.Enabled = False
        txtconfirmpassword.Enabled = False
        txtpassword.Enabled = False
        txtuserid.Enabled = False
        cmbusertype.Enabled = False
        txtconfirmpassword.Text = ""
        txtpassword.Text = ""
        txtusername.Text = ""
        txtuserid.Text = ""
        cmbusertype.Text = ""
        txtfullname.Text = ""
        Loadusers()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        uSearch = True
        'frmLoadStaff.ShowDialog()
    End Sub
End Class