using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{

    public partial class MainViewModel : ObservableObject
    {
        IConnectivity connectivity;
        public MainViewModel(IConnectivity connectivity)
        {
            Tasks = new ObservableCollection<string>();
            this.connectivity = connectivity;

        }
        [ObservableProperty]
        ObservableCollection<string> tasks;

        [ObservableProperty]
        string taskTitle;
        [RelayCommand]
        async Task Add()
        {
            if (string.IsNullOrWhiteSpace(TaskTitle))
                return;
            if(connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("ПР №1", "нет соединения с интернетом", "ОК");
                return;
            }
            Tasks.Add(TaskTitle);
            TaskTitle = string.Empty;
        }
        [RelayCommand]
        void Delete(string s) 
        {
            if (Tasks.Contains(s)) { 
            
                Tasks.Remove(s);
            }
        }
        [RelayCommand]
        async Task Tap(string s) { 
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
        }
    }
}
