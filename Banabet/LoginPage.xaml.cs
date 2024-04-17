using Banabet.ViewModel;

namespace BanaBet;

public partial class LoginPage : ContentPage
{
	public LoginPage(RegisterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        LoginEmailEntry.TextChanged += async (sender, e) => await ((RegisterViewModel)BindingContext).CheckFields();
        LoginPasswordEntry.TextChanged += async (sender, e) => await ((RegisterViewModel)BindingContext).CheckFields();
    }
}