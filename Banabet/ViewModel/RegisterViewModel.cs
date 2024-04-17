using Android.Hardware.Usb;
using Banabet.Services;
using BanaBet;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace Banabet.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        string emergencyContactName;
        [ObservableProperty]
        string emergencyContactNumber;
        [ObservableProperty]
        decimal estimatedMoney;

        [ObservableProperty]
        string loginEmail;
        [ObservableProperty]
        string loginPassword;

        [ObservableProperty]
        bool isRegisterButtonEnabled;
        [ObservableProperty]
        bool isLoginButtonEnabled;

        DatabaseManager dbManager = new DatabaseManager();
        AuthService authService = new AuthService();

        public async Task CheckFields()
        {
            await Task.Run(() =>
            {
                IsRegisterButtonEnabled = !string.IsNullOrEmpty(Email) &&
                                          IsValidEmail(Email) &&
                                          !string.IsNullOrEmpty(Password) &&
                                          !string.IsNullOrEmpty(EmergencyContactName) &&
                                          !string.IsNullOrEmpty(EmergencyContactNumber) &&
                                          EstimatedMoney != 0;
                IsLoginButtonEnabled = !string.IsNullOrEmpty(LoginEmail) &&
                                       IsValidEmail(LoginEmail) &&
                                       !string.IsNullOrEmpty(LoginPassword);
            });
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        [RelayCommand]
        async Task RegisterTap()
        {
            bool success = dbManager.EnviarDatosRegistroBase(Email, Password, EmergencyContactName, EmergencyContactNumber, EstimatedMoney);
            if (success)
            {
                Console.WriteLine("Registro completo!");
                authService.Login();
                await Shell.Current.GoToAsync(nameof(FormPage1));
            }    
        }

        [RelayCommand]
        async Task LoginTap()
        {
            bool success = dbManager.ComprobarUsuarioExiste(LoginEmail, LoginPassword);
            if (success)
            {
                Console.WriteLine("Login completo!");
                authService.Login();
                await Shell.Current.GoToAsync(nameof(FormPage1));
            }
            
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
        async Task GoToRegister()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

        [RelayCommand]
        async Task GoToLogin()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        [RelayCommand]
        async Task Skip()
        {
            await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}
