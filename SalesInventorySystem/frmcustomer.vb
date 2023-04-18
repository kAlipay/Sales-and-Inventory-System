Imports System.IO
Imports System.Byte
Imports System.Drawing
'Imports System.IO.MemoryMappedFiles
Imports System.IO.StreamReader

Imports System.Data.OleDb


Public Class frmcustomer
    Dim imgName As String
    Dim daImage As OleDbDataAdapter
    Dim dsImage As DataSet
    Dim cn As New OleDbConnection
    Dim cmd As New OleDbCommand
    Dim dr As OleDbDataReader
    Dim db As String = "InventorySystem.mdb"
    Dim cnstring As String = "PRovider=Microsoft.ace.oledb.12.0;data source=" & db & ""

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim dlgImage As FileDialog = New OpenFileDialog()

            dlgImage.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif"

            If dlgImage.ShowDialog() = DialogResult.OK Then
                imgName = dlgImage.FileName

                Dim newimg As New Bitmap(imgName)

                Eimage.SizeMode = PictureBoxSizeMode.StretchImage
                Eimage.Image = DirectCast(newimg, Image)
            End If

            dlgImage = Nothing
        Catch ae As System.ArgumentException
            imgName = " "

            MessageBox.Show(ae.Message.ToString())
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim SQL As String
            Dim ms As New MemoryStream
            Eimage.Image.Save(ms, Eimage.Image.RawFormat) '--i got error in this line..A generic error occurred in GDI+.
            Dim arrPic() As Byte = ms.GetBuffer()
            SQL = "select * From TblCustomers where CustomerNo='" & txtcostumerid.Text & "'"
            cmd = New OleDbCommand(SQL, cn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                SQL = "update TblCustomers set photo=@photo, CustName='" & txtcustomername.Text & "', BusinessPhone='" & txtbusinessphone.Text & "', FaxNumber='" & txtfaxnumber.Text & "', Address='" & txtaddress.Text & "', Zip_Code='" & txtzipcode.Text & "', Email_Address='" & txtemailaddress.Text & "' where CustomerNo='" & txtcostumerid.Text & "'"
                'sqlEmp = "INSERT INTO REGIS(photo) Values(" & "@photo" & ")"
                cmd = New OleDbCommand(SQL, cn)
                With cmd
                    .Parameters.AddWithValue("@photo", arrPic)
                    .ExecuteNonQuery()
                End With
                MsgBox("Customer Registration Succefully uPdate...", MsgBoxStyle.Information, "Payroll System...")
                Button2.Text = "Save"

            Else
                SQL = "INSERT INTO TblCustomers(PHOTO,CustomerNo,CustName,BusinessPhone,FaxNumber,Address,Zip_Code,Email_Address) values (" & "@photo" & ",'" & txtcostumerid.Text & "','" & txtcustomername.Text & "','" & txtbusinessphone.Text & "','" & txtfaxnumber.Text & "','" & txtaddress.Text & "','" & txtzipcode.Text & "','" & txtemailaddress.Text & "')"
                cmd = New OleDbCommand(SQL, cn)
                With cmd
                    .Parameters.AddWithValue("@photo", arrPic)
                    .ExecuteNonQuery()
                End With
                MsgBox("Customer Registration successfully saved....", MsgBoxStyle.Information, "Erorr")
            End If
            _GetDgv()
            _Clear()
            Me.Eimage.Image.Dispose()
            Eimage.Image = System.Drawing.Image.FromFile("noimage.jpg")
            Me.Eimage.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If cn.State = ConnectionState.Open Then cn.Close()
        cn.ConnectionString = cnstring
        cn.Open()
        _GetDgv()
        Eimage.Image = System.Drawing.Image.FromFile("noimage.jpg")
        Eimage.Refresh()
        _Clear()
    End Sub
    Sub _GetDgv()
        Dim dt As New DataTable
        dt.Clear()
        Dim sql As String
        sql = "select * From TblCustomers"
        Dim da As New OleDbDataAdapter(sql, cn)
        da.Fill(dt)
        With dgv
            .DataSource = dt
            .Columns(7).Visible = False
            .Columns(0).HeaderText = "Customer ID"
            .Columns(1).HeaderText = "Customer Name"
            .Columns(2).HeaderText = "Business Phone"
            .Columns(3).HeaderText = "Fax Number"
            .Columns(4).HeaderText = "Address"
            .Columns(5).HeaderText = "Zip Postal Code"
            .Columns(6).HeaderText = "Email Address"
            '.Columns(8).HeaderText = "CNIC"
            ' .Columns(9).HeaderText = "Address"
            '   .Columns(11).HeaderText = "Designation"




        End With
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub EID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcostumerid.KeyDown

        If e.KeyCode = Keys.Enter Then
            Try
                'Eimage.Image = Image.FromFile("D:\noimage.jpg")
                If Eimage.Image IsNot Nothing Then
                    Eimage.Image.Dispose()
                End If
                Dim sQL As String
                sQL = "SELECT * fROM TblCustomers WHERE Customer_ID='" & txtcostumerid.Text & "'"
                cmd = New OleDbCommand(sQL, cn)
                dr = cmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    txtcustomername.Text = dr("CustomerName").ToString
                    txtbusinessphone.Text = dr("BusinessPhone").ToString
                    txtaddress.Text = dr("Address").ToString
                    txtzipcode.Text = dr("Zip_Code").ToString
                    txtemailaddress.Text = dr("Email_Address").ToString
                    '  ESalry.Text = dr("").ToString
                    txtfaxnumber.Text = dr("FaxNumber").ToString
                    ' txtaddress1.Text = dr("Addr").ToString
                    'depr.DropDownStyle = ComboBoxStyle.DropDown
                    'depr.Text = dr("Department").ToString
                    'sec.Text = dr("SE_C").ToString

                    Dim fsImage As New FileStream("image.jpg", FileMode.Create)
                    Dim blob As Byte() = DirectCast(dr.GetValue(7), Byte())
                    fsImage.Write(blob, 0, blob.Length)
                    fsImage.Close()
                    fsImage = Nothing
                    Eimage.Image = Image.FromFile("image.jpg")
                    Eimage.SizeMode = PictureBoxSizeMode.StretchImage
                    Eimage.Refresh()
                    'Eimage.Dispose()
                Else
                    MsgBox("No Record were Found....", MsgBoxStyle.Exclamation, "Erorr Found")
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, "ERORR")
            End Try
        End If
    End Sub
    Sub _Clear()
        txtcostumerid.Clear()
        txtcustomername.Clear()
        txtbusinessphone.Clear()
        txtemailaddress.Clear()
        '  ESalry.Clear()
        txtzipcode.Clear()
        ' txtaddress1.Clear()
        txtfaxnumber.Clear()
        txtaddress.Clear()
        'depr.Clear()
        '()

    End Sub

    Private Sub EID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcostumerid.TextChanged

    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub dgv_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgv.MouseDoubleClick
        If dgv.SelectedRows.Count > 0 Then
            txtcostumerid.Text = dgv.SelectedRows(0).Cells(0).Value
            Button2.Enabled = False
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtcostumerid.Text = "" Or txtcustomername.Text = "" Or txtbusinessphone.Text = "" Or txtfaxnumber.Text = "" Then
            MsgBox("Do not Print First Please fill all abount Employee...", MsgBoxStyle.Exclamation, "Erorr Print....")
        Else
            '.ShowDialog()
        End If
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sql As String
        If txtcostumerid.Text = "" Then
            MsgBox("Please Enter Customer ID...", MsgBoxStyle.Exclamation, "Erorr Delete")
        Else
            If MsgBox("Do you want Delete this Record confirm....", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Delete Record Permently") = MsgBoxResult.Yes Then
                sql = "Delete from TblCustomers where CustomerNo='" & txtcostumerid.Text & "'"
                cmd = New OleDbCommand(sql, cn)
                cmd.ExecuteNonQuery()
                _GetDgv()
                Eimage.Image.Dispose()
                _Clear()
                Button2.Enabled = True

            Else
                Exit Sub

            End If
        End If

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If Eimage.Image IsNot Nothing Then
            Eimage.Image.Dispose()
        End If
        Close()

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim dt As New DataTable
        dt.Clear()
        Dim sql As String
        Dim da As New OleDbDataAdapter
        Dim Search As String = "%" & TextBox1.Text & "%"
        If ComboBox1.Text = "ID" Then
            sql = "select * From TblCustomers where Customer_ID like'" & Search & "'"
            da = New OleDbDataAdapter(sql, cn)
            da.Fill(dt)
            With dgv
                .DataSource = dt
                .Columns(7).Visible = False
                .Columns(0).HeaderText = "Customer ID"
                .Columns(1).HeaderText = "Customer Name"
                .Columns(2).HeaderText = "Business Phone"
                .Columns(3).HeaderText = "Fax Number"
                .Columns(4).HeaderText = "Address"
                .Columns(5).HeaderText = "Zip Postal Code"
                .Columns(6).HeaderText = "Email Address"
                ' .Columns(8).HeaderText = "CNIC"
                ' .Columns(9).HeaderText = "Address"
                '   .Columns(11).HeaderText = "Designation"
            End With
        Else
            If ComboBox1.Text = "Name" Then
                dt.Clear()
                sql = "select * From TblCustomers where CustName like'" & Search & "'"
                da = New OleDbDataAdapter(sql, cn)
                da.Fill(dt)
                With dgv
                    .DataSource = dt
                    .Columns(7).Visible = False
                    .Columns(0).HeaderText = "CustomerNo"
                    .Columns(1).HeaderText = "CustName"
                    .Columns(2).HeaderText = "Business Phone"
                    .Columns(3).HeaderText = "Fax Number"
                    .Columns(4).HeaderText = "Address"
                    .Columns(5).HeaderText = "Zip Postal Code"
                    .Columns(6).HeaderText = "Email Address"
                    ' .Columns(8).HeaderText = "CNIC"
                    '.Columns(9).HeaderText = "Address"
                    ' .Columns(11).HeaderText = "Designation"
                End With
            End If
        End If
    End Sub

    ' Private Sub depr_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '     Me.depr.DropDownStyle = ComboBoxStyle.DropDownList
    '  Me.depr.Items.Clear()
    ' Dim sql As String
    '    sql = "Select * From E_sec"
    '    cmd = New OleDbCommand(sql, cn)
    '   dr = cmd.ExecuteReader
    '  While dr.Read
    '       depr.Items.Add(dr.GetValue(1)).ToString()
    '   End While
    'End Sub

    Private Sub depr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '  frm_Sec.ShowDialog()

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If txtcostumerid.Text = "" Then
            MsgBox("Please Enter Employee ID...", MsgBoxStyle.Exclamation, "Erorr Search...")
        Else
            Try
                'Eimage.Image = Image.FromFile("D:\noimage.jpg")
                If Eimage.Image IsNot Nothing Then
                    Eimage.Image.Dispose()
                End If
                Dim sQL As String
                sQL = "SELECT * fROM TblCustomers WHERE CustomerNo='" & txtcostumerid.Text & "'"
                cmd = New OleDbCommand(sQL, cn)
                dr = cmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    txtcustomername.Text = dr("CustName").ToString
                    txtbusinessphone.Text = dr("BusinessPhone").ToString
                    txtaddress.Text = dr("Address").ToString
                    txtzipcode.Text = dr("Zip_Code").ToString
                    txtemailaddress.Text = dr("Email_Address").ToString
                    ' ESalry.Text = dr("").ToString
                    txtfaxnumber.Text = dr("FaxNumber").ToString
                    Button2.Enabled = True
                    Button2.Text = "Update"
                    '  txtaddress1.Text = dr("Addr").ToString
                    'depr.DropDownStyle = ComboBoxStyle.DropDown
                    'depr.Text = dr("Department").ToString
                    '  sec.DropDownStyle = ComboBoxStyle.DropDown
                    '  sec.Text = dr("SE_C").ToString

                    Dim fsImage As New FileStream("image.jpg", FileMode.Create)
                    Dim blob As Byte() = DirectCast(dr.GetValue(7), Byte())
                    fsImage.Write(blob, 0, blob.Length)
                    fsImage.Close()
                    fsImage = Nothing
                    Eimage.Image = Image.FromFile("image.jpg")
                    Eimage.SizeMode = PictureBoxSizeMode.StretchImage
                    Eimage.Refresh()
                    'Eimage.Dispose()
                Else
                    MsgBox("No Record were Found....", MsgBoxStyle.Exclamation, "Erorr Found")
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, "ERORR")
            End Try
        End If
    End Sub

    '  Private Sub sec_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    'Me.sec.DropDownStyle = ComboBoxStyle.DropDownList
    '   Me.sec.Items.Clear()
    '  Dim sql As String
    '     sql = "Select * From desig"
    '  cmd = New OleDbCommand(sql, cn)
    '  dr = cmd.ExecuteReader
    '   While dr.Read
    '       sec.Items.Add(dr.GetValue(1)).ToString()
    '    End While
    ' End Sub

    Private Sub sec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '  frmDegis.ShowDialog()

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        ComboBox1.Enabled = True
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        ComboBox1.Enabled = False
    End Sub

    Private Sub Button9_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtbusinessphone_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbusinessphone.TextChanged

    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtzipcode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtzipcode.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtbusinessphone_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbusinessphone.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtfaxnumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfaxnumber.TextChanged

    End Sub

    Private Sub txtfaxnumber_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfaxnumber.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtcostumerid_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcostumerid.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtaddress_TextChanged(sender As Object, e As EventArgs) Handles txtaddress.TextChanged

    End Sub
End Class

