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
        public string Type { get; }
        public bool Traversable { get; }

        public Tuile(int x, int y, string type, bool traversable)
        {
            X = x;
            Y = y;
            Type = type;
            Traversable = traversable;
        }
    }
}
