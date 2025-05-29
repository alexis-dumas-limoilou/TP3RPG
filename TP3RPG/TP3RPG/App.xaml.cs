using TP3RPG.Pages;
using TP3RPG.Service;

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
