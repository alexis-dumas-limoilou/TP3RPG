using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3RPG.Model
{
    public class Joueur : ICombattants
    {
        private Carte _carte;
        public string Nom { get; set; }
        public int PV { get; set; }
        public int Degat { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Vitesse { get; set; } = 1;

        public Joueur(string nom, Carte carte)
        {
            Nom = nom;
            PV = 100;
            Degat = 20;
            _carte = carte;
        }

        public void Attaquer(ICombattants cible)
        {
            cible.PV -= Degat;
            if (cible.PV <= 0)
            {
                cible.EliminerCombattant();
            }
        }
        public void SeDeplacer(string direction)
        {
            Debug.WriteLine($"🎮 Avant déplacement : ({X}, {Y})");
            int nouveauX=X;
            int nouveauY=Y;
            switch (direction)
            {
                case "gauche":
                    nouveauX = X - Vitesse;
                    break;
                case "droite":
                    nouveauX = X + Vitesse;
                    break;
                case "haut":
                    nouveauY = Y - Vitesse;
                    break;
                case "bas":
                    nouveauY = Y + Vitesse;
                    break;
            }
            if (_carte.GetTuile(nouveauX, nouveauY).Traversable)
            {
                Debug.WriteLine("X :"+ nouveauX + " Y : " + nouveauY);
                X = nouveauX;
                Y = nouveauY;
                _carte.LancerEvenementTuile(X, Y);
            }
        }

        public void EliminerCombattant()
        {
            throw new NotImplementedException();
        }
    }
}
