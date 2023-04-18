Imports System.IO
Imports System.Byte
Imports System.Drawing
'Imports System.IO.MemoryMappedFiles
Imports System.IO.StreamReader
Imports System.Data.OleDb


Public Class frmProduct

    Dim danz As ListViewItem
    Dim category As String
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
            SQL = "select * From TblProduct where ProductID='" & txtid.Text & "'"
            cmd = New OleDbCommand(SQL, cn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                SQL = "update TblProduct set photo=@photo, ProductName='" & txtproductname.Text & "', Category='" & category & "', Type='" & lbltype.Text & "', StocksOnHand='" & txtquantity.Text & "', UnitPrice='" & txtprice.Text & "', UnitInStock='" & txtunitinstock.Text & "', UnitOnOrder='" & txtunitsinorder.Text & "' where ProductID='" & txtid.Text & "'"
                'sqlEmp = "INSERT INTO REGIS(photo) Values(" & "@photo" & ")"
                cmd = New OleDbCommand(SQL, cn)
                With cmd
                    .Parameters.AddWithValue("@photo", arrPic)
                    .ExecuteNonQuery()
                End With
                MsgBox("Product Registration Succefully uPdate...", MsgBoxStyle.Information, "Inventory System...")
                Button2.Text = "Save"
            Else
                SQL = "INSERT INTO TBLPRODUCT(PHOTO,ProductID,ProductName,Category,StocksOnHand,Type,UnitPrice,UnitInStock,UnitOnOrder) values (" & "@photo" & ",'" & txtid.Text & "','" & txtproductname.Text & "','" & category & "','" & txtquantity.Text & "','" & lbltype.Text & "','" & txtprice.Text & "','" & txtunitinstock.Text & "','" & txtunitsinorder.Text & "')"
                cmd = New OleDbCommand(SQL, cn)
                With cmd
                    .Parameters.AddWithValue("@photo", arrPic)
                    .ExecuteNonQuery()

                End With
                MsgBox("Product Registration successfully saved....", MsgBoxStyle.Information, "Erorr")
                ' RadioButton4.Checked = True
                ' RadioButton3.Checked = False
                ' RadioButton2.Checked = False
                ' RadioButton1.Checked = False
                ' Form6.Visible = True
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
        '    VScrollBar1.Maximum = (10000)
        '   VScrollBar1.Size = New Size(175, 15)


        '    Me.BackColor = Color.DarkTurquoise


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
        sql = "select * From TblProduct"
        Dim da As New OleDbDataAdapter(sql, cn)
        da.Fill(dt)
        With dgv
            .DataSource = dt
            .Columns(7).Visible = False
            .Columns(0).HeaderText = "Product ID"
            .Columns(1).HeaderText = "Product Name"
            .Columns(2).HeaderText = "Category"
            .Columns(3).HeaderText = "Type"
            .Columns(4).HeaderText = "Quantity"
            .Columns(5).HeaderText = "Units In Stock"
            .Columns(6).HeaderText = "Units In Order"
            .Columns(8).HeaderText = "Unit Pice"




        End With
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub EID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Try
                'Eimage.Image = Image.FromFile("D:\noimage.jpg")
                If Eimage.Image IsNot Nothing Then
                    Eimage.Image.Dispose()
                End If
                Dim sQL As String
                sQL = "SELECT * fROM TBLPRODUCTS WHERE ProductID='" & txtid.Text & "'"
                cmd = New OleDbCommand(sQL, cn)
                dr = cmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    txtproductname.Text = dr("ProductName").ToString
                    category = dr("Category").ToString
                    lbltype.Text = dr("Type").ToString
                    txtquantity.Text = dr("StocksOnHand").ToString
                    txtunitinstock.Text = dr("UnitInStock").ToString
                    txtunitsinorder.Text = dr("UnitOnOrder").ToString
                    txtprice.Text = dr("UnitPrice").ToString
                    ' txtaddress.Text = dr("Addr").ToString
                    'depr.DropDownStyle = ComboBoxStyle.DropDown
                    ' depr.Text = dr("Department").ToString

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
        txtid.Clear()
        txtproductname.Clear()
        txtunitinstock.Clear()
        txtunitsinorder.Clear()
        lbltype.Text = ""
        ' txtaddress.Clear()
        txtprice.Clear()
        txtquantity.Clear()
        'depr.Clear()
        '()
        '  RadioButton1.Checked = True
        '   RadioButton1.Checked = True
        '  RadioButton1.Checked = True
    End Sub

    Private Sub EID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub dgv_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If txtid.Text = "" Or txtproductname.Text = "" Or category = "" Or txtprice.Text = "" Then
            MsgBox("Do not Print First Please fill all abount Employee...", MsgBoxStyle.Exclamation, "Erorr Print....")
        Else
            '    Card_Print.ShowDialog()
        End If
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim sql As String
        If txtid.Text = "" Then
            MsgBox("Please Enter Product ID...", MsgBoxStyle.Exclamation, "Erorr Delete")
        Else
            If MsgBox("Do you want Delete this Record confirm....", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Delete Record Permently") = MsgBoxResult.Yes Then
                sql = "Delete from TblProduct where ProductID='" & txtid.Text & "'"
                cmd = New OleDbCommand(sql, cn)
                cmd.ExecuteNonQuery()
                _GetDgv()
                Eimage.Image.Dispose()
                _Clear()
                Button2.Text = "Save"
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
            sql = "select * From TblProduct where ProductID like'" & Search & "'"
            da = New OleDbDataAdapter(sql, cn)
            da.Fill(dt)
            With dgv
                .DataSource = dt
                .Columns(7).Visible = False
                .Columns(0).HeaderText = "Product ID"
                .Columns(1).HeaderText = "Product Name"
                .Columns(2).HeaderText = "Category"
                .Columns(3).HeaderText = "StocksOnHand"
                .Columns(4).HeaderText = "Unit Price"
                .Columns(5).HeaderText = "Unit In Stock"
                .Columns(6).HeaderText = "Units On Order"
                .Columns(8).HeaderText = "CNIC"
            End With
        Else
            If ComboBox1.Text = "Name" Then
                dt.Clear()
                sql = "select * From TblProduct where ProductName like'" & Search & "'"
                da = New OleDbDataAdapter(sql, cn)
                da.Fill(dt)
                With dgv
                    .DataSource = dt
                    .Columns(7).Visible = False
                    .Columns(0).HeaderText = "Product ID"
                    .Columns(1).HeaderText = "Product Name"
                    .Columns(2).HeaderText = "Category"
                    .Columns(3).HeaderText = "StocksOnHand"
                    .Columns(4).HeaderText = "Unit Price"
                    .Columns(5).HeaderText = "Unit In Stock"
                    .Columns(6).HeaderText = "Units On Order"
                    .Columns(8).HeaderText = "CNIC"
                    '  .Columns(9).HeaderText = "CNIC"

                End With
            End If
        End If
    End Sub

    ' Private Sub depr_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles depr.MouseClick
    '    Me.depr.DropDownStyle = ComboBoxStyle.DropDownList
    '    Me.depr.Items.Clear()
    ' Dim sql As String
    '    sql = "Select * From E_sec"
    '   cmd = New OleDbCommand(sql, cn)
    '  dr = cmd.ExecuteReader
    '   While dr.Read
    '       depr.Items.Add(dr.GetValue(1)).ToString()
    '   End While
    ' End Sub

    Private Sub depr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If txtid.Text = "" Then
            MsgBox("Please Enter Employee ID...", MsgBoxStyle.Exclamation, "Erorr Search...")
        Else
            Try
                'Eimage.Image = Image.FromFile("D:\noimage.jpg")
                If Eimage.Image IsNot Nothing Then
                    Eimage.Image.Dispose()
                End If
                Dim sQL As String
                sQL = "SELECT * fROM TBLPRODUCT WHERE ProductID='" & txtid.Text & "'"
                cmd = New OleDbCommand(sQL, cn)
                dr = cmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    txtproductname.Text = dr("ProductName").ToString
                    category = dr("Category").ToString
                    lbltype.Text = dr("Type").ToString
                    txtquantity.Text = dr("StocksOnHand").ToString
                    txtprice.Text = dr("UnitPrice").ToString
                    txtunitinstock.Text = dr("UnitInStock").ToString
                    txtunitsinorder.Text = dr("UnitOnOrder").ToString


                    'txtaddress.Text = dr("Addr").ToString
                    ' depr.Text = dr("Department").ToString
                    'depr.Text = dr("Department").ToString
                    Dim fsImage As New FileStream("image.jpg", FileMode.Create)
                    Dim blob As Byte() = DirectCast(dr.GetValue(7), Byte())
                    fsImage.Write(blob, 0, blob.Length)
                    fsImage.Close()
                    fsImage = Nothing
                    Eimage.Image = Image.FromFile("image.jpg")
                    Eimage.SizeMode = PictureBoxSizeMode.StretchImage
                    Eimage.Refresh()
                    'Eimage.Dispose()
                    Button2.Text = "Update"
                Else
                    MsgBox("No Record were Found....", MsgBoxStyle.Exclamation, "Erorr Found")
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, "ERORR")
            End Try
        End If
    End Sub

    '  Private Sub sec_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles sec.MouseClick
    '      Me.sec.DropDownStyle = ComboBoxStyle.DropDownList
    '     Me.sec.Items.Clear()
    '  Dim sql As String
    '     sql = "Select * From desig"
    '    cmd = New OleDbCommand(sql, cn)
    '    dr = cmd.ExecuteReader
    '    While dr.Read
    '        sec.Items.Add(dr.GetValue(1)).ToString()
    '   End While
    'End Sub

    Private Sub sec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '  frmDegis.ShowDialog()

    End Sub



    Private Sub dgv_MouseDoubleClick1(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgv.MouseDoubleClick
        If dgv.SelectedRows.Count > 0 Then
            txtid.Text = dgv.SelectedRows(0).Cells(0).Value

        End If
    End Sub

    Private Sub dgv_CellContentClick_1(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub Efname_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub GroupBox3_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButton1.CheckedChanged
        category = "Food and Beverages"
        frmtype.lblfoodandbeverages.Visible = True
        frmtype.lblpersonalcare.Visible = False
        frmtype.lblhouseholdcleaner.Visible = False
        frmtype.lvfoodandproducts.Visible = True
        frmtype.lvpersonalcare.Visible = False
        frmtype.lvhouseholdcleaner.Visible = False
        frmtype.Show()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButton2.CheckedChanged
        category = "Personal Care"
        frmtype.lblfoodandbeverages.Visible = False
        frmtype.lblpersonalcare.Visible = True
        frmtype.lblhouseholdcleaner.Visible = False
        frmtype.lvfoodandproducts.Visible = False
        frmtype.lvpersonalcare.Visible = True
        frmtype.lvhouseholdcleaner.Visible = False
        frmtype.Show()

    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButton3.CheckedChanged
        category = "House Hold Cleaner"
        frmtype.lblfoodandbeverages.Visible = False
        frmtype.lblpersonalcare.Visible = False
        frmtype.lblhouseholdcleaner.Visible = True
        frmtype.lvfoodandproducts.Visible = False
        frmtype.lvpersonalcare.Visible = False
        frmtype.lvhouseholdcleaner.Visible = True
        frmtype.Show()
    End Sub


    Private Sub ER_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub lv2_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        '  ER.Text = lv2.FocusedItem.Text

        '    frmMain.lblProduct.Text = lv2.FocusedItem.SubItems(1).Text
        '   frmMain.lblPrice.Text = lv2.FocusedItem.SubItems(2).Text
    End Sub


    Private Sub lv2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub txtcnic_TextChanged(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Sub radio()
    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        category = ""
    End Sub

    Private Sub txtquantity_TextChanged(ByVal sender As Object, ByVal e As EventArgs)

    End Sub


    Private Sub txtquantity_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtquantity.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
        ' If Char.IsNumber(e.KeyChar) = False Then
        'e.Handled = True

        ' End If
    End Sub

    Private Sub HScrollBar1_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)

    End Sub

    Private Sub HScrollBar1_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub HScrollBar1_Scroll_1(ByVal sender As Object, ByVal e As ScrollEventArgs)

    End Sub

    Private Sub txtquantity_TextChanged_1(ByVal sender As Object, ByVal e As EventArgs) Handles txtquantity.TextChanged

    End Sub


    Private Sub VScrollBar1_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)

    End Sub

    Private Sub txtunitinstock_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtunitinstock.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtunitsinorder_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtunitsinorder.TextChanged

    End Sub

    Private Sub txtunitsinorder_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtunitsinorder.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtunitinstock_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtunitinstock.TextChanged
    End Sub

    Private Sub txtprice_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtprice.TextChanged
    End Sub
End Class
