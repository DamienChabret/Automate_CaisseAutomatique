using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automate.Etats
{
    public class EtatAttenteClient : Etat
    {
        public EtatAttenteClient(Caisse metier) : base(metier)
        {
        }

        public override void Action(Evenement e)
        {
            // no-op
        }

        public override Etat Transition(Evenement e)
        {
            return new EtatAttenteClient(this.Metier);
        }
    }
}
