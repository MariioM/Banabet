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
    }
    private async void TapSet(object sender, EventArgs e)
    {
        await _mainViewModel.EmpezarPublico();
    }
}