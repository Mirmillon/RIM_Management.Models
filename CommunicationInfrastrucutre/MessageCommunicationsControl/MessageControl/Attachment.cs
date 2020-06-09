using System;
using System.Collections.Generic;
using System.Text;

//An addressable data block which can be referred to from the interior of the message. 
//Usage Notes: Attachments are referred to from the message body using the reference functionality of the ED data type.
//OpenIssue:  Proper use of this class (Attachment) requires an extension of the referencing mechanism of the ED data type.
//Design Comment: </i>Open issue requires more detail.</p>


namespace RIM_Management.Models.MessageCommunicationsControl.MessageControl
{
    public class Attachment
    {


        //
        public string TransmissionId { get; set; }
        public Transmission Transmission { get; set; }
    }
}
