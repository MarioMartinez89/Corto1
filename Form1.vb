Imports System.Data.SqlClient

Public Class Form1
	Dim con As New SqlConnection
	Dim cmd As New SqlCommand
	Dim i As Integer
	Dim nombre, imc2 As String
	Dim peso, altura, imc As Double

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		peso = TxtBpeso.Text()
		altura = TxtBaltura.Text
		imc = peso / (altura * altura)
		Lbimcr.Text = Format(imc, "#.#0")


		Dim imc2 As String = If(imc <= 18.5, "Bajo de peso",
										If(imc <= 24.9, "Peso normal",
										If(imc <= 29.9, "Sobrepeso",
										If(imc <= 34.9, "Obesidad grado l",
										If(imc <= 39.9, "Obesidad grado ll", "Obesidad grado III (Obesidad mórbida)")))))

		Lbmsg.Text = imc2.ToString()


	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		cmd = con.CreateCommand()
		cmd.CommandType = CommandType.Text
		cmd.CommandText = "insert into Resultados values('" + TxtBnombre.Text + "','" + TxtBpeso.Text + "','" + Lbimcr.Text + "','" + Lbmsg.Text + "')"
		cmd.ExecuteNonQuery()
		TxtBnombre.Text = ""
		TxtBpeso.Text = ""
		TxtBaltura.Text = ""
		Label6.Text = " Exito al Grabar Información"
		disp_data()
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		If con.State = ConnectionState.Open Then
			con.Close()
		End If
		con.Open()
		cmd = con.CreateCommand()
		cmd.CommandType = CommandType.Text
		cmd.CommandText = "delete from Resultados where Nombre= '" + TxtBnombre.Text + "'"
		cmd.ExecuteNonQuery()
		disp_data()

	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		Me.Close()
		Form2.Close()
	End Sub


	Private Sub Form1_load(sender As Object, e As EventArgs) Handles MyBase.Load
		con.ConnectionString = "Data Source=DESKTOP-P3R1P5S\SQLEXPRESS;Initial Catalog=IMC;integrated Security=True"
		If con.State = ConnectionState.Open Then
			con.Close()
		End If
		con.Open()
		Label6.Text = "Conexion ok"
		disp_data()
	End Sub

	Public Sub disp_data()
		cmd = con.CreateCommand()
		cmd.CommandType = CommandType.Text
		cmd.CommandText = "Select * from Resultados"
		cmd.ExecuteNonQuery()
		Dim dt As New DataTable()
		Dim da As New SqlDataAdapter(cmd)
		da.Fill(dt)
		DataGridView1.DataSource = dt
	End Sub
	Private Sub DataGridView1_Cellclick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
		Try
			If con.State = ConnectionState.Open Then
				con.Close()

			End If
			con.Open()
			i = Convert.ToInt32(DataGridView1.SelectedCells.Item(0).Value.ToString())
			cmd = con.CreateCommand()
			cmd.CommandType = CommandType.Text
			cmd.CommandText = "Select * from Resultados where Nombre=" & i & ""
			cmd.ExecuteNonQuery()
			Dim dt As New DataTable()
			Dim da As New SqlDataAdapter(cmd)
			da.Fill(dt)
			Dim dr As SqlClient.SqlDataReader
			dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
			While dr.Read
				TxtBnombre.Text = dr.GetString(1).ToString()
				TxtBaltura.Text = dr.GetString(2).ToString()

			End While

		Catch ex As Exception


		End Try
	End Sub
End Class
