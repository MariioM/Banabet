using Banabet.Services;

namespace BanaBet;

public partial class LoadingPage : ContentPage
{
	private readonly AuthService _authService;
	public LoadingPage(AuthService authService)
	{
		InitializeComponent();
		_authService = authService;
	}

	protected async override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		if(await _authService.IsAutheticated())
		{
            //user loged in
            //Redirect to main
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
		else
		{
			//Usr not login
			//Redidirect to login
			await Shell.Current.GoToAsync(nameof(RegisterPage));
		}
	}


}