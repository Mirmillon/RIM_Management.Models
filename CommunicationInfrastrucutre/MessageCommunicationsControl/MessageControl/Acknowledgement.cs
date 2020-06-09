using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
//Metadata necessary when acknowledging a message.

namespace RIM_Management.Models.MessageCommunicationsControl.MessageControl
{
    public class Acknowledgement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AcknowledgementId { get; set; }

        [Required]
        public string ConveyingTransmission { get; set; }

        [Required]
        public string Acknowledges { get; set; }

        [Required]
        //The acknowledgement as defined in an enumerated set of acknowledgement types.
        //Examples: The receiving application successfully processed message; the receiving application found error(s) in message
        public string TypeCode { get; set; }

        //The sequence number of the message within a set of messages.
        public string ExpectedSequenceNumber { get; set; }

        //The number of messages the acknowledging application has waiting in queue for the receiving application.
        //If there are 3 low priority messages, 1 medium priority message and 1 high priority message, the message waiting number would be 5, because that is the total number of messages.
        //Usage Notes:  These messages would need to be retrieved via queries.The message count facilitates receiving applications that cannot receive unsolicited message(i.e., polling).
        public string MessageWaitingNumber { get; set; }

        //The highest level of importance in the set of messages the acknowledging application has waiting in queue for the receiving application.
        //These messages would need to be retrieved via queries.This facilitates receiving applications that cannot receive unsolicited messages(i.e., polling). 
        //The specific code specified identifies how important the most important waiting message is and may affect how soon the receiving application is required to poll for the message.
        //Priority may be used by local agreement to determine the timeframe in which the receiving application is expected to retrieve the messages from the queue
        public string MessageWaitingPriorityCode { get; set; }

        public ICollection<AcknowledgementDetail> AcknowledgementDetails { get; set; }
    }
}
