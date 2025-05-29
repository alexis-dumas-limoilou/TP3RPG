using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3RPG.Service;

namespace TP3RPG.Model
{
    public class Carte
    {
        public List<Tuile> Tuiles { get; set; } = new List<Tuile>();
        static public int TailleCarte = 20;
        static public int MilieuCarte = TailleCarte / 2;
        public Dictionary<(int, int), Action> EvenementsTuiles=new Dictionary<(int, int), Action>();
        public event Action<int> OnChangementCarte;
        public bool EnnemiVisible { get; set; } = false;

        public Joueur Joueur { get; set; }
        public PNJ PNJ { get; set; }
        public Ennemi Ennemi { get; set; }

        public Tuile GetTuile(int x, int y)
        {
            return Tuiles.FirstOrDefault(t => t.X == x && t.Y == y);
        }

        public void LancerEvenementTuile(int x, int y)
        {
            if (EvenementsTuiles.TryGetValue((x, y), out Action evenement))
            {
                evenement.Invoke();
            }
        }
        public void DéclencherChangementCarte(int idCarte)
        {
            OnChangementCarte?.Invoke(idCarte);
        }
        public bool PNJExiste(int x, int y)
        {
            if(PNJ !=null)
                return (PNJ.X == x && PNJ.Y == y);
            else
                return false;
        }

        public bool EnnemiExiste(int x, int y)
        {
            if (Ennemi != null)
                return (Ennemi.X == x && Ennemi.Y == y);
            else
                return false;
        }

       
    }
}