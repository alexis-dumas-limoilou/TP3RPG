using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3RPG.Model
{
    public interface ICombattants
    {
        string Nom { get; set; }
        int PV { get; set; }
        int Degat { get; set; }
        int X {  get; set; }
        int Y { get; set; }
        int Vitesse { get; set; }

        void SeDeplacer(string direction);
        void Attaquer(ICombattants cible);
        void EliminerCombattant();
    }
}
