using SharpHook;
using SharpHook.Native;
using System.Diagnostics;
using TP3RPG.Model;

namespace TP3RPG.Assets
{
    public class Controls
    {
        private Joueur joueur;
        private EventSimulator simulator;
        private TaskPoolGlobalHook globalHook;
        public Controls(Joueur joueur)
        {
            this.joueur = joueur;
            globalHook = new TaskPoolGlobalHook();
            globalHook.KeyPressed += OnKeyDown;
            globalHook.RunAsync();
        }
        public event Action OnJoueurDéplacé;

        private void OnKeyDown(object sender, KeyboardHookEventArgs args)
        {
            if (joueur == null) return;

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
            }
            OnJoueurDéplacé?.Invoke();
        }
    }
}
