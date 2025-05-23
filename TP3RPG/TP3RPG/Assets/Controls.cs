﻿using SharpHook;
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
        public Controls(Joueur joueur, CarteJeu carte)
        {
            this.joueur = joueur;
            this._carte = carte;
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
                case (KeyCode)27:
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _carte.OverlayMenuPublic.Show();
                    });
                    break;
                case KeyCode.VcSpace:
                    
                    break;
            }
            OnJoueurDéplacé?.Invoke();
        }
    }
}
