using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automate.Etats
{
    internal class EtatProblemeProduit : Etat
    {
        public EtatProblemeProduit(Caisse metier, Automate automate) : base(metier, automate)
        {
        }

        public override string Message => "Problème poids";

        public override void Action(Evenement e)
        {
            switch (e)
            {
                case Evenement.POSER:
                    // no-op
                    break;
                case Evenement.ENLEVER:
                    // no-op
                    break;
            }
        }

        public override Etat Transition(Evenement e)
        {
            Etat etat = this;
            switch(e)
            {
                case Evenement.POSER:
                    // Vérifie que le poids est le bon
                    if (this.Metier.PoidsAttendu == this.Metier.PoidsBalance)
                    {
                        etat = new EnregistreProduit(this.Metier, this.Automate);
                    }
                    break;
                case Evenement.ENLEVER:
                    // Vérifie que le poids est le bon
                    if (this.Metier.PoidsAttendu == this.Metier.PoidsBalance)
                    {
                        etat = new EtatAttenteClient(this.Metier, this.Automate);
                    }
                    break;
            }
            return etat;
        }
    }
}
