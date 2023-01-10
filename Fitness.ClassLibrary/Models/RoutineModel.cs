using Fitness.ClassLibrary.Resources;
using System;
using System.ComponentModel;

namespace Fitness.ClassLibrary.Models;

public class RoutineModel : INotify, IDataErrorInfo
{
    private TimeModel duration;
    private TimeModel rest;
    int laps;
    public int Id { get; set; }
    public TCollectionView<ExerciseModel> Exercises { get; set; }
    public TimeModel Duration
    {
        get => duration;
        set
        {
            if(SetProperty(ref duration, value))
            {
                CalculateTotalTime();
            }
        }
    }
    public TimeModel Rest
    {
        get => rest;
        set
        {
            if(SetProperty(ref rest, value)){
                CalculateTotalTime();
            }
        }
    }
    public TimeModel TotalTime { get; set; }
    public int Laps
    {
        get => laps;
        set
        {
            if(SetProperty(ref laps, value))
            {
                CalculateTotalTime();
            }
        }
    }
    public string Name { get; set; }

    public string Error => string.Empty;

    public int DurationMin
    {
        set { Duration.Minutes = value; }
    }
    public int DurationSec
    {
        set { Duration.Seconds = value; }
    }
    public int RestMin
    {
        set { Rest.Minutes = value; }
    }
    public int RestSec
    {
        set { Rest.Seconds = value; }
    }

    public string this[string columnName]
    {
        get
        {
            if (columnName == nameof(Duration) && Duration.Seconds > 59) return "los segundos deben estar entre 0 - 59";
            if (columnName == nameof(Rest) && Rest.Seconds > 59) return "los segundos deben estar entre 0 - 59";
            return string.Empty;
        }
    }

    public RoutineModel()
    {
        Exercises = new();
        Name = "Rutina " + DateOnly.FromDateTime(DateTime.Now).ToString();
        duration = new();
        rest = new();
        TotalTime = new();
        Exercises.CollectionChanged = CalculateTotalTime;
    }
    public RoutineModel(RoutineModel routine) : this()
    {
        Id = routine.Id;
        Exercises = new(routine.Exercises.Collection);
        Name = routine.Name;
        duration = new(routine.Duration);
        rest = new(routine.Rest);
        laps = routine.laps;
        Exercises.CollectionChanged = CalculateTotalTime;
        CalculateTotalTime();
    }

    private void CalculateTotalTime()
    {
        TotalTime = new();
        int tseconds = (rest.Seconds + duration.Seconds + (rest.Minutes + duration.Minutes) * 60) * laps * Exercises.Collection.Count;

        TotalTime.Seconds = tseconds % 60;
        TotalTime.Minutes = tseconds / 60;

        OnPropertyChanged(nameof(TotalTime));
    }
}
