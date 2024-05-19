using CustomControlsDemo.ViewModels;

namespace CustomControlsDemo.Views;

public partial class CompositeControlDemoPage : ContentPage
{
	public CompositeControlDemoPage()
	{
		InitializeComponent();
    this.BindingContext = new CompositeControlDemoViewModel();
	}
}