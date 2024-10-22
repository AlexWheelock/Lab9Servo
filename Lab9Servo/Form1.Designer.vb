<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Me.SerialPort = New System.IO.Ports.SerialPort(Me.components)
        Me.ValueLabel = New System.Windows.Forms.Label()
        Me.ADCRequestTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ResendHandshakeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.RHButton = New System.Windows.Forms.Button()
        Me.TempButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'SerialPort
        '
        Me.SerialPort.PortName = "COM4"
        '
        'ValueLabel
        '
        Me.ValueLabel.AutoSize = True
        Me.ValueLabel.Location = New System.Drawing.Point(212, 17)
        Me.ValueLabel.Name = "ValueLabel"
        Me.ValueLabel.Size = New System.Drawing.Size(0, 16)
        Me.ValueLabel.TabIndex = 2
        '
        'ADCRequestTimer
        '
        Me.ADCRequestTimer.Enabled = True
        Me.ADCRequestTimer.Interval = 300
        '
        'ResendHandshakeTimer
        '
        Me.ResendHandshakeTimer.Interval = 1000
        '
        'RHButton
        '
        Me.RHButton.Location = New System.Drawing.Point(137, 12)
        Me.RHButton.Name = "RHButton"
        Me.RHButton.Size = New System.Drawing.Size(75, 23)
        Me.RHButton.TabIndex = 6
        Me.RHButton.Text = "RH"
        Me.RHButton.UseVisualStyleBackColor = True
        '
        'TempButton
        '
        Me.TempButton.Location = New System.Drawing.Point(137, 41)
        Me.TempButton.Name = "TempButton"
        Me.TempButton.Size = New System.Drawing.Size(75, 23)
        Me.TempButton.TabIndex = 7
        Me.TempButton.Text = "Temp"
        Me.TempButton.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(355, 73)
        Me.Controls.Add(Me.TempButton)
        Me.Controls.Add(Me.RHButton)
        Me.Controls.Add(Me.ValueLabel)
        Me.Name = "Form1"
        Me.Text = "Lab 9 Serial"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SerialPort As IO.Ports.SerialPort
    Friend WithEvents ValueLabel As Label
    Friend WithEvents ADCRequestTimer As Timer
    Friend WithEvents ResendHandshakeTimer As Timer
    Friend WithEvents RHButton As Button
    Friend WithEvents TempButton As Button
End Class
