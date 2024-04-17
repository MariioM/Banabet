using Banabet.ViewModel;

namespace BanaBet;

public partial class LoginPage : ContentPage
{
	public LoginPage(RegisterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}