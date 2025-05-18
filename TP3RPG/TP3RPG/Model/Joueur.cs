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
            X = 3;
            Y = 5;
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
            Carte _carte = new();
            int nouveauX;
            int nouveauY;
            switch (direction)
            {
                case "gauche":
                    nouveauX = X - Vitesse;
                    if (_carte.GetTuile(nouveauX,Y).Traversable)
                        X = nouveauX;
                    break;
                case "droite":
                    nouveauX = X + Vitesse;
                    if (_carte.GetTuile(nouveauX, Y).Traversable)
                        X = nouveauX;
                    break;
                case "haut":
                    nouveauY = Y - Vitesse;
                    if (_carte.GetTuile(X, nouveauY).Traversable)
                        Y = nouveauY;
                    break;
                case "bas":
                    nouveauY = Y + Vitesse;
                    if (_carte.GetTuile(X, nouveauY).Traversable)
                        Y = nouveauY;
                    break;
            }
        }

        public void EliminerCombattant()
        {
            throw new NotImplementedException();
        }
    }
}
