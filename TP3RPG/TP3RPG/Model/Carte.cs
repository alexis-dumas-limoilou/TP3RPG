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
                Tuiles.Add(new Tuile(0, i, "Barriere", false));
                Tuiles.Add(new Tuile(i, 0, "Barriere", false));
                Tuiles.Add(new Tuile(TailleCarte, i, "Barriere", false));
                Tuiles.Add(new Tuile(i, TailleCarte, "Barriere", false));
            }

            for(int i = 1; i<TailleCarte; i++)
            {
                for (int j = 1; j < TailleCarte; j++)
                {
                    if (i >= MilieuCarte-2 && j >= MilieuCarte-2 && i <= MilieuCarte+2 && j <= MilieuCarte+2)
                    {
                        if ((i == MilieuCarte && j == MilieuCarte - 2) || (i >= MilieuCarte-1 && i<=MilieuCarte+1 && j==MilieuCarte-1) || (j==MilieuCarte))
                        {
                            Tuiles.Add(new Tuile(i, j, "Toit", false));
                        }
                        else if (!(i == MilieuCarte && j == MilieuCarte + 2) && j>MilieuCarte)
                        {
                            Tuiles.Add(new Tuile(i, j, "Mur", false));
                        }
                        else if(!(i == MilieuCarte && j == MilieuCarte + 2))
                        {
                            Tuiles.Add(new Tuile(i, j, "Herbe", true));
                        }
                        else
                        {
                            Tuiles.Add(new Tuile(i, j, "Default", true));
                        }
                    }
                    else 
                    { 
                        Tuiles.Add(new Tuile(i, j, "Herbe", true));
                    }
                }
            }
            Joueur = new Joueur("Nicolas", this);

        }
        public Tuile GetTuile(int x, int y)
        {
            return Tuiles.FirstOrDefault(t => t.X == x && t.Y == y);
        }

    }
}