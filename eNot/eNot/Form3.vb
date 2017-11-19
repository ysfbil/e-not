Imports System.Xml
Imports System.Text
Imports System.Security.Cryptography
Imports System.IO

Public Class Form3

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Güncelleme ve kadetme
        If TextBox1.Text.Trim <> "" Then
            Dim doc As XmlDocument = New XmlDocument()
            Dim pth As String = Application.StartupPath & "\kullanicilar.xml"
            Dim pe As New sifreleme
            If IO.File.Exists(pth) Then
                doc.Load(pth)
                Dim root As XmlElement = doc.DocumentElement
                Dim xmlKullanici As XmlElement = root.SelectSingleNode("kullanici[@ad='" & TextBox1.Text.Trim() & "']")
                If Not IsNothing(xmlKullanici) Then
                    'güncelleme yapılacak
                    Dim yenikullanici As XmlElement = xmlKullanici
                    yenikullanici.Attributes("sifre").Value = pe.Encrypting(TextBox2.Text, "")
                    root.ReplaceChild(yenikullanici, xmlKullanici)
                    doc.Save(pth)
                    MsgBox("Şifreniz başarıyla değiştirildi")

                Else
                    'yeni kayıt yapılacak

                    Dim yenikullanici As XmlElement = doc.CreateElement("kullanici")
                    yenikullanici.SetAttribute("ad", TextBox1.Text.Trim)
                    yenikullanici.SetAttribute("sifre", pe.Encrypting(TextBox2.Text.Trim, ""))
                    root.AppendChild(yenikullanici)
                    doc.Save(pth)
                    MsgBox("Yeni kullanıcı başarıyla kaydedildi.")

                End If
            Else
                'xml dosyası oluşturuyoruz ve yeni kullanıcıyı kaydediyoruz
                Dim root As XmlElement = doc.CreateElement("kullanicilar")
                Dim yenikullanici As XmlElement = doc.CreateElement("kullanici")
                yenikullanici.SetAttribute("ad", TextBox1.Text.Trim)
                yenikullanici.SetAttribute("sifre", pe.Encrypting(TextBox2.Text.Trim, ""))
                root.AppendChild(yenikullanici)
                doc.AppendChild(root)
                doc.Save(pth)
                MsgBox("Yeni kullanıcı başarıyla kaydedildi.")
            End If
        Else
            MsgBox("Lütfen bir kullanıcı adı yazınız.")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'kullanıcı silme
        If TextBox1.Text.Trim <> "" Then

            Dim doc As XmlDocument = New XmlDocument()
            Dim pth As String = Application.StartupPath & "\kullanicilar.xml"
            If IO.File.Exists(pth) Then
                doc.Load(pth)
                Dim root As XmlElement = doc.DocumentElement
                Dim xmlKullanici As XmlElement = root.SelectSingleNode("kullanici[@ad='" & TextBox1.Text.Trim() & "']")
                If Not IsNothing(xmlKullanici) Then
                    Dim msgRslt As DialogResult
                    msgRslt = MessageBox.Show(TextBox1.Text & " kullanıcısını silmek istediğinize emin misiniz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If msgRslt = Windows.Forms.DialogResult.Yes Then
                        root.RemoveChild(xmlKullanici)
                        doc.Save(pth)
                        MsgBox("Kullanıcı başarıyla silindi!")
                    End If
                Else
                    MsgBox("Böyle bir kullanıcı bulunamadı!")
                End If
            End If
        Else
            MsgBox("Lütfen bir kullanıcı adı giriniz!")
        End If
    End Sub

   
    Private Sub Form3_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Form1.initializeMe()
    End Sub
End Class

Public Class sifreleme

    Protected defaultKey As String = "x%yz=*p5"

    Public Function Encrypting(ByVal Source As String, ByVal Key As String) As String
        Dim byKey As Byte() = Nothing
        Dim IV As Byte() = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF} 'ilk blok şifrelemesinde gerekli ama kullanmasan da olur.
        If Key.Trim = "" Then Key = defaultKey

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Key.Substring(0, 8))
            Dim des As DESCryptoServiceProvider = New DESCryptoServiceProvider()
            Dim inputByteArray As Byte() = Encoding.UTF8.GetBytes(Source)
            Dim ms As MemoryStream = New MemoryStream()
            Dim ic As ICryptoTransform = des.CreateEncryptor(byKey, IV)
            Dim cs As CryptoStream = New CryptoStream(ms, ic, CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()

            Return Convert.ToBase64String(ms.ToArray)
        Catch ex As Exception
            Return Nothing
        End Try


    End Function


    Public Function Decrypting(ByVal Source As String, ByVal Key As String) As String
        Dim byKey As Byte() = Nothing
        Dim IV As Byte() = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        If Key.Trim = "" Then Key = defaultKey
        Dim inputbyteArray(Source.Length + 1) As Byte

        Try
            byKey = Encoding.UTF8.GetBytes(Key.Substring(0, 8))
            Dim des As DESCryptoServiceProvider = New DESCryptoServiceProvider()
            inputbyteArray = Convert.FromBase64String(Source)
            Dim ms As MemoryStream = New MemoryStream()
            Dim ic As ICryptoTransform = des.CreateDecryptor(byKey, IV)
            Dim cs As CryptoStream = New CryptoStream(ms, ic, CryptoStreamMode.Write)
            cs.Write(inputbyteArray, 0, inputbyteArray.Length)
            cs.FlushFinalBlock()
            Dim enc As Encoding = Encoding.UTF8
            Return enc.GetString(ms.ToArray)
        Catch ex As Exception
            Return Nothing
        End Try

        Return Source
    End Function
End Class