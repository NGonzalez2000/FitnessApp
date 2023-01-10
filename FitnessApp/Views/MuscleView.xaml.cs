using Fitness.ClassLibrary.DBAccess;
using Fitness.ClassLibrary.ViewModels;
using System.Windows.Controls;

namespace FitnessApp.Views;

/// <summary>
/// Interaction logic for MuscleView.xaml
/// </summary>
public partial class MuscleView : UserControl
{
    public MuscleView(IMuscleDA dataAccess)
    {
        InitializeComponent();
        DataContext = new MuscleVIewModel(dataAccess);
    }
}
