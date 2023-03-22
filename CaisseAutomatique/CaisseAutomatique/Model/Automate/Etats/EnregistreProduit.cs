using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automate.Etats
{
    internal class EnregistreProduit : Etat
    {
        public override string Message => "Scannez le produit suivant !";

        public EnregistreProduit(Caisse metier, Automate automate) : base(metier, automate) { }

        public override void Action(Evenement e)
        {
            switch (e)
            {
                case Evenement.SCANNER:
                    this.Metier.RegisterArticle();
                    break;
                case Evenement.PAYER:
                    this.Metier.ResetProduit();
                    break;
                case Evenement.ENLEVER:
                    // no-op
                    break;
                case Evenement.POSER:
                    // no-op
                    break;
            }
        }

        public override Etat Transition(Evenement e)
        {
            Etat etat = this;
            switch (e)
            {
                case Evenement.SCANNER:
                    if (!this.Metier.DernierArticleScanne.IsDenombrable)
                    {
                        etat = new EtatAttenteProduit(this.Metier, this.Automate);
                    }
                    else
                    {
                        etat = new EtatQuantiteSaisie(this.Metier, this.Automate);
                    }
                    break;
                case Evenement.PAYER:
                    etat = new EtatFin(this.Metier, this.Automate);
                    break;
                case Evenement.POSER:
                    // 
                    break;
                case Evenement.ENLEVER:
                    // Vérifie que le poids est le bon
                    if (this.Metier.PoidsAttendu == this.Metier.PoidsBalance)
                    {
                        etat = new EtatAttenteProduit(this.Metier, this.Automate);
                    }
                    
                    break;
            }
            return etat;
        }
    }
}
