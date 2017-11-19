<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form4
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.AçToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.KaydetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FarklıKaydetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.KarşılaştırmaListesiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SeçimiKaldırToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TümünüSilToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OrtalamaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AdSoyad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Notu = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.MenuStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AçToolStripMenuItem, Me.ToolStripMenuItem1, Me.KaydetToolStripMenuItem, Me.FarklıKaydetToolStripMenuItem, Me.KarşılaştırmaListesiToolStripMenuItem, Me.TümünüSilToolStripMenuItem, Me.OrtalamaToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(607, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'AçToolStripMenuItem
        '
        Me.AçToolStripMenuItem.Name = "AçToolStripMenuItem"
        Me.AçToolStripMenuItem.Size = New System.Drawing.Size(33, 20)
        Me.AçToolStripMenuItem.Text = "Aç"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        'KaydetToolStripMenuItem
        '
        Me.KaydetToolStripMenuItem.Name = "KaydetToolStripMenuItem"
        Me.KaydetToolStripMenuItem.Size = New System.Drawing.Size(55, 20)
        Me.KaydetToolStripMenuItem.Text = "Kaydet"
        '
        'FarklıKaydetToolStripMenuItem
        '
        Me.FarklıKaydetToolStripMenuItem.Name = "FarklıKaydetToolStripMenuItem"
        Me.FarklıKaydetToolStripMenuItem.Size = New System.Drawing.Size(86, 20)
        Me.FarklıKaydetToolStripMenuItem.Text = "Farklı Kaydet"
        '
        'KarşılaştırmaListesiToolStripMenuItem
        '
        Me.KarşılaştırmaListesiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SeçimiKaldırToolStripMenuItem})
        Me.KarşılaştırmaListesiToolStripMenuItem.Name = "KarşılaştırmaListesiToolStripMenuItem"
        Me.KarşılaştırmaListesiToolStripMenuItem.Size = New System.Drawing.Size(55, 20)
        Me.KarşılaştırmaListesiToolStripMenuItem.Text = "Sınıflar"
        '
        'SeçimiKaldırToolStripMenuItem
        '
        Me.SeçimiKaldırToolStripMenuItem.Name = "SeçimiKaldırToolStripMenuItem"
        Me.SeçimiKaldırToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SeçimiKaldırToolStripMenuItem.Text = "Seçimi Kaldır"
        '
        'TümünüSilToolStripMenuItem
        '
        Me.TümünüSilToolStripMenuItem.Name = "TümünüSilToolStripMenuItem"
        Me.TümünüSilToolStripMenuItem.Size = New System.Drawing.Size(80, 20)
        Me.TümünüSilToolStripMenuItem.Text = "Tümünü Sil"
        '
        'OrtalamaToolStripMenuItem
        '
        Me.OrtalamaToolStripMenuItem.Name = "OrtalamaToolStripMenuItem"
        Me.OrtalamaToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.OrtalamaToolStripMenuItem.Text = "İstatistik"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.AdSoyad, Me.Notu})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 24)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(607, 464)
        Me.DataGridView1.TabIndex = 1
        '
        'No
        '
        DataGridViewCellStyle1.NullValue = Nothing
        Me.No.DefaultCellStyle = DataGridViewCellStyle1
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        '
        'AdSoyad
        '
        Me.AdSoyad.HeaderText = "Ad Soyad"
        Me.AdSoyad.Name = "AdSoyad"
        Me.AdSoyad.Width = 250
        '
        'Notu
        '
        DataGridViewCellStyle2.NullValue = Nothing
        Me.Notu.DefaultCellStyle = DataGridViewCellStyle2
        Me.Notu.HeaderText = "Notu"
        Me.Notu.Name = "Notu"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "csv"
        Me.OpenFileDialog1.Filter = "csv dosyası|*.csv|Tüm Dosyalar|*.*"
        '
        'Form4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(607, 488)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form4"
        Me.Text = "Çevrim Dışı Not Gir"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KaydetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents FarklıKaydetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TümünüSilToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AçToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents KarşılaştırmaListesiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SeçimiKaldırToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AdSoyad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Notu As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrtalamaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
