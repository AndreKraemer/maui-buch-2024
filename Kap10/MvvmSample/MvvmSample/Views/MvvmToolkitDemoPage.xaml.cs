using MvvmSample.ViewModels;

namespace MvvmSample.Views;

public partial class MvvmToolkitDemoPage : ContentPage
{
	public MvvmToolkitDemoPage()
	{
		InitializeComponent();
    BindingContext = new MvvmToolkitDemoViewModel();
  }
}