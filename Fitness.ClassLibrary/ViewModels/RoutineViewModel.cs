using Fitness.ClassLibrary.DBAccess;
using Fitness.ClassLibrary.Models;
using Fitness.ClassLibrary.Resources;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Fitness.ClassLibrary.ViewModels;

public class RoutineViewModel
{
	private string routineFilter;
	private readonly Func<RoutineModel, RoutineModel?> UseEditorView;
	private readonly IRoutineDA routineDA;

	public ICommand NewRoutine_Command => new RelayCommand(_ => NewRoutine_Execute());
	public ICommand EditRoutine_Command => new RelayCommand(_ => EditRoutine_Execute());
	public ICommand DeleteRoutine_Command => new RelayCommand(_ => DeleteRoutine_Execute());
    public string RoutineFilter
	{
		get { return routineFilter; }
		set
		{ 
			routineFilter = value;
			Routines.FilterExecute(Filter);
		}
	}
	public TCollectionView<RoutineModel> Routines { get; set; }
	public RoutineViewModel(Func<RoutineModel,RoutineModel?> func, IRoutineDA routineDA)
	{
		SortDescription sortDescription = new("Name", ListSortDirection.Ascending);
		Routines = new(routineDA.SelectAll());
		Routines.SortExecute(sortDescription);
		routineFilter = string.Empty;
		UseEditorView = func;
		this.routineDA = routineDA;
	}
	private bool Filter(object o)
	{
		if (o is RoutineModel r && r.Name.Contains(routineFilter, StringComparison.OrdinalIgnoreCase)) return true;
		return false;
	}
	
	private async void NewRoutine_Execute()
	{
		RoutineModel routine = new();

		routine.Id = (await routineDA.Insert(routine)).First();

		Routines.Insert(routine);
		Routines.SelectedItem = routine;

		EditRoutine_Execute();
	}

	private async void EditRoutine_Execute()
	{
		if (Routines.SelectedItem is null) return;

		Routines.SelectedItem.Exercises = new(await routineDA.SelectExercises(Routines.SelectedItem));

		RoutineModel? routine = UseEditorView.Invoke(Routines.SelectedItem);

		if (routine is null) return;

		await routineDA.Update(routine);

		foreach(ExerciseModel exercise in routine.Exercises.Collection)
		{
			ExerciseModel? temp = Routines.SelectedItem.Exercises.Collection.FirstOrDefault(x => x.Id == exercise.Id);

            if (temp is null)
			{
				await routineDA.InsertExercise(routine, exercise);
			}
			else Routines.SelectedItem.Exercises.Delete(temp);
		}

		foreach (ExerciseModel exercise in Routines.SelectedItem.Exercises.Collection) await routineDA.DeleteExercise(routine, exercise);

		Routines.Edit(Routines.SelectedItem, routine);
	}

	private async void DeleteRoutine_Execute()
	{
		if (Routines.SelectedItem is null) return;

		await routineDA.Delete(Routines.SelectedItem);

		Routines.Delete(Routines.SelectedItem);
	}
}
