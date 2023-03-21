using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automate.Etats
{
    internal class Etat2 : Etat
    {
        public override string Message => "Salut message !";

        public Etat2(Caisse metier) : base(metier) { }

        public override void Action(Evenement e)
        {
            throw new NotImplementedException();
        }

        public override Etat Transition(Evenement e)
        {
            throw new NotImplementedException();
        }
    }
}
