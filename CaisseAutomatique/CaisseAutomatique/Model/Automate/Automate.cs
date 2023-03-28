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

        public Etat EtatCourant
        {
            get => etat;
        }

        /// <summary>
        /// Constructeur de l'automate
        /// </summary>
        /// <param name="metier"> Caisse </param>
        public Automate(Caisse metier)
        {
            this.metier = metier;
            etat = new EtatAttenteClient(this.metier, this);
            this.etat.PropertyChanged += EtatCourant_PropertyChanged;
        }

        public void Activer(Evenement e)
        {
            this.etat.Action(e);
            this.etat = etat.Transition(e);
            this.etat.PropertyChanged += EtatCourant_PropertyChanged;
            NotifyPropertyChanged("Message");
        }

        /// <summary>
        /// Pas sur 
        /// </summary>
        public void EtatCourant_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ScanArticleDenombrable")
                NotifyPropertyChanged("ScanArticleDenombrable");
            if(e.PropertyName == "InterventionAdmin")
                NotifyPropertyChanged("InterventionAdmin");
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
