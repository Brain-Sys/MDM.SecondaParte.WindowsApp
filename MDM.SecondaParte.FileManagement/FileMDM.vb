Imports System.IO
Imports System.Text

Public Class FileMDM

    Public Function Manage(Input As String) As String
        Dim contenutoFileFinale As String = ""

        Dim linee As String() = System.IO.File.ReadAllLines(Input)

        For Each linea In linee
            'Debug.WriteLine(linea)
            Dim indice As Integer = linea.IndexOf(":")

            If indice <> -1 Then
                ' Riga valida, la elaboro...
                Dim Parte1 As String = linea.Substring(0, indice + 1)
                Dim Parte2 As String = linea.Substring(indice + 6)
                Dim numeroPorta As String = linea.Substring(indice + 1, 5)
                Debug.WriteLine(numeroPorta)

                Dim parametro As Double = Double.Parse(numeroPorta)
                parametro = parametro / 2.0

                contenutoFileFinale += Parte1 + parametro.ToString() + Parte2 + vbCrLf
            End If

        Next

        File.WriteAllText("Z:\FileFinale.mdm", contenutoFileFinale)


    End Function


End Class