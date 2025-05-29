using TP3RPG.Model;

namespace TP3RPG.Service
{
    public class QueteService
    {
        private static QueteService _instance;
        public static QueteService Instance => _instance ??= new QueteService();
        public List<Quete> Quetes { get; private set; } = new List<Quete>();

        public QueteService()
        {
            Quetes.Add(new Quete
            {
                Id = 1,
                Nom = "Tuer un rat",
                Description = "Le vieillard de la maison vous a demandé de tuer un rat qui le dérange.",
                Etat = 1
            });
        }

        public Quete ObtenirQuete(int id)
        {
            return Quetes.FirstOrDefault(q => q.Id == id);
        }
    }
}
