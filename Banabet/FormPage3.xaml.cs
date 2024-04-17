using Banabet.ViewModel;

namespace BanaBet;

public partial class FormPage3 : ContentPage
{
	public FormPage3(RegisterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}