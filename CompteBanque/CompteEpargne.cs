using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompteBanque;

public class CompteEpargne : Compte
{
    public decimal TauxInteret {  get; set; } = 0.0025m;
    public CompteEpargne() : base()
    {
    }
    public CompteEpargne(Client proprietaire) : base(proprietaire)
    {
        TauxInteret = 0.0035m;
    }
    public CompteEpargne(Client proprietaire, decimal tauxInteret) : base(proprietaire)
    {
        TauxInteret = tauxInteret;
    }
    public decimal CalculerInteret()
    {
        ListeTransactions.Add($"{DateTime.Now} Intérêt : +{Solde * TauxInteret:C} dans {NumeroCompte}");
        return Solde * TauxInteret;
    }
}
