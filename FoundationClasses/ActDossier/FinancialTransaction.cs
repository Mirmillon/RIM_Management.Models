using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RIM_Management.Models.FoundationClasses.ActDossier
{
    public class FinancialTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string FinancialTransactionId { get; set; }
    }
}
