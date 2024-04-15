using Banabet.ViewModel;

namespace BanaBet;

public partial class FormPage4 : ContentPage
{
	public FormPage4(RegisterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}