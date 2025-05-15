using TP3RPG.Model;


#if WINDOWS
    using Windows.System;
    using Microsoft.Maui.Controls.Platform;
#endif



namespace TP3RPG.Assets
{
    public class Controls
    {
        private Joueur joueur;
        #if WINDOWS
            private Microsoft.UI.Xaml.Window nativeWindow;
        #endif
        public Controls(Joueur joueur)
        {
            this.joueur = joueur;
           
            #if WINDOWS
            var mauiWindow = Microsoft.Maui.Controls.Application.Current.Windows.FirstOrDefault();
            if (mauiWindow?.Handler?.PlatformView is MauiWinUIWindow winuiWindow)
            {
                nativeWindow = winuiWindow;
                nativeWindow.Activated += (s, e) =>
                {
                    nativeWindow.CoreWindow.KeyDown += OnKeyDown;
                };
            }
            #endif
        }

#if WINDOWS
        private void OnKeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            Console.WriteLine($"KeyDown détecté: {args.VirtualKey}"); // Ajout
            if (joueur == null) return; // Empêcher les erreurs null

            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    joueur.SeDeplacer("gauche");
                    Console.WriteLine("il va se deplacer a gauche");
                    break;
                case VirtualKey.Right:
                    joueur.SeDeplacer("droite");
                    Console.WriteLine("il va se deplacer a droite");
                    break;
                case VirtualKey.Up:
                    joueur.SeDeplacer("haut");
                    Console.WriteLine("il va se deplacer en haut");
                    break;
                case VirtualKey.Down:
                    joueur.SeDeplacer("bas");
                    Console.WriteLine("il va se deplacer en bas");
                    break;
            }
        }
#endif
    }

}