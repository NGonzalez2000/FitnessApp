using Fitness.ClassLibrary.DBAccess;
using System;
using System.Collections.ObjectModel;

namespace Fitness.ClassLibrary.Models.FilterModels;

public class ExerciseFilterModel
{
    private readonly Action<Predicate<object>> _filter;
    private MuscleModel? selectedMuscle;
    public MuscleModel? SelectedMuscle
    {
        get => selectedMuscle;
        set
        {
            selectedMuscle = value;
            _filter.Invoke(FilterExecute);
        }
    }
    private string exerciseFilter;
    public string ExerciseFilter
    {
        get => exerciseFilter;
        set
        {
            exerciseFilter = value;
            _filter.Invoke(FilterExecute);
        }
    }
    public ObservableCollection<MuscleModel> Muscles { get; set; }
    public ExerciseFilterModel(IMuscleDA muscleDA,Action<Predicate<object>> filter)
    {
        _filter = filter;
        exerciseFilter = string.Empty;
        Muscles = new(muscleDA.SelectAll());
    }
    public bool FilterExecute(object o)
    {
        if (o is ExerciseModel e)
        {
            bool ret = true;
            ret &= e.Name!.Contains(exerciseFilter.ToUpper());
            ret &= selectedMuscle is null || e.Muscle!.Id == selectedMuscle.Id;
            return ret;
        }
        return false;
    }
}
