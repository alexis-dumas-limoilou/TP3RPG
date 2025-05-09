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

        public Carte()
        {
            Tuiles = new List<Tuile>();
            int tailleCarte = 20;
            int milieuCarte = tailleCarte/2;

            for(int i = 0; i<=tailleCarte; i++)
            {
                Tuiles.Add(new Tuile(0, i, "Barriere"));
                Tuiles.Add(new Tuile(i, 0, "Barriere"));
                Tuiles.Add(new Tuile(tailleCarte, i, "Barriere"));
                Tuiles.Add(new Tuile(i, tailleCarte, "Barriere"));
            }

            for(int i = 1; i<tailleCarte; i++)
            {
                for (int j = 1; j < tailleCarte; j++)
                {
                    if (i >= milieuCarte-2 && j >= milieuCarte-2 && i <= milieuCarte+2 && j <= milieuCarte+2)
                    {
                        if ((i == milieuCarte && j == milieuCarte - 2) || (i >= milieuCarte-1 && i<=milieuCarte+1 && j==milieuCarte-1) || (j==milieuCarte))
                        {
                            Tuiles.Add(new Tuile(i, j, "Toit"));
                        }
                        else if (!(i == milieuCarte && j == milieuCarte + 2) && j>milieuCarte)
                        {
                            Tuiles.Add(new Tuile(i, j, "Mur"));
                        }
                        else if(!(i == milieuCarte && j == milieuCarte + 2))
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
        }
    }
}