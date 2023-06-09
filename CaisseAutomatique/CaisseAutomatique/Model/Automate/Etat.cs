﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automate
{
    /// <summary>
    /// Etat de l'automate
    /// </summary>
    public abstract class Etat : INotifyPropertyChanged
    {
        /// <summary>
        /// Message de l'état
        /// </summary>
        public abstract string Message
        {
            get;
        }

        /// <summary>
        /// Métier de l'état
        /// </summary>
        public Caisse Metier
        {
            get => metier;
        }
        private Caisse metier;

        /// <summary>
        /// Automate
        /// </summary>
        private Automate automate;

        public Automate Automate
        {
            get => automate;
        }

        /// <summary>
        /// Constructeur naturelle de l'état
        /// </summary>
        /// <param name="metier"> caisse</param>
        public Etat(Caisse metier, Automate automate)
        {
            this.metier = metier;
            this.automate = automate;
        }

        /// <summary>
        /// Transition de l'état
        /// </summary>
        /// <param name="e"> évènement qui gère la transition </param>
        /// <returns> le nouvelle état </returns>
        public abstract Etat Transition(Evenement e);

        /// <summary>
        /// Evènement qui gère l'action selon l'évènement reçu
        /// </summary>
        /// <param name="e"></param>
        public abstract void Action(Evenement e);


        #region NOTIFY
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
