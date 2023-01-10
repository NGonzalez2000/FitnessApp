using Fitness.ClassLibrary.DBAccess;
using Fitness.ClassLibrary.Models;
using Fitness.ClassLibrary.ViewModels;
using System.Windows.Controls;

namespace FitnessApp.Views;

public partial class RoutineView : UserControl
{
    private readonly IExerciseDA exerciseDA;
    private readonly IMuscleDA muscleDA;
    public RoutineView(IExerciseDA exerciseDA, IMuscleDA muscleDA, IRoutineDA routineDA)
    {
        InitializeComponent();
        DataContext = new RoutineViewModel(UseRoutineEditorView, routineDA);
        this.exerciseDA = exerciseDA;
        this.muscleDA = muscleDA;
    }

    private RoutineModel? UseRoutineEditorView(RoutineModel routine)
    {
        RoutineEditorViewModel dataContext = new(new(routine), exerciseDA, muscleDA);
        RoutineEditorView view = new()
        {
            DataContext = dataContext
        };


        _ = view.ShowDialog();

        if (!view.Response) return null;
        return dataContext.Routine;
    }

}
