using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3RPG.Pages;

namespace TP3RPG.Service
{
    public static class GameManager
    {
        public static CarteJeu CarteActuelle { get; set; }

        public static void ChangerCarte(int idCarte)
        {
            CarteActuelle = new CarteJeu(idCarte);
        }
    }
}
