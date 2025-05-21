namespace TP3RPG.Pages;

public partial class InfoPage : ContentPage
{
	public InfoPage()
	{
		InitializeComponent();
	}

    private void OnBackClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new MainMenu();
    }
}