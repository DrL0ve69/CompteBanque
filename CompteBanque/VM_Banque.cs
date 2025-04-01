using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompteBanque
{
    public class VM_Banque : INotifyPropertyChanged
    {
        public ObservableCollection<Client> _lesClients;
        public ObservableCollection<Client> LesClients 
        { 
            get 
            {
                return _lesClients;
            } 
            set
            { 
                _lesClients = value;
                OnPropertyChanged(nameof(LesClients));
            }
        }

        public Client client { get; set; }

        private Client _clientS;
        public Client ClientSelect { 
            get { return _clientS; }
            set { 
                _clientS = value;
                OnPropertyChanged(nameof(ClientSelect));
                OnPropertyChanged(nameof(ClientSelect.ListeComptes));
            }
        }

        public Compte compte { get; set; }

        private Compte _cauth;
        public Compte compteAuth
        {
            get { return _cauth; }
            set {
                _cauth = value;
                OnPropertyChanged(nameof(compteAuth));
                OnPropertyChanged(nameof(compteAuth.ListeTransactions));
            }
        }

        public string NAS { get; set; }
        public string NIP { get; set; }

        public string Nip_Connexion { get; set; }

        public string Nip1 { get; set; }
        public string Nip2 { get; set; }

        public string Montant { get; set; }
        private string _typeCompte;
        public List<string> TypesComptes { get; set; } = new List<string> { "Chèque", "Épargne" , "Crédit"};
        public string SelectedTypeCompte
        {
            get { return _typeCompte; }
            set
            {
                _typeCompte = value;
                OnPropertyChanged(nameof(SelectedTypeCompte));
            }
        }

        public ICommand commandNouveauClient { get; set; }
        public ICommand commandNouveauCompte { get; set; }
        public ICommand commandConnexion { get; set; }
        public ICommand commandChangerNip { get; set; }
        public ICommand commandRetirer { get; set; }
        public ICommand commandDeposer { get; set; }
        public ICommand CommandCalculerInteret { get; set; }
        public ICommand CommandEnvoyerVirement { get; set; }

        public VM_Banque() 
        {
            LesClients = new ObservableCollection<Client>();
            client = new Client();
            ClientSelect = new Client();


            commandNouveauClient = new RelayCommand(NouveauClient);
            commandNouveauCompte = new RelayCommand(NouveauCompte);
            commandConnexion = new RelayCommand(Connexion);
            commandChangerNip = new RelayCommand(ChangerNip);
            commandDeposer = new RelayCommand(Deposer);
            commandRetirer = new RelayCommand(Retirer);
            CommandCalculerInteret = new RelayCommand(CalculerInteret);
            CommandEnvoyerVirement = new RelayCommand(EnvoyerVirement);
        }
        private void EnvoyerVirement()
        {
            if (compteAuth is CompteCheque && compte != compteAuth && compte != null)
            {
                CompteCheque cc = (CompteCheque)compteAuth;
                cc.EnvoyerVirement(decimal.Parse(Montant), compte);
                OnPropertyChanged(nameof(compteAuth));
                OnPropertyChanged(nameof(compte));
                MessageBox.Show("Virement effectué");
            }
            else
            {
                MessageBox.Show("Ce compte ne possède pas la fonction EnvoyerVirement()");
            }
        }
        private void CalculerInteret()
        {
            if (compteAuth is CompteEpargne)
            {
                CompteEpargne ce = (CompteEpargne)compteAuth;
                MessageBox.Show($"Intérêts calculés: {ce.CalculerInteret()}$");
                OnPropertyChanged(nameof(compteAuth));
            }
            else if(compteAuth is CompteCredit)
            {
                CompteCredit cc = (CompteCredit)compteAuth;
                MessageBox.Show($"Intérêts calculés: {cc.CalculerInteret()}$");
                OnPropertyChanged(nameof(compteAuth));
            }
            else
            {
                MessageBox.Show("Ce compte ne possède pas la fonction CalculInteret()");
            }
        }
        private void Retirer()
        {
            try
            {
                compteAuth.Retirer(decimal.Parse(Montant));
                OnPropertyChanged(nameof(compteAuth));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Authentification non valide ou " + ex.Message);
            }
        }

        private void Deposer()
        {
            try
            {
                compteAuth.Deposer(decimal.Parse(Montant));
                OnPropertyChanged(nameof(compteAuth));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Authentification non valide ou " + ex.Message);
            }
        }

        private void ChangerNip()
        {
            try
            {
                ClientSelect.ChangerNIP(Nip1, Nip2);
                MessageBox.Show("Le nip a bien été modifié");
            }
            catch (Exception ex) {
                MessageBox.Show("Authentification non valide");
            }
        }

        private void Connexion()
        {
            if (ClientSelect.Authentifier(Nip_Connexion))
            {
                compteAuth = compte;
            }
            else
            {
                MessageBox.Show("L'authentification a échoué.");
                compteAuth = null;
            }
        }

        private void NouveauCompte()
        {

            if (ClientSelect != null && SelectedTypeCompte != null)
            {
                ClientSelect.OuvrirCompte(SelectedTypeCompte);
                MessageBox.Show("Le compte a été créé");
                Client copie = ClientSelect;
                ClientSelect = null; // forcer la mise à jour du combobox
                ClientSelect = copie;
            }
            else
            {
                MessageBox.Show("Vous devez d'abord sélectionner un client");
            }
        }

        private void NouveauClient()
        {
            Client c = new Client(client);
            c.ChangerNIP(null, NIP);
            LesClients.Add(c);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
