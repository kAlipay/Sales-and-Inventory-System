Public Class frmlogin

    Private Sub btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogin.Click

        Dim dt As New DataTable
        connect()
        sqL = "Select * From TblUser where Username = '" & txtuname.Text & "' And Password = '" & txtpw.Text & "' "
        da = New OleDb.OleDbDataAdapter(sql, con)
        da.Fill(dt)
        If dt.Rows.Count = 0 Then
            MsgBox("Invalid Username or Password!", vbExclamation, "Login")
            con.Close()
        Else
            MsgBox("Welcome " & dt.Rows(0).ItemArray(0) & "!", vbInformation, "login")
            frmmain.statuser.Text = dt.Rows(0).ItemArray(1)
            frmPrintReceipt.lblEmpName.Text = dt.Rows(0).ItemArray(1)
            frmmain.statusertype.Text = dt.Rows(0).ItemArray(3)
            ' frmmain.txtpic.Text = dt.Rows(0).ItemArray(5)
            '   frmmain.pbximage.Image = System.Drawing.Bitmap.FromFile(frmmain.txtpic.Text)
            con.Close()
            frmmain.Show()
            ' Me.Close()
        End If
        If frmmain.statusertype.Text = "Admin" Then
            ' frmmain.ToolStripButton1.Visible = False
            ' frmmain.ToolStripButton3.Visible = False
        ElseIf frmmain.statusertype.Text = "User" Then
            frmmain.ToolStripButton1.Visible = False
        End If
        '   frmkeyboard.Close()

    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click

        Me.Close()

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        con.Close()
        Me.Close()

    End Sub

    Private Sub txtuname_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuname.Click
        '  frmkeyboard.Show()
        Label4.Text = "uname"
    End Sub

    Private Sub txtpw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpw.Click
        ' frmkeyboard.Show()
        Label4.Text = "pw"
    End Sub

    Private Sub frmlogin_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    End Sub
End Class
