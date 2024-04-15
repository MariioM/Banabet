using Banabet.ViewModel;

namespace BanaBet;

public partial class FormPage1 : ContentPage
{
	public FormPage1(RegisterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}