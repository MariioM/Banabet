using Banabet.ViewModel;

namespace BanaBet;

public partial class FormPage2 : ContentPage
{
	public FormPage2(RegisterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}