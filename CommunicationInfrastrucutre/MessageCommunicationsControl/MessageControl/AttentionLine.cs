using System;
using System.Collections.Generic;
using System.Text;

//A collection of parameters related to a transmission that may need to be accessible from the transmission wrapper.
//Examples: If encrypted or compressed payloads are used, and the receiver needs to have access to the Patient.id for internal routing purposes within the receiving application, 
//then the sender can make this information available in AttentionLine.
//Usage Constraint: The contents of the class shall be related to the transmission 
//as a whole and shall be solely used for transmission related purposes and not have any impact on the semantic interpretation of the contents of the transmission. 
//Usage Notes: >AttentionLine is a name-value pair, with keyWordText providing the topic from an enumerated set and value providing the parameter.
//Design Comment: Confirm edits. Clarify in definition, add to examples.
							


namespace RIM_Management.Models.MessageCommunicationsControl.MessageControl
{
    public class AttentionLine
    {
        public string AttentionLineId { get; set; }

        //A category of attentionLine parameter.
        //Examples: Patient identifier, public health case type
        public string KeyWordText { get; set; }

        //A value associated with the key as provided in the AttentionLine.keyWordText attribute.
        //Formal Constraint: The data type of the attribute SHALL be constrained to one of the following data types: BL, CV, II, URL, INT, REAL, TS, PQ, MO, IVL&<TS>
							
        public string Value { get; set; }

        //
        public string TransmissionId { get; set; }
        public Transmission Transmission { get; set; }
    }
}
