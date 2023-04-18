<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmtype
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lvfoodandproducts = New System.Windows.Forms.ListView()
        Me.lv2Code = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvpersonalcare = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvhouseholdcleaner = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblfoodandbeverages = New System.Windows.Forms.Label()
        Me.lblpersonalcare = New System.Windows.Forms.Label()
        Me.lblhouseholdcleaner = New System.Windows.Forms.Label()
        Me.btnok = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lvfoodandproducts
        '
        Me.lvfoodandproducts.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lv2Code})
        Me.lvfoodandproducts.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvfoodandproducts.FullRowSelect = True
        Me.lvfoodandproducts.GridLines = True
        Me.lvfoodandproducts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvfoodandproducts.Location = New System.Drawing.Point(12, 42)
        Me.lvfoodandproducts.Name = "lvfoodandproducts"
        Me.lvfoodandproducts.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lvfoodandproducts.Size = New System.Drawing.Size(206, 187)
        Me.lvfoodandproducts.TabIndex = 60
        Me.lvfoodandproducts.UseCompatibleStateImageBehavior = False
        Me.lvfoodandproducts.View = System.Windows.Forms.View.Details
        '
        'lv2Code
        '
        Me.lv2Code.Text = "Type"
        Me.lv2Code.Width = 200
        '
        'lvpersonalcare
        '
        Me.lvpersonalcare.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lvpersonalcare.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvpersonalcare.FullRowSelect = True
        Me.lvpersonalcare.GridLines = True
        Me.lvpersonalcare.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvpersonalcare.Location = New System.Drawing.Point(12, 42)
        Me.lvpersonalcare.Name = "lvpersonalcare"
        Me.lvpersonalcare.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lvpersonalcare.Size = New System.Drawing.Size(206, 187)
        Me.lvpersonalcare.TabIndex = 61
        Me.lvpersonalcare.UseCompatibleStateImageBehavior = False
        Me.lvpersonalcare.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Type"
        Me.ColumnHeader1.Width = 200
        '
        'lvhouseholdcleaner
        '
        Me.lvhouseholdcleaner.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.lvhouseholdcleaner.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvhouseholdcleaner.FullRowSelect = True
        Me.lvhouseholdcleaner.GridLines = True
        Me.lvhouseholdcleaner.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvhouseholdcleaner.Location = New System.Drawing.Point(12, 42)
        Me.lvhouseholdcleaner.Name = "lvhouseholdcleaner"
        Me.lvhouseholdcleaner.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lvhouseholdcleaner.Size = New System.Drawing.Size(206, 187)
        Me.lvhouseholdcleaner.TabIndex = 62
        Me.lvhouseholdcleaner.UseCompatibleStateImageBehavior = False
        Me.lvhouseholdcleaner.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Type"
        Me.ColumnHeader2.Width = 200
        '
        'lblfoodandbeverages
        '
        Me.lblfoodandbeverages.AutoSize = True
        Me.lblfoodandbeverages.Location = New System.Drawing.Point(12, 9)
        Me.lblfoodandbeverages.Name = "lblfoodandbeverages"
        Me.lblfoodandbeverages.Size = New System.Drawing.Size(106, 13)
        Me.lblfoodandbeverages.TabIndex = 63
        Me.lblfoodandbeverages.Text = "Food and Beverages"
        '
        'lblpersonalcare
        '
        Me.lblpersonalcare.AutoSize = True
        Me.lblpersonalcare.Location = New System.Drawing.Point(12, 9)
        Me.lblpersonalcare.Name = "lblpersonalcare"
        Me.lblpersonalcare.Size = New System.Drawing.Size(73, 13)
        Me.lblpersonalcare.TabIndex = 64
        Me.lblpersonalcare.Text = "Personal Care"
        '
        'lblhouseholdcleaner
        '
        Me.lblhouseholdcleaner.AutoSize = True
        Me.lblhouseholdcleaner.Location = New System.Drawing.Point(16, 9)
        Me.lblhouseholdcleaner.Name = "lblhouseholdcleaner"
        Me.lblhouseholdcleaner.Size = New System.Drawing.Size(102, 13)
        Me.lblhouseholdcleaner.TabIndex = 65
        Me.lblhouseholdcleaner.Text = "House Hold Cleaner"
        '
        'btnok
        '
        Me.btnok.Location = New System.Drawing.Point(73, 235)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(69, 31)
        Me.btnok.TabIndex = 66
        Me.btnok.Text = "OK"
        Me.btnok.UseVisualStyleBackColor = True
        '
        'frmtype
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(241, 267)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.lblhouseholdcleaner)
        Me.Controls.Add(Me.lblpersonalcare)
        Me.Controls.Add(Me.lblfoodandbeverages)
        Me.Controls.Add(Me.lvhouseholdcleaner)
        Me.Controls.Add(Me.lvpersonalcare)
        Me.Controls.Add(Me.lvfoodandproducts)
        Me.Name = "frmtype"
        Me.Text = "Type"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lvfoodandproducts As System.Windows.Forms.ListView
    Friend WithEvents lv2Code As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvpersonalcare As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvhouseholdcleaner As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblfoodandbeverages As System.Windows.Forms.Label
    Friend WithEvents lblpersonalcare As System.Windows.Forms.Label
    Friend WithEvents lblhouseholdcleaner As System.Windows.Forms.Label
    Friend WithEvents btnok As System.Windows.Forms.Button
End Class
