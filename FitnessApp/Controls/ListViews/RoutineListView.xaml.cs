using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FitnessApp.Controls.ListViews;

public partial class RoutineListView : UserControl
{
    private string? sourceBindingPath;
    public string? SourceBindingPath
    {
        get { return sourceBindingPath; }
        set
        {
            sourceBindingPath = value;
            lv_Exercises.SetBinding(ListView.ItemsSourceProperty,sourceBindingPath);
        }
    }
    private string? selectedItemBindingPath;
    public string? SelectedItemBindingPath
    {
        get { return selectedItemBindingPath; }
        set
        {
            selectedItemBindingPath = value;
            lv_Exercises.SetBinding (ListView.SelectedItemProperty,selectedItemBindingPath);
        }
    }


    public ICommand DoubleClick_Command
    {
        get { return (ICommand)GetValue(DoubleClick_CommandProperty); }
        set { SetValue(DoubleClick_CommandProperty, value); }
    }

    public static readonly DependencyProperty DoubleClick_CommandProperty =
        DependencyProperty.Register("DoubleClick_Command", typeof(ICommand), typeof(RoutineListView), new PropertyMetadata(null));


    public RoutineListView()
    {
        InitializeComponent();
        
    }
    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        string? url = ((Hyperlink)sender).NavigateUri.ToString();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
    }

    private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        DoubleClick_Command?.Execute(null);
    }

    
}
