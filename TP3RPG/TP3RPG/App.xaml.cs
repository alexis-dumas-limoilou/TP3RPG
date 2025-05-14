using TP3RPG.Pages;

namespace TP3RPG
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainMenu();
        }
    }
}
