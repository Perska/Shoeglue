Public Class Form2
    Public Class a
        Public Shared a = 0
    End Class
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        a.a += 1
        If a.a = 1 Then TextBox1.Text = "You win nothing!" : Button1.Text = "Why?"
        If a.a = 2 Then TextBox1.Text = "Well... I have no idea..." : Button1.Text = "Did you have anything to tell me?"
        If a.a = 3 Then TextBox1.Text = "Oh, yes." : Button1.Text = "Okie dokie!"
        If a.a = 4 Then TextBox1.Text = "I released the PTC version of Shoeglue nearly a month ago." : Button1.Text = "*pretend to listen*"
        If a.a = 5 Then TextBox1.Text = "I had a lot of fun in making it. Even those small bugs couldn't ruin that." : Button1.Text = "Okay."
        If a.a = 6 Then TextBox1.Text = "And since SmileBASIC's European release is still not confirmed, I thought that I should make more games for Windows." : Button1.Text = "Zzzz... What? Yes!"
        If a.a = 7 Then TextBox1.Text = "What do you think?" : Button1.Text = "Uhh... there is only one button."
        If a.a = 8 Then TextBox1.Text = "I know. So vote in the poll #34." : Button1.Text = "Oh, I see."
        If a.a = 9 Then TextBox1.Text = "So. I hope you had " + Chr(34) + "fun" + Chr(34) + " playing this PC port!" : Button1.Text = ":^)"
        If a.a = 10 Then TextBox1.Text = "So, yeah. Bye!" : Button1.Text = "Bye!"
        If a.a = 11 Then Me.Close()
    End Sub
End Class