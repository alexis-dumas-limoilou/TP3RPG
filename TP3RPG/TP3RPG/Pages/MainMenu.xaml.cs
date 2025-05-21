namespace TP3RPG.Pages;

public partial class MainMenu : ContentPage
{
    public bool IsWindows { get; set; }

    public MainMenu()
	{
		InitializeComponent();

        IsWindows = DeviceInfo.Current.Platform == DevicePlatform.WinUI;
        BindingContext = this;
    }

    private async void OnNouvPartieClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new CarteJeu(1);
    }

    private void OnInfoClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new InfoPage();
    }

    private void OnQuitClicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}