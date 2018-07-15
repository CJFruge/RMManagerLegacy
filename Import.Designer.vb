<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.lbFrom = New System.Windows.Forms.ListBox
        Me.lbTo = New System.Windows.Forms.ListBox
        Me.cmdImportSelected = New System.Windows.Forms.Button
        Me.cmdImportAll = New System.Windows.Forms.Button
        Me.cmdUnImportAll = New System.Windows.Forms.Button
        Me.cmdUnImportSelected = New System.Windows.Forms.Button
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(331, 262)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(170, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(78, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(88, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(78, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'lbFrom
        '
        Me.lbFrom.FormattingEnabled = True
        Me.lbFrom.Location = New System.Drawing.Point(14, 21)
        Me.lbFrom.Name = "lbFrom"
        Me.lbFrom.Size = New System.Drawing.Size(208, 225)
        Me.lbFrom.TabIndex = 1
        '
        'lbTo
        '
        Me.lbTo.FormattingEnabled = True
        Me.lbTo.Location = New System.Drawing.Point(290, 21)
        Me.lbTo.Name = "lbTo"
        Me.lbTo.Size = New System.Drawing.Size(208, 225)
        Me.lbTo.TabIndex = 2
        '
        'cmdImportSelected
        '
        Me.cmdImportSelected.Location = New System.Drawing.Point(239, 21)
        Me.cmdImportSelected.Name = "cmdImportSelected"
        Me.cmdImportSelected.Size = New System.Drawing.Size(35, 23)
        Me.cmdImportSelected.TabIndex = 3
        Me.cmdImportSelected.Text = ">"
        Me.cmdImportSelected.UseVisualStyleBackColor = True
        '
        'cmdImportAll
        '
        Me.cmdImportAll.Location = New System.Drawing.Point(239, 54)
        Me.cmdImportAll.Name = "cmdImportAll"
        Me.cmdImportAll.Size = New System.Drawing.Size(35, 23)
        Me.cmdImportAll.TabIndex = 4
        Me.cmdImportAll.Text = ">>"
        Me.cmdImportAll.UseVisualStyleBackColor = True
        '
        'cmdUnImportAll
        '
        Me.cmdUnImportAll.Location = New System.Drawing.Point(239, 223)
        Me.cmdUnImportAll.Name = "cmdUnImportAll"
        Me.cmdUnImportAll.Size = New System.Drawing.Size(35, 23)
        Me.cmdUnImportAll.TabIndex = 5
        Me.cmdUnImportAll.Text = "<<"
        Me.cmdUnImportAll.UseVisualStyleBackColor = True
        '
        'cmdUnImportSelected
        '
        Me.cmdUnImportSelected.Location = New System.Drawing.Point(239, 192)
        Me.cmdUnImportSelected.Name = "cmdUnImportSelected"
        Me.cmdUnImportSelected.Size = New System.Drawing.Size(35, 23)
        Me.cmdUnImportSelected.TabIndex = 6
        Me.cmdUnImportSelected.Text = ">"
        Me.cmdUnImportSelected.UseVisualStyleBackColor = True
        '
        'frmImport
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(532, 301)
        Me.Controls.Add(Me.cmdUnImportSelected)
        Me.Controls.Add(Me.cmdUnImportAll)
        Me.Controls.Add(Me.cmdImportAll)
        Me.Controls.Add(Me.cmdImportSelected)
        Me.Controls.Add(Me.lbTo)
        Me.Controls.Add(Me.lbFrom)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImport"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Import"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents lbFrom As System.Windows.Forms.ListBox
    Friend WithEvents lbTo As System.Windows.Forms.ListBox
    Friend WithEvents cmdImportSelected As System.Windows.Forms.Button
    Friend WithEvents cmdImportAll As System.Windows.Forms.Button
    Friend WithEvents cmdUnImportAll As System.Windows.Forms.Button
    Friend WithEvents cmdUnImportSelected As System.Windows.Forms.Button

End Class
