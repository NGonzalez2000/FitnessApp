using Fitness.ClassLibrary.DBAccess;
using Fitness.ClassLibrary.Models;
using Fitness.ClassLibrary.Resources;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Fitness.ClassLibrary.ViewModels;

public class MuscleVIewModel : INotify
{
	private readonly IMuscleDA _muscleDA;
	private string muscleFilter;
	public MuscleModel NewMuscle { get; set; }
	public MuscleModel? EditMuscle { get; set; }
    public ICommand NewMuscle_Command => new RelayCommand(_ => NewMuscle_Execute(), _ => string.IsNullOrEmpty(NewMuscle["Name"]));
	public ICommand EditMuscle_Command => new RelayCommand(_ => EditMuscle_Execute(), _ => Muscles.SelectedItem is not null);
	public ICommand DeleteMuscle_Command => new RelayCommand(_ => DeleteMuscle_Execute(), _ => Muscles.SelectedItem is not null);
	public ICommand CancelNew_Command => new RelayCommand(_ => CancelNew_Execute());
	public ICommand CancelEdit_Command => new RelayCommand(_ => CancelEdit_Execute());
    public string MuscleFilter
	{
		get { return muscleFilter; }
		set
		{
			SetProperty(ref muscleFilter, value);
            Muscles.FilterExecute(Filter);
        }
    }

	public TCollectionView<MuscleModel> Muscles { get; set; }
	
	public MuscleVIewModel(IMuscleDA muscleDA)
	{
		_muscleDA = muscleDA;
        muscleFilter = string.Empty;

		Muscles = new(_muscleDA.SelectAll())
		{
			OnSelectionChanged = OnSelectionChanged
		};
		NewMuscle = new();
    }

	private bool Filter(object o)
	{
		if (o is MuscleModel muscle && muscle.Name!.Contains(muscleFilter.ToUpper())) return true;
		return false;
	}
	private async void NewMuscle_Execute()
	{
		if(Muscles.Collection.Any(x => x.Name == NewMuscle.Name))
		{
			MessageBox.Show($"Ya existe un músclo de nombre {NewMuscle.Name}.");
			return;
		}

		NewMuscle.Id = (await _muscleDA.Insert(NewMuscle)).First();
		Muscles.Insert(NewMuscle);

		MuscleFilter = muscleFilter;

		NewMuscle = new();
		OnPropertyChanged(nameof(NewMuscle));
	}
    private async void EditMuscle_Execute()
    {
        if (Muscles.Collection.Any(x => x.Name == NewMuscle.Name))
        {
            MessageBox.Show($"Ya existe un músclo de nombre {NewMuscle.Name}.");
            return;
        }
		MuscleModel editMuscle = new() { Id = Muscles.SelectedItem!.Id, Name = Muscles.SelectedItem!.Name };
        await _muscleDA.Update(editMuscle!);

		Muscles.Edit(Muscles.SelectedItem!, editMuscle!);

		Muscles.SelectedItem = null;

		MessageBox.Show("Músculo editado con éxito.");
    }
	private async void DeleteMuscle_Execute()
	{
		if (MessageBox.Show($"Seguro que desea eliminar al músculo {Muscles.SelectedItem!.Name} ?","Eliminar músculo", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

		await _muscleDA.Delete(Muscles.SelectedItem!);
		Muscles.Delete(Muscles.SelectedItem!);
	}
	private void CancelNew_Execute()
	{
		NewMuscle = new();
		OnPropertyChanged(nameof(NewMuscle));
	}
	private void CancelEdit_Execute()
	{
		Muscles.SelectedItem = null;
	}
	private void OnSelectionChanged(MuscleModel? muscle)
	{
		EditMuscle = null;
		if (muscle is not null) EditMuscle = new() { Id = muscle.Id, Name = muscle.Name };
		OnPropertyChanged(nameof(EditMuscle));
	}
}
