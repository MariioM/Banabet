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

        private bool _isScreenLocked;

        public bool IsScreenLocked
        {
            get => _isScreenLocked;
            set => SetProperty(ref _isScreenLocked, value);
        }
        [RelayCommand]
        public async Task LockScreenForSeconds(int seconds)
        {
            IsScreenLocked = true;
            await Task.Delay(seconds * 1000); // Delay for specified seconds
            IsScreenLocked = false;
        }

    }
}