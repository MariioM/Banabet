using Banabet.Services;
using Banabet.ViewModel;

namespace BanaBet;

public partial class ProfilePage : ContentPage
{
	private readonly AuthService _authService;
	private readonly MainViewModel _mainViewModel;
	public ProfilePage(AuthService authService, MainViewModel mainViewModel)
	{
		InitializeComponent();
		_authService = authService;
		_mainViewModel = mainViewModel;
	}

	private async void TapLogOut(object sender, EventArgs e)
	{
		_authService.Logout();
		await Shell.Current.GoToAsync(nameof(RegisterPage));
	}

	private void TapReset(object sender, EventArgs args)
	{
		_mainViewModel.ReinicioPublico();
	}
    private async void TapSet(object sender, EventArgs args)
    {
        await _mainViewModel.EmpezarPublico();
    }
}