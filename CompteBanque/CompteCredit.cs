using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompteBanque;

public class CompteCredit : Compte
{
    public decimal Limite { get; set; } = 10_000m;
    public decimal TauxInteret { get; set; } = 0.18m;
    public CompteCredit() : base()
    {
    }
    public CompteCredit(Client proprietaire) : base(proprietaire)
    {
    }
    public CompteCredit(Client proprietaire, decimal limite, decimal tauxInteret) : base(proprietaire)
    {
        Limite = limite;
        TauxInteret = tauxInteret;
    }
    public decimal CalculerInteret() 
    {  
        ListeTransactions.Add($"{DateTime.Now} Intérêt : +{Solde * TauxInteret:C} dans {NumeroCompte}");
        return Solde * TauxInteret;
    }
}
