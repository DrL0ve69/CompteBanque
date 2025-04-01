using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompteBanque;

public class CompteCheque : Compte
{
    public string Succursuale { get; set; } = string.Empty;
    public CompteCheque() : base()
    {
    }
    public CompteCheque(Client proprietaire) : base(proprietaire)
    {
    }
    public CompteCheque(Client proprietaire, string succursale) : base(proprietaire)
    {
        Succursuale = succursale;
    }
    public void CommanderChqeue() { }
    public void PayerFacture(decimal montant, string numeroFacture) 
    {
        Solde -= montant;
        ListeTransactions.Add($"{DateTime.Now} Facture : -{montant:C} de {NumeroCompte} pour {numeroFacture}");
        // Ajouter le lien avec la facture
    }
    public void EnvoyerVirement(decimal montant, Compte compte)
    {
        Solde -= montant;
        compte.Solde += montant;
        ListeTransactions.Add($"{DateTime.Now} Virement : -{montant:C} de {NumeroCompte} à {compte.NumeroCompte}");
        compte.ListeTransactions.Add($"{DateTime.Now} Virement : +{montant:C} de {NumeroCompte} à {compte.NumeroCompte}");
        // Ajouter le lien avec la facture
    }
}
