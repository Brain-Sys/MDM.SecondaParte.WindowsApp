Imports Microsoft.Win32
Imports MDM.SecondaParte.FileManagement
Imports System.IO

Class MainWindow

    Dim logica As New FileMDM

    ' Metodo / Funzione
    Private Sub btnChooseFile_Click(sender As Object, e As RoutedEventArgs) Handles btnChooseFile.Click
        Dim chooseFromDisk As New OpenFileDialog

        ' Valorizziamo le proprietà di OpenFileDialog
        'chooseFromDisk.InitialDirectory = "C:\"
        chooseFromDisk.InitialDirectory =
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        chooseFromDisk.Multiselect = False
        chooseFromDisk.Filter = "File di MDM Chatillon|*.mdm|Tutti i files|*.*"

        ' Sub
        ' Function

        ' Chiamata ad un metodo / funzione
        Dim risultato As Boolean = chooseFromDisk.ShowDialog()

        If risultato = True Then
            ' L'utente ha scelto un file...
            txtFilename.Text = chooseFromDisk.FileName
            btnStart.IsEnabled = True
        Else
            ' L'utente ha annullato l'operazione
            txtFilename.Text = Nothing
            btnStart.IsEnabled = False
            MessageBox.Show("Operazione annullata")
        End If
    End Sub

    Private Function DammiElementoSelezionato() As String
        Dim selezionato As Integer = cmbSeparator.SelectedIndex
        Dim separatore As ComboBoxItem = cmbSeparator.Items(selezionato)
        Dim simbolo As String = separatore.Content.ToString()
        Return simbolo
    End Function

    Private Sub btnStart_Click(sender As Object, e As RoutedEventArgs) Handles btnStart.Click

        Try
            ' Come faccio a capire qual è l'elemento selezionato?
            Dim selezionato As Integer = cmbSeparator.SelectedIndex
            Dim separatore As ComboBoxItem = cmbSeparator.Items(selezionato)
            Dim fileGenerato As String



            If selezionato < 0 Then
                ' Nessuna selezione sulla combo
                fileGenerato = logica.Manage(txtFilename.Text)
            Else
                ' Ho una seleziona sulla combo
                Dim simbolo As String = separatore.Content.ToString()
                fileGenerato = logica.Manage(txtFilename.Text, simbolo)
            End If




            Dim messaggio As String = "Operazione completata!" +
            vbCrLf + vbCrLf + "Il file " + fileGenerato +
            " è stato generato con successo!"
            MessageBox.Show(messaggio, "OK!",
                        MessageBoxButton.OK, MessageBoxImage.Information)

            If chkOpenFile.IsChecked = True Then
                Process.Start("Notepad.exe", fileGenerato)
            End If

            'MessageBox.Show("Operazione completata!")
            'MessageBox.Show("Il file " + fileGenerato + " è stato generato con successo!")
        Catch ex As Exception
            File.AppendAllText("E:\\LogMDM.txt",
                               DateTime.Now + vbTab + ex.ToString() + vbCrLf)
            MessageBox.Show(ex.Message, "Errore",
                            MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

    End Sub

    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnExit.Click
        Dim risposta As MessageBoxResult

        risposta = MessageBox.Show("Sei sicuro di voler chiudere?", "Conferma!",
                        MessageBoxButton.YesNo, MessageBoxImage.Question)

        If risposta = MessageBoxResult.Yes Then
            Application.Current.Shutdown()
        End If

    End Sub

    Private Sub btnHelp_Click(sender As Object, e As RoutedEventArgs) Handles btnHelp.Click
        Dim finestra As New AboutWindow
        finestra.ShowDialog()
    End Sub

    Private Sub Grid_Loaded(sender As Object, e As RoutedEventArgs)
        txtFileGenerato.Visibility = Visibility.Hidden
    End Sub

    Private Sub btnReadVariable_Click(sender As Object, e As RoutedEventArgs) Handles btnReadVariable.Click
        Dim chooseFromDisk As New OpenFileDialog
        chooseFromDisk.Multiselect = False

        Dim risultato As Boolean = chooseFromDisk.ShowDialog()

        If risultato = True Then
            Dim elencoVariabili As Variable() = logica.ReadVariables(chooseFromDisk.FileName)
            lstVariables.ItemsSource = elencoVariabili
        End If
    End Sub

    Private Sub btnReadComplexFile_Click(sender As Object, e As RoutedEventArgs) Handles btnReadComplexFile.Click
        Dim lines As String() = File.ReadAllLines("C:\Users\igord\Desktop\Testo.txt")
        Dim simbolo As String = DammiElementoSelezionato()

        For Each currentLine In lines
            Dim pezziDiRiga As String() = currentLine.Split(simbolo)

            Dim nome As String = pezziDiRiga(0)
            Dim anno As String = pezziDiRiga(1)
            Dim città As String = pezziDiRiga(2)
            Dim lavoro As String = pezziDiRiga(3)

        Next
    End Sub
End Class