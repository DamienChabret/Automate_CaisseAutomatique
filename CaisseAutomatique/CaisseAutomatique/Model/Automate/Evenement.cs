using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automate
{
    /// <summary>
    /// Evenement capable de faire changer l'état
    /// </summary>
    public enum Evenement
    {
        SCANNER,
        POSER,
        PAYER,
        RESET,
        ENLEVER,
        SAISIEQUANTITE,
        INTERVENTION_ADMIN,
        ANNULER_COMMANDE,
        ANNULER_DERNIERARTICLE,
        QUITTER_ADMINISTRATION
    }
}
