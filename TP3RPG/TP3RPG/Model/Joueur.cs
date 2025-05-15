using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3RPG.Model
{
    public class Joueur : ICombattants
    {
        public string Nom { get; set; }
        public int PV { get; set; }
        public int Degat { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Vitesse { get; set; } = 1; // Vitesse de déplacement

        public Joueur(string nom)
        {
            Nom = nom;
            PV = 100;
            Degat = 20;
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
            switch (direction)
            {
                case "gauche":
                    X -= Vitesse;
                    break;
                case "droite":
                    X += Vitesse;
                    break;
                case "haut":
                    Y -= Vitesse;
                    break;
                case "bas":
                    Y += Vitesse;
                    break;
            }
        }

        public void EliminerCombattant()
        {
            throw new NotImplementedException();
        }
    }
}
