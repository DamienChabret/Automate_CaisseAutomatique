using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automate.Etats
{
    public class EtatSessionAdmin : Etat
    {
        public EtatSessionAdmin(Caisse metier, Automate automate) : base(metier, automate)
        {
        }

        public override string Message => "Session Administrateur mon petit filou";

        public override void Action(Evenement e)
        {
            switch (e)
            {
                case Evenement.ANNULER_COMMANDE:
                    this.Metier.Reset();
                    break;
                case Evenement.ANNULER_DERNIERARTICLE:
                    this.Metier.CancelLastArticle();
                    break;
            }
        }

        public override Etat Transition(Evenement e)
        {
            Etat etat = this;
            switch (e)
            {
                case Evenement.QUITTER_ADMINISTRATION:
                    // Vérifie que le poids est le bon
                    if (this.Metier.PoidsAttendu == this.Metier.PoidsBalance)
                    {
                        etat = new EnregistreProduit(this.Metier, this.Automate);
                    }
                    else
                    {
                        etat = new EtatProblemeProduit(this.Metier, this.Automate);
                    }
                    break;
            }
            return etat;
        }
    }
}
