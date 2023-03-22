using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automate.Etats
{
    public class EtatAttenteClient : Etat
    {
        public override string Message => "Bonjour, scannez votre premier article !";

        public EtatAttenteClient(Caisse metier, Automate automate) : base(metier, automate) { }

        public override void Action(Evenement e)
        {
            switch (e)
            {
                case Evenement.SCANNER:
                    this.Metier.RegisterArticle();
                    this.NotifyPropertyChanged("ScanArticleDenombrable");
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
            }
            return etat;
        }
    }
}
