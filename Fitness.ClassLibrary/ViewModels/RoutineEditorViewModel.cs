using Fitness.ClassLibrary.DBAccess;
using Fitness.ClassLibrary.Models;
using Fitness.ClassLibrary.Models.FilterModels;
using Fitness.ClassLibrary.Resources;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Fitness.ClassLibrary.ViewModels;

public class RoutineEditorViewModel : INotify
{
	public ICommand AddExercise_Command => new RelayCommand(_ => AddExercise_Execute());
	public ICommand DeleteExercise_Command => new RelayCommand(_ => DeleteExercise_Execute());
	public ExerciseFilterModel ExerciseFilter { get; set; }
	public ExerciseFilterModel AuxExerciseFilter { get; set; }
    public RoutineModel Routine { get; set; }
	public TCollectionView<ExerciseModel> AuxExercises { get; set; }
	public RoutineEditorViewModel(RoutineModel routine, IExerciseDA exerciseDA, IMuscleDA muscleDA)
	{
		Routine = new(routine);
        AuxExercises = new(exerciseDA.SelectAll().Where(x => !Routine.Exercises.Collection.Any(y => y.Id == x.Id) ) );
        
		SortDescription muscleSortDescription = new("Muscle.Name",ListSortDirection.Ascending);
        SortDescription nameSortDescription = new("Name",ListSortDirection.Ascending);
		
		Routine.Exercises.SortExecute(muscleSortDescription);
		Routine.Exercises.SortExecute(nameSortDescription);

        AuxExercises.SortExecute(muscleSortDescription);
        AuxExercises.SortExecute(nameSortDescription);

		ExerciseFilter = new(muscleDA,Routine.Exercises.FilterExecute);
		AuxExerciseFilter = new(muscleDA, AuxExercises.FilterExecute);
    }
    private void AddExercise_Execute()
	{
		if (AuxExercises.SelectedItem is null) return;
		Routine.Exercises.Insert(AuxExercises.SelectedItem);
		AuxExercises.Delete(AuxExercises.SelectedItem);
	} 
    private void DeleteExercise_Execute()
	{
		if(Routine.Exercises.SelectedItem is null) return;
		AuxExercises.Insert(Routine.Exercises.SelectedItem);
		Routine.Exercises.Delete(Routine.Exercises.SelectedItem);

    }
}
