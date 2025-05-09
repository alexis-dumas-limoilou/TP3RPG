using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3RPG.Model
{
    public class Tuile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Type { get; } // "Herbe", "Mur", "Sol"

        public Tuile(int x, int y, string type)
        {
            X = x;
            Y = y;
            Type = type;
        }
    }
}
