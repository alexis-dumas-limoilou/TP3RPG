using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3RPG.Pages;
using TP3RPG.Service;

namespace TP3RPG.Model
{
    public class Joueur : ICombattants
    {
        private Carte _carte;
        public string Nom { get; set; }
        public int PV { get; set; }
        public int Degat { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Vitesse { get; set; } = 1;
        public int Direction { get; set; }

        public Joueur(string nom, Carte carte)
        {
            Nom = nom;
            PV = 100;
            Degat = 20;
            _carte = carte;
        }

        public void Attaquer(Ennemi cible)
        {
            cible.PV -= Degat;
            if (cible.PV <= 0)
            {
                cible.EliminerCombattant();
            }
        }
        public void SeDeplacer(string direction)
        {
            int nouveauX = X;
            int nouveauY = Y;
            switch (direction)
            {
                case "gauche":
                    if (_carte.Joueur.Direction != 4)
                        _carte.Joueur.Direction = 4;
                    else
                        nouveauX = X - Vitesse;
                    break;
                case "droite":
                    if (_carte.Joueur.Direction != 2)
                        _carte.Joueur.Direction = 2;
                    else
                        nouveauX = X + Vitesse;
                    break;
                case "haut":
                    if (_carte.Joueur.Direction != 1)
                        _carte.Joueur.Direction = 1;
                    else
                        nouveauY = Y - Vitesse;
                    break;
                case "bas":
                    if (_carte.Joueur.Direction != 3)
                        _carte.Joueur.Direction = 3;
                    else
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
        public void EliminerCombattant()
        {
            throw new NotImplementedException();
        }

        public async Task LancerAction()
        {
            int nouveauX = X;
            int nouveauY = Y;
            switch (Direction)
            {
                case 4:
                    nouveauX = X - Vitesse;
                    break;
                case 2:
                    nouveauX = X + Vitesse;
                    break;
                case 1:
                    nouveauY = Y - Vitesse;
                    break;
                case 3:
                    nouveauY = Y + Vitesse;
                    break;
            }
            if (_carte.PNJExiste(nouveauX, nouveauY))
               await LancerConversation(_carte.PNJ.Id);
            else if (_carte.EnnemiExiste(nouveauX, nouveauY))
                Attaquer(_carte.Ennemi);
        }

        public async Task LancerConversation(int idPNJ)
        {
            CarteJeu carteJeu = GameManager.CarteActuelle;
            switch (idPNJ)
            {
                case 1:
                     Quete quete = QueteService.Instance.ObtenirQuete(1);

                    if (quete != null)
                    {
                        if (!carteJeu.DialogueBox.IsVisible)
                        {
                            if (quete.Etat == 3)
                                await carteJeu.AfficherDialogue("\"Merci à toi, jeune homme ! tu peux quitter ma maison, maintenant !\"");
                            else if (quete.Etat == 2)
                                await carteJeu.AfficherDialogue("\"Et ben alors ? Tu ne vois pas que le rat est juste là ?\"");
                            else
                            {
                                await carteJeu.AfficherDialogue("\"Puisque tu t'invites chez les gens, pourrais-tu me rendre service ?\"");

                                int reponse = await carteJeu.AfficherDialogueAvecChoix(new List<string> {"Avec plaisir !", "Non merci, je ne fais que squatter" });

                                if (reponse==0)
                                {
                                    // L'utilisateur a répondu Oui
                                    quete.Etat = 2;
                                    await carteJeu.AfficherDialogue("\"Il y a une sale bête qui se permet de rentrer chez moi, et non je ne parle pas de toi.\"");
                                    carteJeu.AjouterMonstre();
                                    await carteJeu.AfficherDialogue("\"Ah ! le revoila ! Je compte sur toi pour m'en débarrasser !\"");
                                }
                                else
                                {
                                    // L'utilisateur a répondu Non
                                    await carteJeu.AfficherDialogue("\"Mais qu'est-ce que c'est que ces manières ?\"");
                                }
                            }
                        }
                        else
                        {
                            await carteJeu.FermerDialogue();
                        }
                    }
                    break;
            }
        }

        
    }
}