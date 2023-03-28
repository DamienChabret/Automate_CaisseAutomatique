using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automate.Etats
{
    /// <summary>
    /// Attend que le produit soit posé 
    /// </summary>
    internal class EtatAttenteProduit : Etat
    {
        public EtatAttenteProduit(Caisse metier, Automate automate) : base(metier, automate)
        {
        }

        public override string Message => "Poser votre produit monsieur";

        public override void Action(Evenement e)
        {
            switch (e)
            {
                case Evenement.SCANNER:
                    // no-op
                    break;
                case Evenement.PAYER:
                    // no-op
                    break;
                case Evenement.RESET:
                    // no-op
                    break;
                case Evenement.POSER:
                    // no-op
                    break;
                case Evenement.INTERVENTION_ADMIN:
                    this.NotifyPropertyChanged("InterventionAdmin");
                    break;
            }
        }

        public override Etat Transition(Evenement e)
        {
            Etat etat = this;
            switch (e)
            {
                case Evenement.SCANNER:
                    break;
                case Evenement.PAYER:
                    // no-op
                    break;
                case Evenement.RESET:
                    // no-op
                    break;
                case Evenement.POSER:
                    // Vérifie que le poids est le bon
                    if(this.Metier.PoidsAttendu == this.Metier.PoidsBalance)
                    {
                        etat = new EnregistreProduit(this.Metier, this.Automate);
                    }
                    else
                    {
                        etat = new EtatProblemeProduit(this.Metier, this.Automate);
                    }
                    break;
                case Evenement.ENLEVER:
                    // no-op
                    break;
                case Evenement.INTERVENTION_ADMIN:
                    etat = new EtatSessionAdmin(this.Metier, this.Automate);
                    break;
            }
            return etat;
        }
    }
}
