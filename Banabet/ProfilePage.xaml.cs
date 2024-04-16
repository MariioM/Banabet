using Banabet.Services;

namespace BanaBet;

public partial class ProfilePage : ContentPage
{
	private readonly AuthService _authService;
	public ProfilePage(AuthService authService)
	{
		InitializeComponent();
		_authService = authService;
	}

	private async void TapLogOut(object sender, EventArgs e)
	{
		_authService.Logout();
		await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
	}
}