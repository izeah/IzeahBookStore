Imports System.Data.Odbc

Public Class FormUser
    Dim databaru As Boolean
    Dim Conn As OdbcConnection
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim str As String
    Dim CMD As OdbcCommand
    Dim DR As OdbcDataReader

    Sub isigrid()
        koneksi()
        da = New Odbc.OdbcDataAdapter("SELECT * FROM user", konek)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "user")
        DataGridView1.DataSource = (ds.Tables("user"))
        DataGridView1.Enabled = True
    End Sub

    Sub bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub

    Sub NomorOtomatis()
        Call koneksi()
        CMD = New OdbcCommand("Select * from user where id in (Select max(id) from user)", konek)
        Dim UrutanKode As String
        Dim Hitung As Long
        DR = CMD.ExecuteReader
        DR.Read()
        If Not DR.HasRows Then
            UrutanKode = "U" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(DR.GetString(0), 3) + 1
            UrutanKode = "U" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox1.Text = UrutanKode
    End Sub

    Private Sub FormUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        databaru = False
        Button1.Text = "INPUT"
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        Button5.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        isigrid()
        NomorOtomatis()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "INPUT" Then
            Button2.Enabled = True
            Button5.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            TextBox4.Enabled = True
            TextBox5.Enabled = True
            Button1.Text = "SIMPAN"
        ElseIf Button1.Text = "SIMPAN" Then
            Dim simpan As String = ""
            TextBox2.Focus()
            If TextBox2.Text = "" Then
                MsgBox("Nama User Kosong", vbInformation)
            ElseIf TextBox3.Text = "" Then
                MsgBox("Username Kosong", vbInformation)
            ElseIf TextBox4.Text = "" Then
                MsgBox("Password Kosong", vbInformation)
            ElseIf TextBox5.Text = "" Then
                MsgBox("Level Kosong", vbInformation)
            Else
                databaru = True
            End If
            If databaru = True Then
                simpan = "INSERT INTO user(id,nama,username,password,level) VALUES ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "')"
                Dim pesan = MsgBox("Apakah Anda Yakin Data Akan ditambahkan ke Database ?", vbYesNo + vbInformation, "Perhatian")
                If pesan = MsgBoxResult.Yes Then
                    jalankansql(simpan)
                End If
            End If
            Me.Cursor = Cursors.WaitCursor
            DataGridView1.Refresh()
            isigrid()
            Me.Cursor = Cursors.Default
            bersih()
            NomorOtomatis()
        End If  
    End Sub

    Private Sub jalankansql(ByVal sQl As String)
        Dim objcmd As New System.Data.Odbc.OdbcCommand
        Call koneksi()
        Try
            objcmd.Connection = konek
            objcmd.CommandType = CommandType.Text
            objcmd.CommandText = sQl
            objcmd.ExecuteNonQuery()
            objcmd.Dispose()
            MsgBox("Syntax SQL berhasil", vbInformation)
        Catch ex As Exception
            MsgBox("Syntax SQL terjadi kesalahan" & ex.Message)
        End Try
    End Sub

    Private Sub isiTextBox(ByVal x As Integer)
        Try
            TextBox1.Text = DataGridView1.Rows(x).Cells(0).Value
            TextBox2.Text = DataGridView1.Rows(x).Cells(1).Value
            TextBox3.Text = DataGridView1.Rows(x).Cells(2).Value
            TextBox4.Text = DataGridView1.Rows(x).Cells(3).Value
            TextBox5.Text = DataGridView1.Rows(x).Cells(4).Value
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        isiTextBox(e.RowIndex)
        databaru = False
        Button3.Enabled = True
        Button4.Enabled = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        NomorOtomatis()
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim ubah As String = "UPDATE  user  SET  " + "nama  ='" & TextBox2.Text & "'," + "username  ='" & TextBox3.Text & "'," + "password  ='" & TextBox4.Text & "'," + "level  ='" & TextBox5.Text & "'  WHERE  id  =  '" & TextBox1.Text & "' "
        If TextBox2.Text = "" Then
            MsgBox("Nama User Kosong", vbInformation)
        ElseIf TextBox3.Text = "" Then
            MsgBox("Username Kosong", vbInformation)
        ElseIf TextBox4.Text = "" Then
            MsgBox("Password Kosong", vbInformation)
        ElseIf TextBox5.Text = "" Then
            MsgBox("Level Kosong", vbInformation)
        Else
            jalankansql(ubah)
        End If
        Me.Cursor = Cursors.WaitCursor
        DataGridView1.Refresh()
        isigrid()
        Me.Cursor = Cursors.Default
        bersih()
        NomorOtomatis()
        Button3.Enabled = False
        Button4.Enabled = False
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim hapussql As String
        Dim pesan As Integer
        pesan = MsgBox("Apakah anda yakin akan menghapus Data pada server  ...  " + TextBox2.Text, vbExclamation + vbYesNo, "perhatian")
        If pesan = vbNo Then Exit Sub
        hapussql = "DELETE  FROM  user  WHERE  id='" & TextBox1.Text & "'"
        jalankansql(hapussql)
        Me.Cursor = Cursors.WaitCursor
        DataGridView1.Refresh()
        isigrid()
        Me.Cursor = Cursors.Default
        Call bersih()
        NomorOtomatis()
        Button3.Enabled = False
        Button4.Enabled = False
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
        Menu_Utama.Show()
    End Sub
End Class