using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Fitness.ClassLibrary.Resources;

public class TCollectionView<T> : INotify
{

    private T? selectedItem;
    private ObservableCollection<T> collection;
    private ICollectionView collectionView;
    public ObservableCollection<T> Collection
    {
        get { return collection; }
        set { collection = value; }
    }
    public T? SelectedItem
    {
        get { return selectedItem; }
        set
        {
            if (SetProperty(ref selectedItem, value) && OnSelectionChanged != null)
            {
                OnSelectionChanged!.Invoke(selectedItem);
            }
        }
    }

    public TCollectionView()
    {

        collection = new ObservableCollection<T>();
        collectionView = CollectionViewSource.GetDefaultView(collection);
    }
    public TCollectionView(IEnumerable<T>? _collection) : this()
    {
        if (_collection != null) collection = new(_collection);
    }
    public void FilterExecute(Predicate<object> predicate)
    {
        collectionView = CollectionViewSource.GetDefaultView(collection);
        collectionView.Filter = predicate;
        collectionView.Refresh();
    }
    public void SortExecute(SortDescription sortDescription)
    {
        collectionView = CollectionViewSource.GetDefaultView(collection);
        collectionView.SortDescriptions.Add(sortDescription);
        collectionView.Refresh();
    }
    public void Insert(T item)
    {
        Collection.Add(item);
        CollectionChanged?.Invoke();
    }
    public void Edit(T oldi, T newi)
    {
        int indx = Collection.IndexOf(oldi);
        Collection[indx] = newi;
        SelectedItem = Collection[indx];
    }
    public void Delete(T item)
    {
        Collection.Remove(item);
        CollectionChanged?.Invoke();
    }

    public Action<T?>? OnSelectionChanged;
    public Action? CollectionChanged;
}
