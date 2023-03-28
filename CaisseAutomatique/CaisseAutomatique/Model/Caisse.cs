using CaisseAutomatique.Model.Articles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model 
{
    /// <summary>
    /// Caisse automatique (couche métier)
    /// </summary>
    public class Caisse : INotifyPropertyChanged
    {
        /// <summary>
        /// Liste des articles enregistrés
        /// </summary>
        private List<Article> articles;
        public List<Article> Articles => articles;

        /// <summary>
        /// Dernier article scanné (dans un vrai modèle, seule la référence est connue et on utiliserait une Bdd pour retrouver l'article)
        /// </summary>
        public Article DernierArticleScanne => dernierArticleScanne;
        private Article dernierArticleScanne;

        /// <summary>
        /// Quantité saisie pour les articles dénombrables
        /// </summary>
        public int QuantiteSaise => quantiteSaisie;
        private int quantiteSaisie;
        

        /// <summary>
        /// Poids des objets sur la balance
        /// </summary>
        public double PoidsBalance => poidsBalance;
        private double poidsBalance;

        /// <summary>
        /// Somme déjà payée par l'utilisateur
        /// </summary>
        public double SommePayee => sommePayee;
        private double sommePayee;

        /// <summary>
        /// Poids attendu dans la balance
        /// </summary>
        public double PoidsAttendu
        {
            get
            {
                double res = 0;
                foreach (Article article in articles) res += article.Poids;
                return res;
            }
        }

        /// <summary>
        /// Prix total
        /// </summary>
        public double PrixTotal
        {
            get
            {
                double res = 0;
                foreach (Article article in articles) res += article.Prix;
                return res;
            }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Caisse()
        {
            this.articles = new List<Article>();
            this.dernierArticleScanne = null;
            this.poidsBalance = 0;
            this.sommePayee = 0;
        }

        /// <summary>
        /// Saisie d'une quantité pour un article dénombrable
        /// </summary>
        /// <param name="valeur">Valeur de la quantité</param>

        public void SaisieQuantite(int valeur)
        {
            this.quantiteSaisie = valeur;
        }

        /// <summary>
        /// Scan d'un article mais ne l'enregistre pas
        /// </summary>
        /// <param name="article">L'article scanné</param>
        public void ScanArticle(Article article)
        {
            this.dernierArticleScanne = article;
        }

        /// <summary>
        /// Met à jour la caisse
        /// </summary>
        public void ResetProduit()
        {
            // Si plus d'un produits
            if(this.articles.Count > 0)
            {
                this.articles.Clear();
                this.NotifyPropertyChanged("Articles");
            }
            
        }

        /// <summary>
        /// Reset tout
        /// </summary>
        public void Reset()
        {
            this.articles = new List<Article>();
            this.dernierArticleScanne = null;
            this.poidsBalance = 0;
            this.sommePayee = 0;
            this.NotifyPropertyChanged("Reset");
        }

        /// <summary>
        /// Ajoute un articles
        /// </summary>
        public void AddArticle(Article article)
        {
            this.poidsBalance = this.poidsBalance + article.Poids;
        }

        /// <summary>
        /// Enlève un article
        /// </summary>
        public void RemoveArticle(Article article)
        {
            this.poidsBalance = this.PoidsBalance - article.Poids;
        }

        /// <summary>
        /// Enregistre un article
        /// </summary>
        public void RegisterArticle(int quantiteSaisie = 1)
        {
            
            for(int i = 0; i < quantiteSaisie; i++)
            {
                this.articles.Add(this.DernierArticleScanne);
            }
            this.NotifyPropertyChanged("Articles");
        }

        /// <summary>
        /// Annule le dernier article enregistré
        /// </summary>
        public void CancelLastArticle()
        {
            if(this.articles.Count > 0)
            {
                this.articles.RemoveAt(this.articles.Count - 1);
            }
        }

        /// <summary>
        /// Pattern d'observable
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
