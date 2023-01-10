using System.Windows;
using System.Windows.Controls;

namespace FitnessApp.Views;

public partial class RoutineEditorView : Window
{
    public bool Response { get; set; }
    public RoutineEditorView()
    {
        InitializeComponent();
        Response = false;
    }

    private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (sender is TextBox tx)
        {
            tx.CaretIndex = int.MaxValue;
        }
    }

    private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
         if(sender is TextBox tx)
        {
            tx.CaretIndex = int.MaxValue;
        }
    }

    private void AcceptButton_Click(object sender, RoutedEventArgs e)
    {
        Response = true;
        Close();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Response = false;
        Close();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (Response) return;
        if(MessageBox.Show("Seguro que desea salir ?\nSe perdera el trabajo no guardado.","Cerrando Rutina",MessageBoxButton.YesNo) == MessageBoxResult.No)
        {
            e.Cancel = true;
        }
    }
}

