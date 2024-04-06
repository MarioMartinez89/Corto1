Imports System.Data.SqlClient


Public Class Form2
	Public con As New SqlConnection
	Public cmd As New SqlCommand
	Public dr As SqlDataReader
	Public adaptador As SqlDataAdapter
	Public ds As DataSet
	Dim i As Integer
	Private Sub Form2_load(sender As Object, e As EventArgs) Handles MyBase.Load
		con.ConnectionString = "Data Source=DESKTOP-P3R1P5S\SQLEXPRESS;Initial Catalog=IMC;integrated Security=True"
		If con.State = ConnectionState.Open Then
			con.Close()
		End If
		con.Open()
		Label4.Text = " "

	End Sub

	Function Eusuario(ByVal usuario As String) As Boolean
		Dim result As Boolean = False
		Try
			cmd = New SqlCommand("Select * from usuarios where Usuario = '" & usuario & "'", con)
			dr = cmd.ExecuteReader
			If dr.Read Then
				result = True
			End If
			dr.Close()

		Catch ex As Exception

		End Try
		Return result
	End Function

	Function Obcontra(ByVal usuario As String) As String
		Dim contra As String = ""
		Try
			cmd = New SqlCommand("Select Password from Usuarios where usuario= '" & usuario & "'", con)
			dr = cmd.ExecuteReader
			If dr.Read Then
				contra = dr.Item("Password").ToString

			End If
			dr.Close()

		Catch ex As Exception

		End Try
		Return contra


	End Function

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If Eusuario(TextBox1.Text) = True Then
			If TextBox2.Text.Equals(Obcontra(TextBox1.Text)) Then
				Me.Hide()
				Form1.Show()

			Else
				MsgBox("contraseña no es valida")
			End If

		Else
			MsgBox("El usuario y/o contraseña no es valida")
		End If
	End Sub
End Class