using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automate
{
    /// <summary>
    /// Automate
    /// </summary>
    public class Automate
    {
        private Caisse metier;
        private Etat etat;

        /// <summary>
        /// Constructeur de l'automate
        /// </summary>
        /// <param name="metier"> Caisse </param>
        public Automate(Caisse metier)
        {
            this.metier = metier;
        }

        public void Activer(Evenement e)
        {

        }
    }
}
