using System;
using System.Collections.Generic;
using System.Text;

//A message which is a collection of HL7 V3 messages. 
//Design Comment: Does the batch have any effect on the member message, or is it a composition class that contains the member messages?

namespace RIM_Management.Models.MessageCommunicationsControl.MessageControl
{
    public class Batch
    {
        public string BatchId { get; set; }

        public string BatchId { get; set; }

        public string BatchId { get; set; }

        public string BatchId { get; set; }

        public string BatchId { get; set; }

        public string BatchId { get; set; }

        public string BatchId { get; set; }

        public string BatchId { get; set; }

        //
        public string TransmissionId { get; set; }
        public Transmission Transmission { get; set; }
    }
}
