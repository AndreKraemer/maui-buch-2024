using CustomControlsDemo.ViewModels;

namespace CustomControlsDemo.Views;

public partial class ControlTemplatesDemoPage : ContentPage
{
	public ControlTemplatesDemoPage()
	{
		InitializeComponent();
    this.BindingContext = new CompositeControlDemoViewModel();
  }
}