Public Class Form1

    Public Class vars
        Public Shared x As Integer = 0
        Public Shared y As Integer = 0
        Public Shared bg(8) As Bitmap
        Public Shared img As New Bitmap(My.Resources.bg)
        Public Shared map(15, 11) As Integer
        Public Shared sp(16) As Bitmap
        Public Shared img2 As New Bitmap(My.Resources.sp)
        Public Shared map2 As Bitmap
        Public Shared bob(1) As Bitmap
        Public Shared img3 As New Bitmap(My.Resources.bomb)
        Public Shared draw As System.Drawing.Graphics
        Public Shared key As Boolean
        Public Shared winre As Boolean
        Public Shared g As Boolean
        Public Shared l As Boolean
        Public Shared r As Boolean
        Public Shared u As Boolean
        Public Shared d As Boolean
        Public Shared ld As Boolean
        Public Shared coin As Integer
        Public Shared bomb As Integer
        Public Shared bo As Boolean
        Public Shared bt As Integer
        Public Shared bx As Integer
        Public Shared by As Integer
        Public Shared lvl As Integer
    End Class

    Public Class a
        Public Shared a
        Public Shared b
        Public Shared f
    End Class


    Private Sub Form1_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load

        For x = 0 To 8
            vars.bg(x) = New Bitmap(16, 16)
            Dim gr As Graphics = Graphics.FromImage(vars.bg(x))
            gr.DrawImage(vars.img, 0, 0, New RectangleF(x * 16, 0, 16, 16), GraphicsUnit.Pixel)
        Next
        For x = 0 To 16
            vars.sp(x) = New Bitmap(16, 16)
            Dim gr As Graphics = Graphics.FromImage(vars.sp(x))
            gr.DrawImage(vars.img2, 0, 0, New RectangleF(x * 16, 0, 16, 16), GraphicsUnit.Pixel)
        Next
        For x = 0 To 1
            vars.bob(x) = New Bitmap(16, 16)
            Dim gr As Graphics = Graphics.FromImage(vars.bob(x))
            gr.DrawImage(vars.img3, 0, 0, New RectangleF(x * 16, 0, 16, 16), GraphicsUnit.Pixel)
        Next
        levelload()
        Timer1.Start()
    End Sub

    Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Int32) As UShort


    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick

        Dim m = False

        If GetAsyncKeyState(Convert.ToInt32(Keys.Right)) Then
            If vars.r Then vars.x += 1 : Timer2.Start() : a.f = 0 : m = True
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.Left)) Then
            If vars.l Then vars.x -= 1 : Timer2.Start() : a.f = 2 : m = True
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.Up)) Then
            If vars.u And vars.ld Then vars.y -= 1 : Timer2.Start() : a.f = 3 : m = True
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.Down)) Then
            If vars.d And vars.ld Then vars.y += 1 : Timer2.Start() : a.f = 3 : m = True
        End If

        If Not m Then
            Timer2.Stop()
            a.a = 0
            a.f = 1
            If Not vars.g Then a.a = 16 : a.f = 0
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.F1)) Then
            TextBox1.Visible = True
        Else
            TextBox1.Visible = False
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.F2)) Then
            TextBox2.Visible = True
        Else
            TextBox2.Visible = False
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.F3)) Then
            TextBox3.Visible = True
        Else
            TextBox3.Visible = False
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.I)) And Timer1.Interval > 1 Then
            Timer1.Interval -= 1
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.O)) And Timer1.Interval < 50 Then
            Timer1.Interval += 1
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.F11)) Then
            FormBorderStyle = FormBorderStyle.None
            Location = New Point(0, 0)
            Size = SystemInformation.PrimaryMonitorSize
        ElseIf GetAsyncKeyState(Convert.ToInt32(Keys.F12)) Then
            FormBorderStyle = FormBorderStyle.Sizable
            Location = New Point(0, 0)
            Size = New Point(528, 422)
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.F9)) Then
            levelload()
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.Space)) Then
            If vars.g Then
                If Not vars.bo Then
                    If vars.bomb Then
                        vars.bomb -= 1
                        vars.bo = 1
                        vars.bt = 201
                        vars.bx = Int((vars.x + 8) / 16) * 16
                        vars.by = Int((vars.y + 8) / 16) * 16
                        Timer3.Start()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub bomb(sender As Object, e As EventArgs) Handles Timer1.Tick
        If vars.bo Then
            vars.bt -= 1
            If vars.bt = 200 Then Timer3.Interval = 240
            If vars.bt = 100 Then Timer3.Interval = 120
            If vars.bt = 50 Then Timer3.Interval = 60
            If vars.bt = 25 Then Timer3.Interval = 30
            If vars.bt = 0 Then
                vars.bo = 0
                Timer3.Stop()
                For x = -1 To 1
                    For y = -1 To 1
                        If chkcol(vars.bx + x * 16, vars.by + y * 16) = 2 Then vars.map(Int((vars.bx + x * 16) / 16), Int((vars.by + y * 16) / 16)) = 0
                    Next
                Next
            End If
        End If

    End Sub

    Private Sub Timer1_Tick_1(sender As Object, e As EventArgs) Handles Timer1.Tick
        vars.map2 = New Bitmap(256, 192)
        For x = 0 To 15
            For y = 0 To 11
                Dim gr As Graphics = Graphics.FromImage(vars.map2)
                gr.DrawImage(vars.bg(vars.map(x, y)), x * 16, y * 16, New RectangleF(0, 0, 16, 16), GraphicsUnit.Pixel)
                PictureBox1.Image = vars.map2
            Next
        Next
        TextBox2.Text = "Stats:" + Environment.NewLine + "Has a key: " + Str(vars.key) + Environment.NewLine + "Level clear: " + Str(vars.winre) + Environment.NewLine + "X:" + Str(vars.x) + " Y:" + Str(vars.y) + Environment.NewLine + "Xf:" + Str(Math.Floor(vars.x / 16)) + " Yf:" + Str(Math.Floor(vars.y / 16)) + Environment.NewLine + "TileID:" + Str(chkcol(vars.x, vars.y)) + Environment.NewLine + "L,R,G:" + Str(vars.l) + Str(vars.r) + Str(vars.g) + Environment.NewLine + "U,D,LD:" + Str(vars.u) + Str(vars.d) + Str(vars.ld) + Environment.NewLine + "Coins:" + Str(vars.coin) + Environment.NewLine + "Bombs:" + Str(vars.bomb)
        TextBox3.Text = "Game speed:" + Str(Timer1.Interval) + Environment.NewLine + "Use the keys I and O to adjust."
        If a.a > 3 And vars.g Then a.a = 0
        Graphics.FromImage(vars.map2).DrawImage(vars.sp(a.f * 4 + a.a), vars.x, vars.y, New RectangleF(0, 0, 16, 16), GraphicsUnit.Pixel)
        Graphics.FromImage(vars.map2).DrawImage(vars.bob(a.b), vars.bx + 256 * Not vars.bo, vars.by, New RectangleF(0, 0, 16, 16), GraphicsUnit.Pixel)
    End Sub

    Private Sub timer1_tick_2(sender As Object, e As EventArgs) Handles Timer1.Tick
        If vars.y > 208 Then levelload()
        If chkcol(vars.x + 8, vars.y + 8) = 6 Then
            vars.key = True
            vars.map(Int((vars.x + 8) / 16), Int((vars.y + 8) / 16)) = 0
        End If
        If chkcol(vars.x + 8, vars.y + 8) = 7 And vars.key Then
            vars.lvl += 1
            levelload()
        End If
        If chkcol(vars.x + 8, vars.y + 8) = 5 Then
            vars.coin += 1
            vars.map(Int((vars.x + 8) / 16), Int((vars.y + 8) / 16)) = 0
        End If

        If chkcol(vars.x + 8, vars.y + 8) = 4 Then
            vars.bomb += 1
            vars.map(Int((vars.x + 8) / 16), Int((vars.y + 8) / 16)) = 0
        End If

        vars.l = 1
        vars.r = 1
        vars.g = 0
        vars.u = 1
        vars.d = 1
        vars.ld = 0

        If chkcol(vars.x - 1, vars.y) = 1 Then vars.l = 0
        If chkcol(vars.x - 1, vars.y + 14) = 1 Then vars.l = 0
        If chkcol(vars.x + 16, vars.y) = 1 Then vars.r = 0
        If chkcol(vars.x + 16, vars.y + 14) = 1 Then vars.r = 0
        If chkcol(vars.x + 2, vars.y + 16) = 1 Then vars.g = 1 : vars.d = 0
        If chkcol(vars.x + 13, vars.y + 16) = 1 Then vars.g = 1 : vars.d = 0

        If chkcol(vars.x - 1, vars.y) = 2 Then vars.l = 0
        If chkcol(vars.x - 1, vars.y + 14) = 2 Then vars.l = 0
        If chkcol(vars.x + 16, vars.y) = 2 Then vars.r = 0
        If chkcol(vars.x + 16, vars.y + 14) = 2 Then vars.r = 0
        If chkcol(vars.x + 2, vars.y + 16) = 2 Then vars.g = 1 : vars.d = 0
        If chkcol(vars.x + 13, vars.y + 16) = 2 Then vars.g = 1 : vars.d = 0

        If chkcol(vars.x + 2, vars.y + 16) = 8 Then vars.g = 1 : vars.d = 0
        If chkcol(vars.x + 13, vars.y + 16) = 8 Then vars.g = 1 : vars.d = 0

        If chkcol(vars.x + 2, vars.y - 1) = 1 Then vars.u = 0
        If chkcol(vars.x + 13, vars.y - 1) = 1 Then vars.u = 0
        If chkcol(vars.x + 2, vars.y - 1) = 2 Then vars.u = 0
        If chkcol(vars.x + 13, vars.y - 1) = 2 Then vars.u = 0

        If chkcol(vars.x + 7, vars.y) = 3 Then vars.ld = 1
        If chkcol(vars.x + 7, vars.y + 15) = 3 Then vars.ld = 1
        If vars.ld Then vars.g = 1

        If Not vars.g Then vars.y += 1 : vars.l = 0 : vars.r = 0 : a.f = 5
    End Sub

    Private Function chkcol(x As Integer, y As Integer)
        If valid(x, y) Then
            Return vars.map(Int(x / 16), Int(y / 16))
        Else Return 0
        End If
    End Function
    Private Function valid(x As Integer, y As Integer)
        If x < 0 Then Return 0
        If y < 0 Then Return 0
        If x > 255 Then Return 0
        If y > 191 Then Return 0
        Return 1
    End Function

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If a.b = 1 Then a.b = 0 Else a.b = 1
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        a.a += 1
        If a.a = 4 Then a.a = 0
    End Sub

    Private Sub levelload()
        If vars.lvl = 8 Then Timer1.Stop() : Form2.Show() : Me.Close()
        vars.coin = 0
        vars.key = 0
        vars.bomb = 0
        vars.bo = 0
        If vars.lvl = 0 Then
            Dim levelDataRaw As String() = (My.Resources.level0).Split({Environment.NewLine}, StringSplitOptions.None)
            Dim levelData16x12(12) As String
            For i As Integer = 0 To 11
                levelData16x12(i) = levelDataRaw(i)
            Next
            For y = 0 To 11
                For x = 0 To 15
                    vars.map(x, y) = CInt(Mid(levelData16x12(y), x + 1, 1))
                Next
            Next
            vars.x = CInt(levelDataRaw(12))
            vars.y = CInt(levelDataRaw(13))
            TextBox1.Text = levelDataRaw(14)
        End If
        If vars.lvl = 1 Then
            Dim levelDataRaw As String() = (My.Resources.level1).Split({Environment.NewLine}, StringSplitOptions.None)
            Dim levelData16x12(12) As String
            For i As Integer = 0 To 11
                levelData16x12(i) = levelDataRaw(i)
            Next
            For y = 0 To 11
                For x = 0 To 15
                    vars.map(x, y) = CInt(Mid(levelData16x12(y), x + 1, 1))
                Next
            Next
            vars.x = CInt(levelDataRaw(12))
            vars.y = CInt(levelDataRaw(13))
            TextBox1.Text = levelDataRaw(14)
        End If
        If vars.lvl = 2 Then
            Dim levelDataRaw As String() = (My.Resources.level2).Split({Environment.NewLine}, StringSplitOptions.None)
            Dim levelData16x12(12) As String
            For i As Integer = 0 To 11
                levelData16x12(i) = levelDataRaw(i)
            Next
            For y = 0 To 11
                For x = 0 To 15
                    vars.map(x, y) = CInt(Mid(levelData16x12(y), x + 1, 1))
                Next
            Next
            vars.x = CInt(levelDataRaw(12))
            vars.y = CInt(levelDataRaw(13))
            TextBox1.Text = levelDataRaw(14)
        End If
        If vars.lvl = 3 Then
            Dim levelDataRaw As String() = (My.Resources.level3).Split({Environment.NewLine}, StringSplitOptions.None)
            Dim levelData16x12(12) As String
            For i As Integer = 0 To 11
                levelData16x12(i) = levelDataRaw(i)
            Next
            For y = 0 To 11
                For x = 0 To 15
                    vars.map(x, y) = CInt(Mid(levelData16x12(y), x + 1, 1))
                Next
            Next
            vars.x = CInt(levelDataRaw(12))
            vars.y = CInt(levelDataRaw(13))
            TextBox1.Text = levelDataRaw(14)
        End If
        If vars.lvl = 4 Then
            Dim levelDataRaw As String() = (My.Resources.level4).Split({Environment.NewLine}, StringSplitOptions.None)
            Dim levelData16x12(12) As String
            For i As Integer = 0 To 11
                levelData16x12(i) = levelDataRaw(i)
            Next
            For y = 0 To 11
                For x = 0 To 15
                    vars.map(x, y) = CInt(Mid(levelData16x12(y), x + 1, 1))
                Next
            Next
            vars.x = CInt(levelDataRaw(12))
            vars.y = CInt(levelDataRaw(13))
            TextBox1.Text = levelDataRaw(14)
        End If
        If vars.lvl = 5 Then
            Dim levelDataRaw As String() = (My.Resources.level5).Split({Environment.NewLine}, StringSplitOptions.None)
            Dim levelData16x12(12) As String
            For i As Integer = 0 To 11
                levelData16x12(i) = levelDataRaw(i)
            Next
            For y = 0 To 11
                For x = 0 To 15
                    vars.map(x, y) = CInt(Mid(levelData16x12(y), x + 1, 1))
                Next
            Next
            vars.x = CInt(levelDataRaw(12))
            vars.y = CInt(levelDataRaw(13))
            TextBox1.Text = levelDataRaw(14)
        End If
        If vars.lvl = 6 Then
            Dim levelDataRaw As String() = (My.Resources.level6).Split({Environment.NewLine}, StringSplitOptions.None)
            Dim levelData16x12(12) As String
            For i As Integer = 0 To 11
                levelData16x12(i) = levelDataRaw(i)
            Next
            For y = 0 To 11
                For x = 0 To 15
                    vars.map(x, y) = CInt(Mid(levelData16x12(y), x + 1, 1))
                Next
            Next
            vars.x = CInt(levelDataRaw(12))
            vars.y = CInt(levelDataRaw(13))
            TextBox1.Text = levelDataRaw(14)
        End If
        If vars.lvl = 7 Then
            Dim levelDataRaw As String() = (My.Resources.level7).Split({Environment.NewLine}, StringSplitOptions.None)
            Dim levelData16x12(12) As String
            For i As Integer = 0 To 11
                levelData16x12(i) = levelDataRaw(i)
            Next
            For y = 0 To 11
                For x = 0 To 15
                    vars.map(x, y) = CInt(Mid(levelData16x12(y), x + 1, 1))
                Next
            Next
            vars.x = CInt(levelDataRaw(12))
            vars.y = CInt(levelDataRaw(13))
            TextBox1.Text = levelDataRaw(14)
        End If

    End Sub
End Class