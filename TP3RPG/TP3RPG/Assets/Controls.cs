using SharpHook;
using SharpHook.Native;
using System.Diagnostics;
using TP3RPG.Model;
using TP3RPG.Pages;

namespace TP3RPG.Assets
{
    public class Controls
    {
        private Joueur joueur;
        private CarteJeu _carte;
        private EventSimulator simulator;
        private TaskPoolGlobalHook globalHook;
        private bool isDisposed = false;
        public Controls(Joueur joueur, CarteJeu carte)
        {
            this.joueur = joueur;
            this._carte = carte;

            globalHook = new TaskPoolGlobalHook();
            globalHook.KeyPressed += OnKeyDown;
            globalHook.RunAsync();
        }
        public event Action OnJoueurDéplacé;

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                if (globalHook != null)
                {
                    globalHook.KeyPressed -= OnKeyDown;
                    globalHook.Dispose();
                    globalHook = null;
                }
            }
        }

        private async void OnKeyDown(object sender, KeyboardHookEventArgs args)
        {
            if (isDisposed || joueur == null) return;

            switch (args.Data.KeyCode)
            {
                case KeyCode.VcLeft:
                    joueur.SeDeplacer("gauche");
                    break;
                case KeyCode.VcRight:
                    joueur.SeDeplacer("droite");
                    break;
                case KeyCode.VcUp:
                    joueur.SeDeplacer("haut");
                    break;
                case KeyCode.VcDown:
                    joueur.SeDeplacer("bas");
                    break;
                case (KeyCode)27:
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _carte.OverlayMenuPublic.Show();
                    });
                    break;
                case KeyCode.VcSpace:
                    if (_carte._dialogueContinue != null)
                    {
                        _carte._dialogueContinue.TrySetResult(true);
                    }
                    else
                    {
                        await joueur.LancerAction();
                    }
                    break;
            }
            OnJoueurDéplacé?.Invoke();
        }
    }
}
