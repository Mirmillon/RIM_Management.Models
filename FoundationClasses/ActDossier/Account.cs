using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

//A set of financial transactions that are tracked and reported together with a single balance.
//Usage Notes:</i> Account can be used to represent the accumulated total of billable amounts for goods or services received, payments made for goods or services, 
//and debit and credit accounts between which financial transactions flow.
//Examples:Patient accounts, encounter accounts, cost centers, accounts receivable

namespace RIM_Management.Models.FoundationClasses.ActDossier
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string  AccountId { get; set; }
    }
}
