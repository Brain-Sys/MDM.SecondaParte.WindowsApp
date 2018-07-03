﻿Imports Microsoft.Win32
Imports MDM.SecondaParte.FileManagement

Class MainWindow
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

    Private Sub btnStart_Click(sender As Object, e As RoutedEventArgs) Handles btnStart.Click
        Dim logica As New FileMDM

        logica.Manage(txtFilename.Text)
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
End Class