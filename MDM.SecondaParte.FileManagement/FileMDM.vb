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

    Public Function Manage(fileDiInput As String, separator As String) As String
        Dim contenutoFileFinale As String = ""
        Dim outputFile As String = "Z:\FileFinale.mdm"

        NumeroLineeElaborate = 0
        Dim linee As String() = System.IO.File.ReadAllLines(fileDiInput)
        NumeroLinee = linee.Length

        For Each linea In linee
            'Debug.WriteLine(linea)
            Dim indice As Integer = linea.IndexOf(separator)

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

    Public Function ReadVariables(input As String) As Variable()
        Dim elencoVariabili As New List(Of Variable)

        If File.Exists(input) Then
            Dim linee As String() = File.ReadAllLines(input)

            For Each linea In linee
                If linea <> "" Then
                    Dim carattereIniziale = linea.Substring(0, 1)

                    If linea.StartsWith("(*") Then
                        Exit For
                    ElseIf carattereIniziale = "#" Then
                        Dim indice1 As Integer = linea.IndexOf("=")
                        Dim indice2 As Integer = linea.IndexOf(" ", indice1)

                        Dim v As New Variable
                        v.Nome = linea.Substring(0, indice1)
                        v.Valore = linea.Substring(indice1 + 1, indice2 - indice1).Trim()
                        v.Info = linea.Substring(linea.IndexOf("("))
                        elencoVariabili.Add(v)
                    End If
                End If
            Next

            Return elencoVariabili.ToArray()
        End If

        Return Nothing

    End Function

End Class