using Banabet;
using Banabet.Services;
using Banabet.ViewModel;

namespace BanaBet;

public partial class RegisterPage : ContentPage
{
    private readonly MainViewModel _mainViewModel;
    
	public RegisterPage(RegisterViewModel vm, MainViewModel mainViewModel)
	{
        InitializeComponent();
        BindingContext = vm;
        _mainViewModel = mainViewModel;

        EmailEntry.TextChanged += async (sender, e) => await ((RegisterViewModel)BindingContext).CheckFields();
        PasswordEntry.TextChanged += async (sender, e) => await ((RegisterViewModel)BindingContext).CheckFields();
        EmergencyContactNameEntry.TextChanged += async (sender, e) => await ((RegisterViewModel)BindingContext).CheckFields();
        EmergencyContactNumberEntry.TextChanged += async (sender, e) => await ((RegisterViewModel)BindingContext).CheckFields();
        EstimatedMoneyEntry.TextChanged += async (sender, e) => await ((RegisterViewModel)BindingContext).CheckFields();
    }
    private async void TapSet(object sender, EventArgs e)
    {
        await _mainViewModel.EmpezarPublico();
    }
}