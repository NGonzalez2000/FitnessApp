using Fitness.ClassLibrary.Resources;
using System.ComponentModel;

namespace Fitness.ClassLibrary.Models;

public class TimeModel : INotify
{
    private int seconds;
    public int Minutes { get; set; }
    public int Seconds
    {
        get => seconds;
        set => SetProperty(ref seconds, value);
    }
    public TimeModel()
    {

    }
    public TimeModel(TimeModel time)
    {
        Seconds = time.Seconds;
        Minutes = time.Minutes;
    }
}
