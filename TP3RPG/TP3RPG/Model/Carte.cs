using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3RPG.Model
{
    public class Carte
    {
        public List<Tuile> Tuiles { get; private set; } = new List<Tuile>();
        static public int TailleCarte = 20;
        static public int MilieuCarte = TailleCarte / 2;
        public Joueur Joueur { get; private set; }
        public Carte()
        {
            Tuiles = new List<Tuile>();

            for(int i = 0; i<=TailleCarte; i++)
            {
                Tuiles.Add(new Tuile(0, i, "Barriere"));
                Tuiles.Add(new Tuile(i, 0, "Barriere"));
                Tuiles.Add(new Tuile(TailleCarte, i, "Barriere"));
                Tuiles.Add(new Tuile(i, TailleCarte, "Barriere"));
            }

            for(int i = 1; i<TailleCarte; i++)
            {
                for (int j = 1; j < TailleCarte; j++)
                {
                    if (i >= MilieuCarte-2 && j >= MilieuCarte-2 && i <= MilieuCarte+2 && j <= MilieuCarte+2)
                    {
                        if ((i == MilieuCarte && j == MilieuCarte - 2) || (i >= MilieuCarte-1 && i<=MilieuCarte+1 && j==MilieuCarte-1) || (j==MilieuCarte))
                        {
                            Tuiles.Add(new Tuile(i, j, "Toit"));
                        }
                        else if (!(i == MilieuCarte && j == MilieuCarte + 2) && j>MilieuCarte)
                        {
                            Tuiles.Add(new Tuile(i, j, "Mur"));
                        }
                        else if(!(i == MilieuCarte && j == MilieuCarte + 2))
                        {
                            Tuiles.Add(new Tuile(i, j, "Herbe"));
                        }
                    }
                    else 
                    { 
                        Tuiles.Add(new Tuile(i, j, "Herbe"));
                    }
                }
            }
            Joueur = new Joueur("Nicolas")
            {
                X = 3,
                Y = 5
            };

        }
    }
}