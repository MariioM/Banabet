using Banabet.ViewModel;

namespace BanaBet;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }
}