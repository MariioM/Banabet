using Banabet.ViewModel;

namespace BanaBet;

public partial class FormPage5 : ContentPage
{
	public FormPage5(RegisterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}