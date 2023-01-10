using Fitness.ClassLibrary.Resources;
using System.ComponentModel;

namespace Fitness.ClassLibrary.Models;

public class MuscleModel : INotify,IDataErrorInfo
{
    private string? name;
    public string this[string columnName]
    {
        get
        {
            if(columnName == nameof(Name))
            {
                if (string.IsNullOrEmpty(Name)) return "Debe especificar un músculo.";
            }
            return string.Empty;
        }
    }
    public int Id { get; set; }
    public string? Name
    {
        get => name;
        set => SetProperty(ref name, value);
    }

    public string Error => string.Empty;
}
