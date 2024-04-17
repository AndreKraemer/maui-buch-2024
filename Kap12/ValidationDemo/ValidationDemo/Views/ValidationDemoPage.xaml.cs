using ValidationDemo.ViewModels;

namespace ValidationDemo.Views;

public partial class ValidationDemoPage : ContentPage
{
	public ValidationDemoPage()
	{
		InitializeComponent();
    BindingContext = new ValidationDemoViewModel();
	}
}