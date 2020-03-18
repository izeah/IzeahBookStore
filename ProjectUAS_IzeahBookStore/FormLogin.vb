Imports System.Data.Odbc

Public Class FormLogin
    Dim CMD As New OdbcCommand
    Dim DR As OdbcDataReader

    Private Sub FormLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox2.PasswordChar = "x"
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MessageBox.Show("username & password kosong", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Call koneksi()
            CMD = New OdbcCommand("select * from  user where username='" & TextBox1.Text & "' and password='" & TextBox2.Text & "'", konek)
            DR = CMD.ExecuteReader
            DR.Read()

            If DR.HasRows Then
                Me.Close()
                MessageBox.Show("LOGIN Berhasil :)!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Menu_Utama.Show()
                Menu_Utama.ToolStripStatusLabel2.Text = DR!id
                Menu_Utama.ToolStripStatusLabel4.Text = DR!nama
                Menu_Utama.ToolStripStatusLabel6.Text = DR!level
                Menu_Utama.Button1.Visible = False
                Menu_Utama.Button2.Visible = True
                Menu_Utama.Button3.Visible = True
                Menu_Utama.Button4.Visible = True
                Menu_Utama.Button5.Visible = True
                Menu_Utama.Button6.Visible = True
                Menu_Utama.Button7.Visible = True
            Else
                MessageBox.Show("LOGIN GAGAL! username/password salah", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox1.Text = ""
                TextBox2.Text = ""
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.PasswordChar = ""
        Else
            TextBox2.PasswordChar = "x"
        End If
    End Sub
End Class