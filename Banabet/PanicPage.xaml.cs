using Banabet.ViewModel;
using Mopups.Services;

namespace BanaBet;

public partial class PanicPage
{
	private readonly MainViewModel _mainViewModel;
	public PanicPage(PanicViewModel vm, MainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = vm;
		_mainViewModel = mainViewModel;
	}
	private void Atras(object sender, EventArgs e)
	{
		MopupService.Instance.PopAsync();
	}
	private void Restart(object sender, EventArgs e)
	{
		_mainViewModel.ReinicioPublico();
	} 
}