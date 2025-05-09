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
            Tuiles = new List<Tuile>
            {
                new Tuile(0, 0, "Herbe"),
                new Tuile(1, 0, "Mur"),
                new Tuile(0, 1, "Sol"),
                new Tuile(1, 1, "Herbe")
            };


        }

    }
}
