using CaisseAutomatique.Model.Automate.Etats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automate
{
    /// <summary>
    /// Automate
    /// </summary>
    public class Automate : INotifyPropertyChanged
    {
        private Caisse metier;
        private Etat etat;

        public string Message
        {
            get => this.etat.Message;
        }

        /// <summary>
        /// Constructeur de l'automate
        /// </summary>
        /// <param name="metier"> Caisse </param>
        public Automate(Caisse metier)
        {
            this.metier = metier;
            etat = new EtatAttenteClient(this.metier, this);
        }

        public void Activer(Evenement e)
        {
            this.etat.Action(e);
            this.etat = etat.Transition(e);
            NotifyPropertyChanged("Message");
        }

        #region Notify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
