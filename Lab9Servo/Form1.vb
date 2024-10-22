Option Compare Text
Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SerialPort.PortName = "COM4"
        SerialPort.BaudRate = 9600
        SerialPort.Open()
        ADCRequestTimer.Enabled = True
    End Sub

    Sub WriteHandshake()
        Dim handshake(1) As Byte
        Dim position As Integer
        Dim activeConversation As Boolean = TestConversation(False, True)
        handshake(0) = &H24

        If activeConversation = False Then
            SerialPort.Write(handshake, 0, 1)
            Console.WriteLine($"Handshake Sent: {Chr(handshake(0))}")
            TestConversation(True)
        End If

        ResendHandshakeTimer.Enabled = True

    End Sub

    Private Sub SerialPort_DataReceived(sender As Object, e As IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort.DataReceived
        Dim position(1) As Byte
        Dim data(SerialPort.BytesToRead) As Byte
        Static bytesReceived As Integer
        Static ADC9to2 As Integer
        Static ADC1to0 As Integer
        Dim ADCcount As Integer
        Dim temperature As Double
        Dim RH As Double

        ResendHandshakeTimer.Enabled = False
        ResendHandshakeTimer.Interval = 1000

        'Console.WriteLine($"Bytes Received: {SerialPort.BytesToRead}")
        SerialPort.Read(data, 0, SerialPort.BytesToRead)

        'Console.WriteLine($"Received: {Chr(data(0))}")

        position(0) = UpdatePosition(False, True)
        position(1) = AnalogSelect(0, True)

        If bytesReceived = 0 Then
            SerialPort.Write(position, 0, 1)
            Console.WriteLine($"Sent Position: {position(0)}")
            bytesReceived += 1
        ElseIf bytesReceived = 1 Then
            SerialPort.Write(position, 1, 1)
            Console.WriteLine($"ADC request sent: {Chr(position(1))}")
            bytesReceived += 1
        ElseIf bytesReceived = 2 Then
            SerialPort.Write(position, 1, 1)
            'Console.WriteLine($"Sent verification of byte 1: {position(0)}")
            ADC9to2 = data(0) * 4
            'Console.WriteLine($"Upper byte received: {data(0)}")
            bytesReceived += 1
        ElseIf bytesReceived = 3 Then
            ADC1to0 = data(0) / 64
            ADCcount = ADC9to2 + ADC1to0
            'temperature = ADCcount * (5 / 1023) * 100
            temperature = (ADCcount * 0.1 * (1000 * (5 / 1023))) - 40
            RH = ADCcount / 13.42281879
            'Console.WriteLine($"Lower byte received: {data(0)}")
            Console.WriteLine($"ADC: {ADCcount}")
            If position(1) = 45 Then
                Console.WriteLine($"Temperature: {temperature} degrees Celsius")
            Else
                Console.WriteLine($"RH: {RH}%")
            End If
            bytesReceived = 0
            TestConversation(False)
        End If

    End Sub

    Function TestConversation(update As Boolean, Optional read As Boolean = False) As Boolean
        Static activeConversation As Boolean

        If read = False Then
            activeConversation = update
        End If

        Return activeConversation
    End Function

    Function UpdatePosition(increment As Boolean, Optional read As Boolean = False) As Integer
        Static position As Integer

        If read = False Then
            If increment Then
                If position <> 20 Then
                    position += 1
                End If
            Else
                If position <> 0 Then
                    position -= 1
                End If
            End If
        End If

        Return position + 5
    End Function

    Function ADCRequest(request As Boolean, Optional read As Boolean = False) As Integer
        Return 45
    End Function

    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = "w" Then
            UpdatePosition(True)
        ElseIf e.KeyChar = "s" Then
            UpdatePosition(False)
        End If
    End Sub

    Private Sub ADCRequestTimer_Tick(sender As Object, e As EventArgs) Handles ADCRequestTimer.Tick
        WriteHandshake()
    End Sub

    Private Sub ResendHandshakeTimer_Tick(sender As Object, e As EventArgs) Handles ResendHandshakeTimer.Tick
        TestConversation(False)
        WriteHandshake()
    End Sub

    Private Sub RHButton_Click(sender As Object, e As EventArgs) Handles RHButton.Click
        AnalogSelect(46)
    End Sub

    Private Sub TempButton_Click(sender As Object, e As EventArgs) Handles TempButton.Click
        AnalogSelect(45)
    End Sub

    Function AnalogSelect(update As Integer, Optional read As Boolean = False) As Integer
        Static selectedChannel As Integer

        If read = False Then
            selectedChannel = update
        End If

        If selectedChannel = 0 Then
            selectedChannel = 45
        End If

        Return selectedChannel
    End Function

End Class
