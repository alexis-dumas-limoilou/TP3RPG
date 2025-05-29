namespace TP3RPG.Model
{
    public class PNJ
    {
        private Carte _carte;
        public int Id { get; set; }
        public string Nom { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Vitesse { get; set; }

        public PNJ(int id, string nom, Carte carte)
        {
            Id = id;
            Nom = nom;
            _carte = carte;
        }

        public void SeDeplacer(string direction)
        {
            int nouveauX = X;
            int nouveauY = Y;
            switch (direction)
            {
                case "gauche":
                    nouveauX = X - Vitesse;
                    break;
                case "droite":
                    nouveauX = X + Vitesse;
                    break;
                case "haut":
                    nouveauY = Y - Vitesse;
                    break;
                case "bas":
                    nouveauY = Y + Vitesse;
                    break;
            }
            if (_carte.GetTuile(nouveauX, nouveauY).Traversable && !_carte.PNJExiste(nouveauX, nouveauY))
            {
                X = nouveauX;
                Y = nouveauY;
                _carte.LancerEvenementTuile(X, Y);
            }
        }

       


    }
}