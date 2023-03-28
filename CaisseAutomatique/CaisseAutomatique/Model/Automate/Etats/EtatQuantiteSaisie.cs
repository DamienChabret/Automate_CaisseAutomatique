using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automate.Etats
{
    /// <summary>
    /// Etat quand on utilise la saisie
    /// </summary>
    public class EtatQuantiteSaisie : Etat
    {
        public EtatQuantiteSaisie(Caisse metier, Automate automate) : base(metier, automate) { }

        public override string Message => "Veuillez saisir la quantitée";

        public override void Action(Evenement e)
        {
            switch(e)
            {
                case Evenement.SAISIEQUANTITE:
                    this.Metier.RegisterArticle(this.Metier.QuantiteSaise);
                    this.NotifyPropertyChanged("ScanArticleDenombrable");
                    break;
                case Evenement.PAYER:
                    // no-op
                    break;
            }
        }

        public override Etat Transition(Evenement e)
        {
            Etat etat = this;
            switch (e)
            {
                case Evenement.SAISIEQUANTITE:
                    etat = new EtatAttenteProduit(this.Metier, this.Automate);
                    break;

            }
            return etat;
        }
    }
}
