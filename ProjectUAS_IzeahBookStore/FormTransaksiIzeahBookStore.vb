Imports System.Data.Odbc
Public Class FormTransaksiIzeahBookStore
    Dim TglMySQL As String
    Sub KondisiAwal()
        LBLNamaPenerbit.Text = ""
        LBLTelp.Text = ""
        LBLTanggal.Text = Today
        LBLAdmin.Text = Menu_Utama.ToolStripStatusLabel4.Text
        LBLKembali.Text = ""
        LBLJudulBuku.Text = ""
        LBLHargaBuku.Text = ""
        TextBox3.Text = ""
        TextBox3.Enabled = False
        LBLItem.Text = ""
        Call MunculKodePenerbit()
        Call MunculKodeBuku()
        Call NomorOtomatis()
        Call BuatKolom()
        Label9.Text = ""
        TextBox1.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Text = ""
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        LBLJam.Text = TimeOfDay
    End Sub

    Private Sub FormTranstransaksi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Sub MunculKodePenerbit()
        Call koneksi()
        ComboBox1.Items.Clear()
        CMD = New OdbcCommand("Select * From penerbit", konek)
        DR = CMD.ExecuteReader
        Do While DR.Read
            ComboBox1.Items.Add(DR.Item(0))
        Loop
    End Sub

    Sub MunculKodeBuku()
        Call koneksi()
        ComboBox2.Items.Clear()
        CMD = New OdbcCommand("Select * From buku", konek)
        DR = CMD.ExecuteReader
        Do While DR.Read
            ComboBox2.Items.Add(DR.Item(0))
        Loop
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call koneksi()
        CMD = New OdbcCommand("Select * From penerbit where id ='" & ComboBox1.Text & "'", konek)
        DR = CMD.ExecuteReader
        DR.Read()
        If DR.HasRows Then
            LBLNamaPenerbit.Text = DR!nama
            LBLTelp.Text = DR!telp
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Call koneksi()
        CMD = New OdbcCommand("Select * From buku where id ='" & ComboBox2.Text & "'", konek)
        DR = CMD.ExecuteReader
        DR.Read()
        If DR.HasRows Then
            LBLJudulBuku.Text = DR.Item("judul")
            LBLHargaBuku.Text = DR.Item("harga")
            TextBox3.Enabled = True
        End If
    End Sub

    Sub NomorOtomatis()
        Call koneksi()
        CMD = New OdbcCommand("Select * from transaksi where id in (Select max(id) from transaksi)", konek)
        Dim UrutanKode As String
        Dim Hitung As Long
        DR = CMD.ExecuteReader
        DR.Read()
        If Not DR.HasRows Then
            UrutanKode = "T" + Format(Now, "yyMMdd") + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(DR.GetString(0), 9) + 1
            UrutanKode = "T" + Format(Now, "yyMMdd") + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        LBLNotransaksi.Text = UrutanKode
    End Sub

    Sub BuatKolom()
        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add("Kode Buku", "Kode Buku")
        DataGridView1.Columns.Add("Judul Buku", "Judul Buku")
        DataGridView1.Columns.Add("Harga Buku", "Harga Buku")
        DataGridView1.Columns.Add("Quantity", "Quantity")
        DataGridView1.Columns.Add("Subtotal", "Subtotal")
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            CMD = New OdbcCommand("Select * From buku where id ='" & ComboBox2.Text & "'", konek)
            DR = CMD.ExecuteReader
            DR.Read()
            If Not DR.HasRows Then
                MsgBox("Kode Buku Tidak Ada")
            Else
                ComboBox2.Text = DR.Item("id")
                LBLJudulBuku.Text = DR.Item("judul")
                LBLHargaBuku.Text = DR.Item("harga")
                TextBox3.Enabled = True
            End If
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If LBLJudulBuku.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Silahkan Masukan Kode Buku Dan Tekan Enter")
        Else
            DataGridView1.Rows.Add(New String() {ComboBox2.Text, LBLJudulBuku.Text, LBLHargaBuku.Text, TextBox3.Text, Val(LBLHargaBuku.Text) * Val(TextBox3.Text)})
            Call RumusSubTotal()
            ComboBox2.Text = ""
            LBLJudulBuku.Text = ""
            LBLHargaBuku.Text = ""
            TextBox3.Text = ""
            TextBox3.Enabled = False
            Call RumusCariItem()
        End If
    End Sub
    Sub RumusSubTotal()
        Dim hitung As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            hitung = hitung + DataGridView1.Rows(i).Cells(4).Value
            Label9.Text = hitung
        Next
    End Sub
    Sub RumusCariItem()
        Dim HitungItem As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            HitungItem = HitungItem + DataGridView1.Rows(i).Cells(3).Value
            LBLItem.Text = HitungItem
        Next
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If Val(TextBox1.Text) < Val(Label9.Text) Then
                MsgBox("Pembayaran Kurang!")
            ElseIf Val(TextBox1.Text) = Val(Label9.Text) Then
                LBLKembali.Text = 0
            ElseIf Val(TextBox1.Text) > Val(Label9.Text) Then
                LBLKembali.Text = Val(TextBox1.Text) - Val(Label9.Text)
                Button1.Focus()
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If LBLKembali.Text = "" Or LBLNamaPenerbit.Text = "" Or Label9.Text = "" Then
            MsgBox("Transaksi Tidak Ada, Silahkan Lakukan Transaksi Terlebih Dahulu")
        Else
            Dim CekStockBuku As String = "Select * From buku where stock <=3"
            CMD = New OdbcCommand(CekStockBuku, konek)
            DR = CMD.ExecuteReader
            DR.Read()
            If DR.HasRows Then
                MsgBox("Stock Buku kurang dari 3, silahkan lakukan re-stock!", vbInformation)
            Else
                TglMySQL = Format(Today, "yyyy-MM-dd")
                Dim Simpantransaksi As String = "Insert into transaksi values ('" & LBLNoTransaksi.Text & "','" & TglMySQL & "','" & LBLJam.Text & "','" & LBLItem.Text & "','" & Label9.Text & "','" & TextBox1.Text & "','" & LBLKembali.Text & "','" & ComboBox1.Text & "','" & Menu_Utama.ToolStripStatusLabel2.Text & "')"
                CMD = New OdbcCommand(Simpantransaksi, konek)
                CMD.ExecuteNonQuery()

                For Baris As Integer = 0 To DataGridView1.Rows.Count - 2
                    Dim SimpanDetail As String = "Insert into detail_transaksi values('" & LBLNoTransaksi.Text & "','" & DataGridView1.Rows(Baris).Cells(0).Value & "','" & DataGridView1.Rows(Baris).Cells(1).Value & "','" & DataGridView1.Rows(Baris).Cells(2).Value & "','" & DataGridView1.Rows(Baris).Cells(3).Value & "','" & DataGridView1.Rows(Baris).Cells(4).Value & "')"
                    CMD = New OdbcCommand(SimpanDetail, konek)
                    CMD.ExecuteNonQuery()
                    Dim CariBuku As String = "Select * From buku where id ='" & DataGridView1.Rows(Baris).Cells(0).Value & "'"
                    CMD = New OdbcCommand(CariBuku, konek)
                    DR = CMD.ExecuteReader
                    DR.Read()
                    If DR.HasRows Then
                        Dim UpdateBuku As String = "update buku set stock = '" & DR!stock - DataGridView1.Rows(Baris).Cells(3).Value & "' where id = '" & DataGridView1.Rows(Baris).Cells(0).Value & "'"
                        CMD = New OdbcCommand(UpdateBuku, konek)
                        CMD.ExecuteNonQuery()
                    End If
                Next
                MsgBox("Transaksi Telah Berhasil Disimpan")
            End If  
            Call KondisiAwal()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
        Menu_Utama.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        KondisiAwal()
    End Sub
End Class