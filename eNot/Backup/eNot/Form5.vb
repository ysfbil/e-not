Public Class Form5

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Drawing.Point(Form4.Location.X + Form4.Width, Form4.Location.Y)

        tabloDoldur()

    End Sub

    Public Sub tabloDoldur()

        DataGridView1.Rows.Clear()

        For Each Inf As String In Form4.degerler
            If Not String.IsNullOrEmpty(Inf.Trim) Then
                DataGridView1.Rows.Add(Inf.Split(";"))
            End If
        Next

        Form4.DataGridView1.Focus()
    End Sub
End Class