using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompteBanque
{
    public abstract class Compte
    {
        public long NumeroCompte { get; private set; }
        public Client Proprietaire { get; private set; }
        private decimal _solde;
        public decimal Solde { 
            get { return Math.Round(_solde, 2); } 
            set { _solde = Math.Round(value, 2); } 
        }
        public ObservableCollection<string> ListeTransactions { get;  set; }
        private static long _compteur = 100000000;
        public Compte()
        {
            // Certains attributs/propriétés obligatoire doivent être
            // initialisés même dans le constructeur par défaut
            // notament les listes.
            NumeroCompte = ++_compteur;
            Solde = (decimal)0.0;
            ListeTransactions = new ObservableCollection<string>();
        }

        public Compte(Client proprietaire)
        {
            NumeroCompte = ++_compteur;
            Proprietaire = proprietaire;
            Solde = (decimal)0.0;
            ListeTransactions = new ObservableCollection<string>();
        }

        public virtual bool Deposer(decimal montant)
        {
            if (montant > 0)
            {
                Solde += montant;
                string transaction = $"{DateTime.Now} Dépôt : +{montant:C} dans {NumeroCompte}";
                ListeTransactions.Add(transaction);
                return true;
            }
            else
            {
                throw new Exception("Montant invalide pour un dépôt.");
            }
        }

        public virtual bool Retirer(decimal montant)
        {
            if (montant > 0 && montant <= Solde)
            {
                Solde -= montant;
                string transaction = $"{DateTime.Now} Retrait : -{montant:C} de {NumeroCompte}";
                ListeTransactions.Add(transaction);
                return true;
            }
            else
            {
                throw new Exception("Montant invalide ou fonds insuffisants.");
            }
        }

        public override string ToString()
        {
            return NumeroCompte.ToString();
        }

    }
}
