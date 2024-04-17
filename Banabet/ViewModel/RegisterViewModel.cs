using Banabet.Services;
using BanaBet;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Banabet.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {

        //private readonly MainViewModel _mainViewModel = new MainViewModel();
        AuthService authService = new AuthService();

        [RelayCommand]
        async Task RegisterTap()
        {
            authService.Login();
            // _mainViewModel.EmpezarPublico();
            await Shell.Current.GoToAsync(nameof(FormPage1));
        }  
        
        [RelayCommand]
        async Task FormFirstTap()
        {
            await Shell.Current.GoToAsync(nameof(FormPage2));
        }
        
        [RelayCommand]
        async Task FormSecondTap()
        {
            await Shell.Current.GoToAsync(nameof(FormPage3));
        }

        [RelayCommand]
        async Task FormThirdTap()
        {
            await Shell.Current.GoToAsync(nameof(FormPage4));
        }

        [RelayCommand]
        async Task FormLastTap()
        {
            await Shell.Current.GoToAsync(nameof(FormPage5));
        }
        
        [RelayCommand]
        async Task Skip()
        {
            await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}
