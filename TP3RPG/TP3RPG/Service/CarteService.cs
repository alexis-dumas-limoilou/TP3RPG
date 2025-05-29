using TP3RPG.Model;
using TP3RPG.Pages;

namespace TP3RPG.Service
{
    public class CarteService
    {
        public static Carte CreerCarte(int idCarte)
        {
            Carte carte = new Carte();
            carte.Joueur = new Joueur("Nicolas", carte);
            carte.EvenementsTuiles.Clear();
            carte.Tuiles.Clear();

            switch (idCarte)
            {
                case 1:

                    for (int i = 0; i <= Carte.TailleCarte; i++)
                    {
                        carte.Tuiles.Add(new Tuile(0, i, "Barriere", false));
                        carte.Tuiles.Add(new Tuile(i, 0, "Barriere", false));
                        carte.Tuiles.Add(new Tuile(Carte.TailleCarte, i, "Barriere", false));
                        carte.Tuiles.Add(new Tuile(i, Carte.TailleCarte, "Barriere", false));
                    }

                    for (int i = 1; i < Carte.TailleCarte; i++)
                    {
                        for (int j = 1; j < Carte.TailleCarte; j++)
                        {
                            if (i >= Carte.MilieuCarte - 2 && j >= Carte.MilieuCarte - 2 && i <= Carte.MilieuCarte + 2 && j <= Carte.MilieuCarte + 2)
                            {
                                if ((i == Carte.MilieuCarte && j == Carte.MilieuCarte - 2) || (i >= Carte.MilieuCarte - 1 && i <= Carte.MilieuCarte + 1 && j == Carte.MilieuCarte - 1) || (j == Carte.MilieuCarte))
                                {
                                    carte.Tuiles.Add(new Tuile(i, j, "Toit", false));
                                }
                                else if (!(i == Carte.MilieuCarte && j == Carte.MilieuCarte + 2) && j > Carte.MilieuCarte)
                                {
                                    carte.Tuiles.Add(new Tuile(i, j, "Mur", false));
                                }
                                else if (!(i == Carte.MilieuCarte && j == Carte.MilieuCarte + 2))
                                {
                                    carte.Tuiles.Add(new Tuile(i, j, "Herbe", true));
                                }
                                else
                                {
                                    carte.Tuiles.Add(new Tuile(i, j, "PorteOuverte", true));
                                }
                            }
                            else
                            {
                                carte.Tuiles.Add(new Tuile(i, j, "Herbe", true));
                            }
                        }
                    }
                    carte.EvenementsTuiles[(10, 12)] = () => carte.DéclencherChangementCarte(2);
                    break;


                case 2:
                    for (int i = 0, j = 0; i <= Carte.TailleCarte; i++, j++)
                    {
                        carte.Tuiles.Add(new Tuile(10, 20, "PorteOuverte", true));
                       
                        carte.Tuiles.Add(new Tuile(0, i, "Mur", false));
                        carte.Tuiles.Add(new Tuile(i, 0, "Mur", false));
                        carte.Tuiles.Add(new Tuile(Carte.TailleCarte, i, "Mur", false));
                        if (!(i == 10))
                        {
                            carte.Tuiles.Add(new Tuile(i, Carte.TailleCarte, "Mur", false));
                        }
                    }
                    for (int i = 1; i < Carte.TailleCarte; i++)
                    {
                        for (int j = 1; j < Carte.TailleCarte; j++)
                        {
                            carte.Tuiles.Add(new Tuile(i, j, "Parquet", true));
                        }
                        
                    }
                    carte.EvenementsTuiles[(10, 20)] = () => carte.DéclencherChangementCarte(1);
                    break;
            }

            if (idCarte == 1)
            {
                carte.Joueur.X = 10;
                carte.Joueur.Y = 13;
            }
            else if (idCarte == 2)
            {
                carte.Joueur.X = 10;
                carte.Joueur.Y = 19;
                carte.PNJ = new PNJ(1, "Papi Muzot", carte);
                carte.PNJ.X = 15;
                carte.PNJ.Y = 12;
            }

            return carte;
        }
    }
}
