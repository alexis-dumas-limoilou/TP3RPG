namespace TP3RPG.Pages;

public partial class MainMenu : ContentPage
{
	public MainMenu()
	{
		InitializeComponent();
	}

    private async void OnNouvPartieClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new CarteJeu();
    }

    private void OnInfoClicked(object sender, EventArgs e)
    {
        
    }

    private void OnQuitClicked(object sender, EventArgs e)
    {
        
    }
}