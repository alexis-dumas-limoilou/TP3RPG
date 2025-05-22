namespace TP3RPG.Pages;

public partial class PauseMenu : ContentView
{
	public PauseMenu()
	{
		InitializeComponent();
        this.IsVisible = false;
	}

    public void Show() => this.IsVisible = true;
    public void Hide() => this.IsVisible = false;

    public event EventHandler ContinueClicked;
    public event EventHandler QuitClicked;

    private void OnContinueClicked(object sender, EventArgs e)
    {
        ContinueClicked?.Invoke(this, EventArgs.Empty);
        Hide();
    }

    private void OnQuitClicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}