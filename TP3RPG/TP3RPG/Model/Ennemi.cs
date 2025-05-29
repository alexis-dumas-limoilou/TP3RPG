using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3RPG.Pages;
using TP3RPG.Service;

namespace TP3RPG.Model
{
    public class Ennemi : ICombattants
    {
        private Carte _carte;
        public int Id { get; set; }
        public string Nom { get; set; }
        public int PV { get; set; }
        public int Degat { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Vitesse { get; set; } = 1;
        public int Direction { get; set; }

        public Ennemi(int id, string nom, Carte carte)
        {
            Id = id;
            Nom = nom;
            PV = 100;
            Degat = 20;
            _carte = carte;
        }

        public void Attaquer(Ennemi cible)
        {
            cible.PV -= Degat;
            if (cible.PV <= 0)
            {
                cible.EliminerCombattant();
            }
        }
        public void SeDeplacer(string direction)
        {
            int nouveauX = X;
            int nouveauY = Y;
            switch (direction)
            {
                case "gauche":
                    if (_carte.Ennemi.Direction != 4)
                        _carte.Ennemi.Direction = 4;
                    else
                        nouveauX = X - Vitesse;
                    break;
                case "droite":
                    if (_carte.Ennemi.Direction != 2)
                        _carte.Ennemi.Direction = 2;
                    else
                        nouveauX = X + Vitesse;
                    break;
                case "haut":
                    if (_carte.Ennemi.Direction != 1)
                        _carte.Ennemi.Direction = 1;
                    else
                        nouveauY = Y - Vitesse;
                    break;
                case "bas":
                    if (_carte.Ennemi.Direction != 3)
                        _carte.Ennemi.Direction = 3;
                    else
                        nouveauY = Y + Vitesse;
                    break;
            }
            if (_carte.GetTuile(nouveauX, nouveauY).Traversable && !_carte.PNJExiste(nouveauX, nouveauY))
            {
                X = nouveauX;
                Y = nouveauY;
                _carte.LancerEvenementTuile(X, Y);
            }
        }
        public void EliminerCombattant()
        {
            if (GameManager.CarteActuelle is CarteJeu carteJeu)
            {
                _carte.EnnemiVisible = false;
                carteJeu.CanvasEnnemi.InvalidateSurface();
            }
        }

        public async Task LancerAction()
        {
            int nouveauX = X;
            int nouveauY = Y;
            switch (Direction)
            {
                case 4:
                    nouveauX = X - Vitesse;
                    break;
                case 2:
                    nouveauX = X + Vitesse;
                    break;
                case 1:
                    nouveauY = Y - Vitesse;
                    break;
                case 3:
                    nouveauY = Y + Vitesse;
                    break;
            }
        }
    }
}
