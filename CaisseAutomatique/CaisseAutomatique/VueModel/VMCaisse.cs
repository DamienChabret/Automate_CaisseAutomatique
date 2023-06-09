﻿using CaisseAutomatique.Model;
using CaisseAutomatique.Model.Articles;
using CaisseAutomatique.Model.Articles.Realisations;
using CaisseAutomatique.Model.Automate;
using CaisseAutomatique.Vue;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CaisseAutomatique.VueModel
{
    /// <summary>
    /// Vue-Model de la caisse automatique
    /// </summary>
    public class VMCaisse : INotifyPropertyChanged
    {
        /// <summary>
        /// Automate
        /// </summary>
        private Automate automate;

        /// <summary>
        /// Message de l'automate
        /// </summary>
        public string Message => automate.Message;

        /// <summary>
        /// La caisse automatique (couche métier)
        /// </summary>
        private Caisse metier;

        /// <summary>
        /// Liste des articles de la caisse
        /// </summary>
        public ObservableCollection<Article> Articles { get=> articles; set => articles = value; }
        private ObservableCollection<Article> articles;

        /// <summary>
        /// La caisse est-elle disponible pour un nouveau client
        /// </summary>
        private bool estDisponible;

        public bool EstDisponible 
        { 
            get => estDisponible;
            set
            {
                estDisponible = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        public VMCaisse()
        {
            this.EstDisponible = true;
            this.metier = new Caisse();
            this.metier.PropertyChanged += Metier_PropertyChanged;
            this.articles = new ObservableCollection<Article>();
            this.AjouterLigneTotalEtResteAPayer();
            this.automate = new Automate(this.metier);
            this.automate.PropertyChanged += Automate_PropertyChanged;
        }

        /// <summary>
        /// Ajout des lignes "Total" et "Reste à payer" dans la facture
        /// </summary>
        private void AjouterLigneTotalEtResteAPayer()
        {
            this.Articles.Add(new ArticleVirtuel("Total", this.metier.PrixTotal));
            this.Articles.Add(new ArticleVirtuel("Reste à payer : ", this.metier.PrixTotal - this.metier.SommePayee));
        }

        /// <summary>
        /// Modification du métier observée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Metier_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Articles")
            {
                this.articles.Clear();
                foreach (Article article in this.metier.Articles) this.Articles.Add(article);
                this.AjouterLigneTotalEtResteAPayer();
            }
            else if(e.PropertyName =="SommePayee")
            {
                if(this.Articles.Count > 0)
                {
                    this.Articles.RemoveAt(this.Articles.Count - 1);
                    this.Articles.Add(new ArticleVirtuel("Reste à payer : ", this.metier.PrixTotal - this.metier.SommePayee));
                }
            }
            else if (e.PropertyName == "Reset")
            {
                this.articles.Clear();
                foreach (Article article in this.metier.Articles) this.Articles.Add(article);
                this.AjouterLigneTotalEtResteAPayer();
                this.EstDisponible = true;
            }
        }

        /// <summary>
        /// Ouvrir l'écran de sélection des quantités pour un article dénombrable
        /// </summary>
        private void OuvrirEcranSelectionQuantite()
        {
            new EcranSelectionQuantite(this).Show();
        }

        /// <summary>
        /// Ouvrir l'écran d'administration
        /// </summary>
        private void OuvrirEcranAdministration()
        {
            new EcranAdministration(this).Show();
        }

        /// <summary>
        /// L'utilisateur tente de scanner un produit
        /// </summary>
        /// <param name="vueArticle">Vue de l'article scanné</param>
        public void PasseUnArticleDevantLeScannair(VueArticle vueArticle)
        {
            this.metier.ScanArticle(vueArticle.Article);
            this.automate.Activer(Evenement.SCANNER);
        }

        /// <summary>
        /// L'utilisateur pose un article sur la balance
        /// </summary>
        /// <param name="vueArticle">Vue de l'article posé sur la balance</param>
        public void PoseUnArticleSurLaBalance(VueArticle vueArticle)
        {
            this.metier.AddArticle(vueArticle.Article);
            this.automate.Activer(Evenement.POSER);   
        }

        /// <summary>
        /// L'utilisateur enlève un article de la balance
        /// </summary>
        /// <param name="vueArticle">Vue de l'article enlevé de la balance</param>
        public void EnleveUnArticleDeLaBalance(VueArticle vueArticle)
        {
            this.metier.RemoveArticle(vueArticle.Article);
            this.automate.Activer(Evenement.ENLEVER);
        }

        /// <summary>
        /// L'utilisateur saisit un nombre d'articles dénombrables
        /// </summary>
        /// <param name="nbArticle">Nombre d'articles</param>
        public void SaisirNombreArticle(int nbArticle)
        {
            this.metier.SaisieQuantite(nbArticle);
            this.automate.Activer(Evenement.SAISIEQUANTITE);
        }

        /// <summary>
        /// L'utilisateur essaye de payer
        /// </summary>
        public void Paye()
        {
            this.automate.Activer(Evenement.PAYER);
        }

        /// <summary>
        /// Un administrateur active le mode administrateur
        /// </summary>
        public void DebutModeAdministration()
        {
            this.automate.Activer(Evenement.INTERVENTION_ADMIN);
        }

        /// <summary>
        /// Un administrateur termine le mode administrateur
        /// </summary>
        public void FinModeAdministration()
        {
            this.automate.Activer(Evenement.QUITTER_ADMINISTRATION);
        }

        /// <summary>
        /// L'administrateur annule le dernier article
        /// </summary>
        public void AnnuleDernierArticle()
        {
            this.automate.Activer(Evenement.ANNULER_DERNIERARTICLE);
        }

        /// <summary>
        /// L'administrateur annule tous les articles
        /// </summary>
        public void AnnuleTousLesArticles()
        {
            this.automate.Activer(Evenement.ANNULER_COMMANDE);
        }

        private void Automate_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // Message changé
            if (e.PropertyName == "Message") this.NotifyPropertyChanged("Message");

            // Scan article dénombrable
            if (e.PropertyName == "ScanArticleDenombrable")
            {
                this.OuvrirEcranSelectionQuantite();
            }

            // Intervention admin
            if (e.PropertyName == "InterventionAdmin") 
            {
                this.OuvrirEcranAdministration();
            }
        }

        #region Notify
        /// <summary>
        /// Evènement d'observation
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
