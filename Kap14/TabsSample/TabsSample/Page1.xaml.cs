namespace TabsSample;

public partial class Page1 : ContentPage
{
	public Page1()
	{
		InitializeComponent();
	}

	private void TrainingButton_Clicked(object sender, EventArgs e)
	{
		var url = $"https://www.andrekraemer.de/training/app-entwicklung/cross-plattform-apps-mit-net-maui-entwickeln/?utm_source=maui-book&utm_medium=sample-app&utm_campaign={AppInfo.Name}";
		Browser.OpenAsync(url);
	}

	private void ProjectButton_Clicked(object sender, EventArgs e)
	{
		var url = $"https://qualitybytes.de/services/mobile-apps/?utm_source=maui-book&utm_medium=sample-app&utm_campaign={AppInfo.Name}";
		Browser.OpenAsync(url);
	}
}