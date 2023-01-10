using Fitness.ClassLibrary.DBAccess;
using Fitness.ClassLibrary.Models;
using Fitness.ClassLibrary.Models.FilterModels;
using Fitness.ClassLibrary.Resources;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Fitness.ClassLibrary.ViewModels;

public class ExerciseViewModel : INotify
{
    private readonly IExerciseDA _exerciseDA;

    public ICommand NewExercise_Command => new RelayCommand(_ => NewExercise_Execute(),
        _ => string.IsNullOrEmpty(NewExercise["Name"]) && string.IsNullOrEmpty(NewExercise["Muscle"]));
    public ICommand EditExercise_Command => new RelayCommand(_ => EditExercise_Execute(),
        _ => EditExercise is not null && string.IsNullOrEmpty(EditExercise["Name"]) && string.IsNullOrEmpty(EditExercise["Muscle"]));
    public ICommand DeleteExercise_Command => new RelayCommand(_ => DeleteExercise_Execute(),_ => EditExercise is not null);
    public ICommand CancelNew_Command => new RelayCommand(_ => CancelNew_Execute());
    public ICommand CancelEdit_Command => new RelayCommand(_ => CancelEdit_Execute());
    
    public ExerciseFilterModel ExerciseFilter { get; set; }
    public ExerciseModel NewExercise { get; set; }
    public ExerciseModel? EditExercise { get; set; }
    public TCollectionView<ExerciseModel> Exercises { get; set; }
    public ObservableCollection<MuscleModel> Muscles { get; set; }
    public ExerciseViewModel(IMuscleDA muscleDa,IExerciseDA exerciseDA)
    {
        _exerciseDA = exerciseDA;

        Muscles = new(muscleDa.SelectAll());
        Exercises = new(_exerciseDA.SelectAll().OrderBy(x => x.Muscle!.Name).ThenBy(x => x.Name));
        Exercises.OnSelectionChanged = OnSelectionChanged;
        ExerciseFilter = new(muscleDa, Exercises.FilterExecute);
        NewExercise = new();
    }

    
    private void OnSelectionChanged(ExerciseModel? exercise)
    {
        EditExercise = null;
        if (exercise is not null)
        {
            EditExercise = new(exercise)
            {
                Muscle = Muscles.First(x => x.Name == exercise.Muscle!.Name)
            };
        }
        OnPropertyChanged(nameof(EditExercise));
    }
    private async void NewExercise_Execute()
    {
        if(Exercises!.Collection.Any(x => x.Name == NewExercise.Name))
        {
            MessageBox.Show($"Ya existe un ejercicio llamado {NewExercise.Name}.");
            return;
        }

        NewExercise.Id = (await _exerciseDA.Insert(NewExercise)).First();

        Exercises.Insert(NewExercise);

        MessageBox.Show("Ejercicio creado con éxito.");
        NewExercise = new()
        {
            Muscle = null
        };
        OnPropertyChanged(nameof(NewExercise));

    }
    private async void EditExercise_Execute()
    {
        if (Exercises!.Collection.Any(x => x.Name == NewExercise.Name))
        {
            MessageBox.Show($"Ya existe un ejercicio llamado {NewExercise.Name}.");
            return;
        }

        await _exerciseDA.Update(EditExercise!);

        Exercises.Edit(Exercises.SelectedItem!, EditExercise!);

        MessageBox.Show("Ejercicio editado con éxito.");
        EditExercise = null;
        OnPropertyChanged(nameof(EditExercise));
    }
    private async void DeleteExercise_Execute()
    {
        await _exerciseDA.Delete(Exercises.SelectedItem!);
        Exercises.Delete(Exercises.SelectedItem!);
    }
    private void CancelNew_Execute()
    {
        NewExercise = new()
        {
            Muscle = null
        };
        OnPropertyChanged(nameof(NewExercise));
    }
    private void CancelEdit_Execute()
    {
        Exercises.SelectedItem = null;
    }
}
