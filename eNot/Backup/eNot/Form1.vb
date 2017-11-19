Imports System.Xml

Public Class Form1


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        WebBrowser1.Navigate(TextBox1.Text)
    End Sub


    Private Sub WebBrowser1_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles WebBrowser1.Navigated
        TextBox1.Text = WebBrowser1.Url.AbsoluteUri

    End Sub


    Private Sub KaydetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KaydetToolStripMenuItem.Click
        If Not IsNothing(WebBrowser1.Document) Then
            Dim gk As HtmlElement = WebBrowser1.Document.GetElementById("Gv_kod1_txtGuvenlikKod")
            If Not IsNothing(gk) AndAlso gk.GetAttribute("value").Trim <> "" Then
                Dim str As String = ""
                Dim i As Integer = 1

                Dim inputs As HtmlElementCollection = WebBrowser1.Document.GetElementsByTagName("input")

                For Each inp As HtmlElement In inputs
                    If inp.GetAttribute("type").ToLower = "text" Then
                        If inp.GetAttribute("id").ToLower.Contains("dglistem") Then
                            str &= i & ";İsimsiz;" & inp.GetAttribute("value") & vbNewLine
                            i += 1
                        End If
                    End If
                Next


                Dim f As New IO.StreamWriter(Application.StartupPath & "\notlar.csv", False, System.Text.Encoding.UTF8)
                f.Write(str)
                f.Close()


                'Kaydeti çalıştırıyoruz
                WebBrowser1.Document.InvokeScript("AlanKontrolveKayit")
            Else
                MessageBox.Show("Lütfen öncelikle güvenlik kodunu girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub


    Private Sub GeriAlToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GeriAlToolStripMenuItem.Click
        geriAl("notlar")
    End Sub

    Private Sub geriAl(ByVal dosyaAdi As String)
        If IO.File.Exists(Application.StartupPath & "\" & dosyaAdi & ".csv") Then
            Dim f As New IO.StreamReader(Application.StartupPath & "\" & dosyaAdi & ".csv", System.Text.Encoding.UTF8)
            Dim notlar() As String = f.ReadToEnd.Split(vbNewLine)
            f.Close()

            Dim i As Integer = 0
            Dim inputs As HtmlElementCollection = WebBrowser1.Document.GetElementsByTagName("input")

            For Each inp As HtmlElement In inputs
                If inp.GetAttribute("type").ToLower = "text" Then
                    If inp.GetAttribute("id").ToLower.Contains("dglistem") Then
                        If (i < notlar.Length AndAlso Not String.IsNullOrEmpty(notlar(i).Trim) AndAlso Not IsNothing(notlar(i).Split(";")(2))) Then _
                            inp.SetAttribute("value", notlar(i).Split(";")(2))
                        i += 1
                    End If
                End If
            Next
        Else
            MessageBox.Show("Program klasöründe böyle bir dosya yok!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub ÇıkışToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ÇıkışToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub kullaniciClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ÇıkışToolStripMenuItem.Click
        Dim pe As New sifreleme
        Dim doc As XmlDocument = New XmlDocument()
        Dim pth As String = Application.StartupPath & "\kullanicilar.xml"
        If IO.File.Exists(pth) Then
            doc.Load(pth)
            Dim root As XmlElement = doc.DocumentElement
            Dim xmlKullanici As XmlElement = root.SelectSingleNode("kullanici[@ad='" & sender.Text.Trim() & "']")
            If Not IsNothing(xmlKullanici) AndAlso Not IsNothing(WebBrowser1) Then
                Dim ktextbox As HtmlElement = WebBrowser1.Document.GetElementById("txtKullaniciAd")
                Dim stextbox As HtmlElement = WebBrowser1.Document.GetElementById("txtSifre")
                If Not IsNothing(ktextbox) AndAlso Not IsNothing(stextbox) Then
                    ktextbox.SetAttribute("value", xmlKullanici.Attributes("ad").Value)
                    stextbox.SetAttribute("value", pe.Decrypting(xmlKullanici.Attributes("sifre").Value, ""))
                End If
            End If
        End If
    End Sub


    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WebBrowser1.Navigate("https://eokul.meb.gov.tr/") 'Application.StartupPath & "/deneme.htm") 'Deneme yapmak için yandaki adresi kullanın 
        initializeMe()
    End Sub

    Private Sub YardımToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YardımToolStripMenuItem.Click
        Form2.Show()
    End Sub


    Private Sub SayfaYapısıToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SayfaYapısıToolStripMenuItem.Click
        WebBrowser1.ShowPageSetupDialog()
    End Sub

    Private Sub YazdırToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YazdırToolStripMenuItem.Click
        WebBrowser1.ShowPrintDialog()
    End Sub

    Private Sub ÖnizlemeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ÖnizlemeToolStripMenuItem.Click
        WebBrowser1.ShowPrintPreviewDialog()
    End Sub

    Private Sub SayfayıKaydetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SayfayıKaydetToolStripMenuItem.Click
        WebBrowser1.ShowSaveAsDialog()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        WebBrowser1.GoBack()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        WebBrowser1.GoForward()
    End Sub


    Private Sub TextBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = 13 Then WebBrowser1.Navigate(TextBox1.Text)
    End Sub

    Private Sub WebBrowser1_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles WebBrowser1.NewWindow
        If WebBrowser1.Url.AbsoluteUri.Contains("reportsubmitter") Then
            WebBrowser1.GoBack()
        End If

    End Sub

    Private Sub WebBrowser1_ProgressChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserProgressChangedEventArgs) Handles WebBrowser1.ProgressChanged
        If e.CurrentProgress = e.MaximumProgress Then
            ProgressBar1.Value = 0
        End If

        If e.CurrentProgress > 0 And e.MaximumProgress > 0 Then
            ProgressBar1.Value = e.CurrentProgress * 100 / e.MaximumProgress
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        WebBrowser1.Refresh()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Not IsNothing(WebBrowser1) AndAlso Not IsNothing(WebBrowser1.Document) Then
            Dim tms As HtmlElementCollection = WebBrowser1.Document.GetElementsByTagName("input")
            Dim ks As String = ""
            Dim dk As String = ""
            Dim sn As String = ""
            If tms.Count > 0 Then
                For Each tm As HtmlElement In tms
                    If Not IsNothing(tm.Id) Then
                        If tm.Id.ToLower.Contains("txtbaglantisonu") Then
                            ks = tm.GetAttribute("value")
                            Label1.Text = "Kalan Süre: " & ks

                            'Containerı Kırmızı-Gri Yapıyoğ
                            If Not String.IsNullOrEmpty(ks) Then
                                dk = ks.Split(":")(0)
                                sn = ks.Split(":")(1)
                                If CInt(dk) = 0 And CInt(sn) <= 30 Then
                                    If sn Mod 2 = 0 Then
                                        SplitContainer1.Panel1.BackColor = Color.Red
                                        SplitContainer1.Panel2.BackColor = Color.Red
                                        Label1.BackColor = Color.White
                                    Else
                                        SplitContainer1.Panel1.BackColor = Color.FromKnownColor(KnownColor.Control)
                                        SplitContainer1.Panel2.BackColor = Color.FromKnownColor(KnownColor.Control)
                                        Label1.BackColor = Color.FromKnownColor(KnownColor.Control)
                                    End If
                                Else
                                    SplitContainer1.Panel1.BackColor = Color.FromKnownColor(KnownColor.Control)
                                    SplitContainer1.Panel2.BackColor = Color.FromKnownColor(KnownColor.Control)
                                    Label1.BackColor = Color.FromKnownColor(KnownColor.Control)
                                End If
                            End If

                            Exit For
                        End If
                    End If
                Next
            End If
        End If
    End Sub



    Private Sub EokulToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EokulToolStripMenuItem.Click
        WebBrowser1.Navigate("https://e-okul.meb.gov.tr/")
    End Sub

    Private Sub EokulToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EokulToolStripMenuItem1.Click
        WebBrowser1.Navigate("https://eokul.meb.gov.tr/")
    End Sub


    Private Sub BilgiGirişiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BilgiGirişiToolStripMenuItem.Click
        Form3.Show()
    End Sub

    Public Sub initializeMe()
        Dim doc As XmlDocument = New XmlDocument()
        Dim pth As String = Application.StartupPath & "\kullanicilar.xml"
        If IO.File.Exists(pth) Then
            doc.Load(pth)
            Dim root As XmlElement = doc.DocumentElement
            Dim xmlKullanici As XmlNodeList = root.ChildNodes
            For Each kullanici As XmlNode In xmlKullanici
                If Not toolStripItemKontrol(kullanici.Attributes("ad").Value) Then
                    OtomatikGirişToolStripMenuItem.DropDownItems.Add(kullanici.Attributes("ad").Value, Nothing, AddressOf Me.kullaniciClick)
                End If
            Next
        End If
    End Sub

    Protected Function toolStripItemKontrol(ByVal t As String) As Boolean
        For Each itm As ToolStripItem In OtomatikGirişToolStripMenuItem.DropDownItems
            If itm.Text = t Then
                Return True
                Exit For
            End If
        Next
    End Function

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        WebBrowser1.Stop()
    End Sub

   
    Private Sub RaporAlToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RaporAlToolStripMenuItem.Click
        Dim frms As HtmlElementCollection = WebBrowser1.Document.Forms
        For Each frm As HtmlElement In frms
            If frm.GetAttribute("name") = "rapor_submit_form" Then
                frm.SetAttribute("target", "_self")
                frm.InvokeMember("submit")
                Exit For
            End If
        Next
    End Sub

    Private Sub OffLineNotGirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffLineNotGirToolStripMenuItem.Click
        Form4.Show()
    End Sub

    Private Sub DosyadanGerialToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DosyadanGerialToolStripMenuItem.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        If IO.Path.GetExtension(OpenFileDialog1.FileName) = ".csv" Then
            Dim dr As DialogResult = MsgBox("Seçtiğiniz dosyadaki notları geri-almak/aktarmak istiyor musunuz?", MsgBoxStyle.YesNo, "İşlem Onayı...")
            If dr = Windows.Forms.DialogResult.Yes Then geriAl(IO.Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName))
        End If
    End Sub
End Class
