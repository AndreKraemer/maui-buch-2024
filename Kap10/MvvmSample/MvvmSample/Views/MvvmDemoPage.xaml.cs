using MvvmSample.ViewModels;

namespace MvvmSample.Views;

public partial class MvvmDemoPage : ContentPage
{
	public MvvmDemoPage()
	{
		InitializeComponent();
    BindingContext = new MvvmDemoViewModel();
	}
}