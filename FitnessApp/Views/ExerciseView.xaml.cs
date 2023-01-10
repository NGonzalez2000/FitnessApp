using Fitness.ClassLibrary.DBAccess;
using Fitness.ClassLibrary.ViewModels;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace FitnessApp.Views;


public partial class ExerciseView : UserControl
{
    public ExerciseView(IMuscleDA muscleDA, IExerciseDA exerciseDA)
    {
        InitializeComponent();
        DataContext = new ExerciseViewModel(muscleDA, exerciseDA);
    }
    
}
