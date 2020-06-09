using System;
using System.Collections.Generic;
using System.Text;

//A relationship class that binds the various entities participating in the transmission (sender, receiver, respond-to) to be linked to the transmission. 

namespace RIM_Management.Models.MessageCommunicationsControl.MessageControl
{
    public class CommunicationFunction
    {
        public string CommunicationFunctionId { get; set; }


        public string CommunicationFunctionId { get; set; }


        public string CommunicationFunctionId { get; set; }

        public ICollection<Entite> Entites { get; set; }

        public ICollection<Transmission> Transmissions { get; set; }

        //
        public string TransmissionId { get; set; }
        public Transmission Transmission { get; set; }

    }
}
