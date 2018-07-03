Imports System.IO
Imports System.Text

Public Class FileMDM

    'Public Peso As Double = 17.9
    'Public Const NumeroPezzi As Integer = 5

    Public NumeroLinee As Integer = 0
    Public NumeroLineeElaborate As Integer = 0

    Public Function Manage(Input As String) As String
        Dim contenutoFileFinale As String = ""
        Dim outputFile As String = "Z:\FileFinale.mdm"

        NumeroLineeElaborate = 0
        Dim linee As String() = System.IO.File.ReadAllLines(Input)
        NumeroLinee = linee.Length

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
                NumeroLineeElaborate += 1
                ' NumeroLineeElaborate = NumeroLineeElaborate + 1
            End If

        Next

        File.WriteAllText(outputFile, contenutoFileFinale)

        Return outputFile

    End Function

    Public Function Riuscita() As Boolean
        Return NumeroLinee = NumeroLineeElaborate
    End Function

End Class