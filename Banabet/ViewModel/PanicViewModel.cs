using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banabet.ViewModel
{
    public partial class PanicViewModel : ObservableObject
    {

        public PanicViewModel()
        {
            _ = StartTimer();
        }
        [ObservableProperty]
        string secs = "30", mins = "0";
        [ObservableProperty]
        bool reinicio = false;
        [ObservableProperty]
        int sec = 30, min = 0;
        [ObservableProperty]
        bool showButton = false;
        [RelayCommand]
        async Task StartTimer()
        {
            for(int i = 0; i < 30; i++)
            {
                Sec--;
                Secs = Convert.ToString(Sec);
                if(Sec == 0)
                {
                    ShowButton = true;
                }
                await Task.Delay(1000);
            }
        }
    }
}