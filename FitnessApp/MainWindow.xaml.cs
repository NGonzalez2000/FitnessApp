using Fitness.ClassLibrary.DBAccess;
using FitnessApp.Views;
using System;
using System.Windows;

namespace FitnessApp;
public partial class MainWindow : Window
{
    private readonly IMuscleDA muscleDA;
    private readonly IExerciseDA exerciseDA;
    private readonly IRoutineDA routineDA;

    public MainWindow(IMuscleDA muscleDA, IExerciseDA exerciseDA, IRoutineDA routineDA)
    {
        InitializeComponent();
        this.muscleDA = muscleDA;
        this.exerciseDA = exerciseDA;
        this.routineDA = routineDA;
        OpenRoutineView();
    }

    private void Muscle_Click(object sender, RoutedEventArgs e)
        => OpenMuscleView();
    

    private void Exercise_Click(object sender, RoutedEventArgs e) 
        => OpenExerciseView();
    

    private void Routine_Click(object sender, RoutedEventArgs e)
        => OpenRoutineView();
    

    private void OpenMuscleView()
    {
        MuscleView view = new(muscleDA);
        MainContentControl.Content = view;
    }

    private void OpenExerciseView()
    {
        ExerciseView view = new(muscleDA, exerciseDA);
        MainContentControl.Content = view;
    }

    private void OpenRoutineView()
    {
        RoutineView view = new(exerciseDA, muscleDA, routineDA);
        MainContentControl.Content = view;
    }
}
