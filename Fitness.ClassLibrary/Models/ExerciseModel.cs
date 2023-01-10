using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Controls;

namespace Fitness.ClassLibrary.Models;

public class ExerciseModel : IDataErrorInfo
{
    
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Link { get; set; }
    public MuscleModel? Muscle { get; set; }
    
    public int MuscleId { set => Muscle!.Id = value; }
    public string MuscleName { set => Muscle!.Name = value; }
    
    public string Error => string.Empty;

    public string this[string columnName]
    {
        get
        {
            if (columnName == nameof(Name) && string.IsNullOrEmpty(Name)) return "El ejercicio necesita un nombre.";
            if (columnName == nameof(Muscle) && (Muscle is null || string.IsNullOrEmpty(Muscle!.Name))) return "El ejercicio debe ser asignado a un grupo muscular.";
            
            return string.Empty;
        }
    }

    public ExerciseModel()
    {
        Link = string.Empty;
        Muscle = new MuscleModel();
    }
    public ExerciseModel(ExerciseModel exercise)
    {
        Id = exercise.Id;
        Name = exercise.Name;
        Link = exercise.Link;
        Muscle = new();
        Muscle.Id = exercise.Muscle!.Id;
        Muscle.Name = exercise.Muscle.Name;
    }
    
}
