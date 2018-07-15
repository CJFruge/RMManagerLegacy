<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReport
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
        Me.rViewer = New Microsoft.Reporting.WinForms.ReportViewer
        Me.SuspendLayout()
        '
        'rViewer
        '
        Me.rViewer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rViewer.Location = New System.Drawing.Point(0, 0)
        Me.rViewer.Name = "rViewer"
        Me.rViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote
        Me.rViewer.ServerReport.ReportServerUrl = New System.Uri("", System.UriKind.Relative)
        Me.rViewer.Size = New System.Drawing.Size(574, 362)
        Me.rViewer.TabIndex = 0
        '
        'frmReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 385)
        Me.Controls.Add(Me.rViewer)
        Me.Name = "frmReport"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Report"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rViewer As Microsoft.Reporting.WinForms.ReportViewer
End Class
