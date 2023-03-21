using System;
using System.Timers;

namespace CaisseAutomatique.Model.Automate.Etats
{
    internal class EtatFin : Etat
    {
        public override string Message => "Au revoir ! ;)";

        /// <summary>
        /// Timer 
        /// </summary>
        private static Timer timer;

        public EtatFin(Caisse metier, Automate automate) : base(metier, automate) {
            timer = null;

            if (timer == null)
            {
                timer = new Timer(2000);
                timer.Elapsed += Timer_Elasped;
                timer.AutoReset = false;
                timer.Start();
            }
        }

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
                    this.Metier.Reset();
                    break;
            }
        }

        public override Etat Transition(Evenement e)
        {
            Etat etat = this;
            switch (e)
            {
                case Evenement.SCANNER:
                    // no-op
                    break;
                case Evenement.PAYER:
                    // no-op
                    break;
                case Evenement.RESET:
                    etat = new EtatAttenteClient(this.Metier, this.Automate);
                    break;
            }
            return etat;
        }

        /// <summary>
        /// A la fin du timer
        /// </summary>
        private void Timer_Elasped(object? sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                this.Automate.Activer(Evenement.RESET);
            });
            timer.Dispose();
            timer = null;
        }
    }
}
