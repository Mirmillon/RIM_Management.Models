using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Text;

//A message that provides information about the communication, parsing or formal (non-business-rule) validation of the message being acknowledged.
namespace RIM_Management.Models.MessageCommunicationsControl.MessageControl
{
    public class AcknowledgementDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AcknowledgementDetailId { get; set; }

        [Required]
        //The kind of information specified in the acknowledgement message.Examples: Error, warning, information.
        public string TypeCode { get; set; }

        //The type of acknowledgement, from an enumerated set of acknowledgement types.
        //Examples: Required attribute missing; unsupported interaction; invalid code system in CNE.
        //Design Comment: Original examples seem to indicate text, not code, by including specific attributes, dates. New examples supplied from concept domain.
        public Code Detail { get; set; }


        //	<body><p>Additional diagnostic information relevant to the message.</p>
        //Examples: Java exception, memory dump, internal error code, call-stack information
        //Usage Notes: This may be free text or structured data (e.g., XML).
        public string Note { get; set; }


        public ICollection<Location >  Locations{ get; set; }




        //
        public string AcknowledgementId { get; set; }
        public Acknowledgement Acknowledgement { get; set; }
    }
}
