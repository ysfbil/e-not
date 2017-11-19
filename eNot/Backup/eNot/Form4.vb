Imports System.IO
Imports System.Xml

Public Class Form4

    Dim dad As String

    Private Sub KaydetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KaydetToolStripMenuItem.Click

        If String.IsNullOrEmpty(dad) Then
            dad = "notlar"
        End If

        Dim msgbx As MsgBoxResult = MessageBox.Show(dad & " dosyası değiştirilecek. Onaylıyor musunuz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If msgbx = MsgBoxResult.Yes Then
            kaydet(dad)
        End If

    End Sub

    Private Sub kaydet(ByVal dosyaAdi As String)

        Dim notlar As String = ""
        Dim girenler As New ArrayList

        For Each r As DataGridViewRow In DataGridView1.Rows
            If Not String.IsNullOrEmpty(r.Cells(0).Value) Then
                girenler.Add(r.Cells(0).Value)
            End If
        Next

        If Not IsNothing(degerler) Then
            For Each girmeyen As Object In girmeyenler(girenler.ToArray(GetType(Integer)))
                DataGridView1.Rows.Add(girmeyen(0), girmeyen(1), "G")
            Next
        End If

        DataGridView1.EndEdit()
        DataGridView1.Columns(0).ValueType = GetType(Integer)

        DataGridView1.Sort(DataGridView1.Columns(0), System.ComponentModel.ListSortDirection.Ascending)

        For Each r As DataGridViewRow In DataGridView1.Rows
            If Not String.IsNullOrEmpty(r.Cells(0).Value) Then
                notlar &= r.Cells(0).Value & ";" & r.Cells(1).Value & ";" & r.Cells(2).Value & vbNewLine
            End If

        Next


        If Not String.IsNullOrEmpty(notlar) Then
            Dim f As New IO.StreamWriter(Application.StartupPath & "\" & dosyaAdi & ".csv", False, System.Text.Encoding.UTF8)
            f.Write(notlar)
            f.Close()
            MsgBox("Tüm notlar " & dosyaAdi & ".csv dosyasına kaydedildi!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Kaydetme Başarılı")
        End If
    End Sub

    Private Function girmeyenler(ByVal girenler() As Integer) As Object()
        If (Not IsNothing(degerler)) Then
            Dim nolar(degerler.Length) As Object
            Dim i As Integer = 0
            Dim sonuc As New ArrayList
            Dim tmp As Integer

            For Each ogr As String In degerler
                If Integer.TryParse(ogr.Split(";")(0).Trim, tmp) Then
                    nolar(i) = New Object() {tmp, ogr.Split(";")(1).Trim}
                    i += 1
                End If
            Next

            For Each No As Object In nolar
                If (Not IsNothing(No)) AndAlso Array.IndexOf(girenler, No(0)) = -1 AndAlso No(0) <> 0 Then
                    sonuc.Add(No)
                End If
            Next

            Return sonuc.ToArray()
        Else
            Return Nothing
        End If
    End Function




    Private Sub DataGridView1_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If (e.ColumnIndex = 0) AndAlso e.RowIndex <> -1 Then
            Dim deger As Integer
            If Integer.TryParse(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString.Trim, deger) Then
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = deger
                If values.ContainsKey(deger) Then DataGridView1.Rows(e.RowIndex).Cells(1).Value = values(deger) 'karşılaştırma listesindeki değeri aktarıyoruz
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
            Else

                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Red

            End If
        ElseIf (e.ColumnIndex = 2) AndAlso e.RowIndex <> -1 Then
            Dim deger2 As Integer
            If (Integer.TryParse(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString.Trim, deger2) _
              AndAlso deger2 <= 100 AndAlso deger2 >= 0) Then
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = deger2
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
            Else
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Red

            End If
        End If
    End Sub


    Private Sub DataGridView1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.Control Then 'EXCELL'DEN YAPIŞTIRMAK İÇİN
            If e.KeyCode = Keys.V Then
                If Clipboard.ContainsText Then
                    Dim data() As String = Clipboard.GetText.Replace(vbNewLine, vbTab).Split(vbTab)
                    If DataGridView1.SelectedCells.Count > 0 Then
                        Dim bas As Integer = DataGridView1.SelectedCells(0).RowIndex
                        Dim i As Integer = 0
                        Dim satirSay As Integer = CInt((data.Length - 1) / 3)

                        If DataGridView1.Rows.Count - DataGridView1.SelectedCells(0).RowIndex < satirSay Then
                            DataGridView1.Rows.Add(satirSay - (DataGridView1.Rows.Count - DataGridView1.SelectedCells(0).RowIndex) + 1)
                        ElseIf DataGridView1.CurrentRow.Index = DataGridView1.Rows.Count - 1 AndAlso satirSay = 1 Then
                            DataGridView1.Rows.Add()
                        End If

                        For Each d As String In data
                            If data.Length = 1 Then
                                DataGridView1.SelectedCells(0).Value = d
                                Exit For
                            End If
                            If 3 * i < data.Length And DataGridView1.Rows.Count > bas Then DataGridView1.Rows(bas).Cells(0).Value = data(3 * i) 'no hücresi
                            If 3 * i + 1 < data.Length And DataGridView1.Rows.Count > bas Then DataGridView1.Rows(bas).Cells(1).Value = data(3 * i + 1) 'isim hücresi
                            If 3 * i + 2 < data.Length And DataGridView1.Rows.Count > bas Then DataGridView1.Rows(bas).Cells(2).Value = data(3 * i + 2) 'not hücresini giriyoruz
                            i += 1
                            bas += 1
                        Next
                    End If
                End If
            End If
        ElseIf e.KeyCode = Keys.Tab Then
            If DataGridView1.CurrentCell.RowIndex = DataGridView1.Rows.Count - 1 _
                AndAlso DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Columns.Count - 1 Then 'TAB İLE SONDA İSEK YENİ SATIR EKLEME
                Dim ri As Integer = DataGridView1.CurrentCell.RowIndex
                Dim ci As Integer = DataGridView1.CurrentCell.ColumnIndex

                DataGridView1.Rows.Add()
                DataGridView1.CurrentCell = DataGridView1.Rows(ri).Cells(ci)
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 1 AndAlso Not IsNothing(degerler) Then 'tab'a basınca son sütüna gitsin
                Dim ri As Integer = DataGridView1.CurrentCell.RowIndex
                DataGridView1.CurrentCell = DataGridView1.Rows(ri).Cells(2)
            End If
        ElseIf e.KeyCode = Keys.Right AndAlso DataGridView1.CurrentCell.ColumnIndex = 1 AndAlso Not IsNothing(degerler) Then 'sağ oka basınca son sütüna gitsin
            Dim ri As Integer = DataGridView1.CurrentCell.RowIndex
            DataGridView1.CurrentCell = DataGridView1.Rows(ri).Cells(2)
        ElseIf e.KeyCode = Keys.Enter AndAlso DataGridView1.CurrentCell.RowIndex < DataGridView1.Rows.Count Then 'enter'e basınca alt satıra geçsin
            Dim ri As Integer = DataGridView1.CurrentCell.RowIndex
            DataGridView1.CurrentCell = DataGridView1.Rows(ri).Cells(0)
        End If
    End Sub


    Private Sub FarklıKaydetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FarklıKaydetToolStripMenuItem.Click
        Dim da As String = InputBox("Dosya Adını Giriniz?", "Dosya Adı", "notlar")
        If Not String.IsNullOrEmpty(da) Then
            dad = da
            kaydet(da)
        End If
    End Sub

    Private Sub TümünüSilToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TümünüSilToolStripMenuItem.Click
        Dim dr As DialogResult = MsgBox("Tüm listeyi silmek istediğinize emin misiniz?", MsgBoxStyle.YesNo, "Uyarı!!!")
        If dr = Windows.Forms.DialogResult.Yes Then
            DataGridView1.Rows.Clear()
            dad = ""
        End If
       
    End Sub

    Private Sub AçToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AçToolStripMenuItem.Click
        If (DataGridView1.Rows.Count > 1 AndAlso DataGridView1.Rows(0).Cells(0).Value.ToString.Trim <> "") Then
            Dim dr As DialogResult = MsgBox("Listedeki tüm bilgiler silinecek. Emin misiniz?", MsgBoxStyle.YesNo, "Uyarı!!!")
            If dr = Windows.Forms.DialogResult.Yes Then
                DataGridView1.Rows.Clear()
                OpenFileDialog1.ShowDialog()
            End If
        Else
            OpenFileDialog1.ShowDialog()
        End If
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        If IO.Path.GetExtension(OpenFileDialog1.FileName) = ".csv" Then
            Dim dosyaAdi As String = IO.Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName)
            If IO.File.Exists(Application.StartupPath & "\" & dosyaAdi & ".csv") Then
                Dim f As New IO.StreamReader(Application.StartupPath & "\" & dosyaAdi & ".csv", System.Text.Encoding.UTF8)
                Dim notlar() As String = f.ReadToEnd.Split(vbNewLine)
                Dim nosu As Integer
                Dim notu As Integer
                Dim notStr As String
                Dim adsoy As String

                For Each Inf As String In notlar
                    If Not String.IsNullOrEmpty(Inf.Trim) Then
                        Integer.TryParse(Inf.Split(";")(0).Trim, nosu)
                        adsoy = Inf.Split(";")(1).Trim
                        notStr = Inf.Split(";")(2).Trim
                        If Not Integer.TryParse(notStr, notu) Then
                            DataGridView1.Rows.Add(nosu, adsoy, notStr)
                        Else
                            DataGridView1.Rows.Add(nosu, adsoy, notu)
                        End If

                    End If
                Next

                dad = dosyaAdi
                f.Close()
            End If

        End If


    End Sub

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Directory.Exists(Application.StartupPath & "\SINIFLAR\") Then
            sinifListeleri()
        Else
            Directory.CreateDirectory(Application.StartupPath & "\SINIFLAR\")
            sinifListeleri()
        End If
    End Sub

    Private Sub sinifListeleri()
        Dim fls() As String = IO.Directory.GetFiles(Application.StartupPath & "\SINIFLAR\", "*.csv")
        For Each file As String In fls
            KarşılaştırmaListesiToolStripMenuItem.DropDownItems.Add(IO.Path.GetFileNameWithoutExtension(file), Nothing, AddressOf karsilastir)
        Next
    End Sub

    Dim values As New Dictionary(Of String, String)
    Public degerler() As String

    Private Sub karsilastir(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If IO.File.Exists(Application.StartupPath & "\SINIFLAR\" & sender.Text & ".csv") Then
            Dim f As New IO.StreamReader(Application.StartupPath & "\SINIFLAR\" & sender.Text.ToString & ".csv", System.Text.Encoding.UTF8)
            values.Clear()
            degerler = f.ReadToEnd.Split(vbNewLine)
            f.Close()

            Dim kyt() As String

            For Each kayit As String In degerler
                If Not String.IsNullOrEmpty(kayit.Trim) Then
                    kyt = kayit.Split(";")
                    values.Add(kyt(0).Trim, kyt(1).Trim)
                End If
            Next

            'seçileni kırmızı yapıyorğ
            For Each t As ToolStripItem In KarşılaştırmaListesiToolStripMenuItem.DropDownItems
                t.BackColor = Nothing
            Next

            Dim tsp As ToolStripItem = sender
            tsp.BackColor = Color.Red

            If Not IsNothing(degerler) Then Form5.Show()
            Form5.tabloDoldur()
        End If
    End Sub

  

    Private Sub SeçimiKaldırToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeçimiKaldırToolStripMenuItem.Click
        degerler = Nothing
        For Each t As ToolStripItem In KarşılaştırmaListesiToolStripMenuItem.DropDownItems
            t.BackColor = Nothing
        Next

        Form5.Close()
    End Sub

    Private so As SortOrder = SortOrder.Ascending

    Private Sub DataGridView1_SortCompare(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewSortCompareEventArgs) Handles DataGridView1.SortCompare
        If e.Column.Index = 2 Then
            If so = SortOrder.Ascending Then so = SortOrder.Descending Else so = SortOrder.Ascending
            DataGridView1.Sort(New RowComparer(so))
            e.Handled = True
        End If
    End Sub

    Private Class RowComparer
        Implements System.Collections.IComparer

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
            Implements System.Collections.IComparer.Compare

            Dim DataGridViewRow1 As DataGridViewRow = CType(x, DataGridViewRow)
            Dim DataGridViewRow2 As DataGridViewRow = CType(y, DataGridViewRow)

            ' Try to sort based on the Last Name column.
            Dim CompareResult As Integer = -1
            Dim tmp1 As Integer = 0
            Dim tmp2 As Integer = 0
            If Not IsNothing(DataGridViewRow1.Cells(2).Value) Then Integer.TryParse(DataGridViewRow1.Cells(2).Value.ToString.Trim, tmp1)
            If Not IsNothing(DataGridViewRow2.Cells(2).Value) Then Integer.TryParse(DataGridViewRow2.Cells(2).Value.ToString.Trim, tmp2)
            If tmp1 > tmp2 Then CompareResult = 1

            Return CompareResult * sortOrderModifier
        End Function
    End Class

    Private Sub OrtalamaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrtalamaToolStripMenuItem.Click
        If DataGridView1.Rows.Count > 0 Then
            Dim gTop As Integer = 0
            Dim oSay As Integer = 0
            Dim cNot As Integer = 0
            Dim gSay As Integer = 0
            Dim kSay As Integer = 0
            Dim notlar As New ArrayList
            Dim ortalama As Double

            For Each r As DataGridViewRow In DataGridView1.Rows
                If Integer.TryParse(r.Cells(2).Value, cNot) Then
                    oSay += 1
                    gTop += cNot
                    notlar.Add(cNot)

                    If cNot >= 50 Then
                        gSay += 1
                    End If

                ElseIf Not IsNothing(r.Cells(2).Value) AndAlso r.Cells(2).Value.ToString.ToUpper = "G" Then
                    kSay += 1
                End If
            Next

            If oSay > 0 Then
                ortalama = Math.Round(gTop / oSay, 2)

                MsgBox("Genel Ortalama: " & ortalama & vbNewLine & "Standart Sapma: " & stdSapma(notlar.ToArray(GetType(Integer)), ortalama) & vbNewLine & _
               "Zayıf Sayısı: " & oSay - gSay & vbNewLine & "Geçer Sayısı: " & gSay & vbNewLine & "Girmeyen Sayısı: " & kSay & _
               vbNewLine & "Toplam: " & oSay + kSay, MsgBoxStyle.Information)
            End If
           
        End If
    End Sub

    Private Function stdSapma(ByVal notlar As Integer(), ByVal ortalama As Double) As Double
        Dim tKiKare As Double = 0

        For Each onot As Integer In notlar
            tKiKare += Math.Pow(onot - ortalama, 2)
        Next

        Return Math.Round(Math.Pow(tKiKare / notlar.Length, 0.5), 2)
    End Function

End Class

