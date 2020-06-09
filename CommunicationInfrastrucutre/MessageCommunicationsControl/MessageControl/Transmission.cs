using MOS_Management.Models.TypeDonnées.Complexes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

//	Information about a specific transmission of information from one application to another.
// OpenIssue:  </i>This class is being actively considered for being split between two distinct classes dealing with transmission and contractual concepts.
// Thus it may be deprecated in a future RIM release.</p></body>

namespace RIM_Management.Models.MessageCommunicationsControl.MessageControl
{
    public class Transmission
    {
        //A unique identifier for the transmission.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TransmissionId { get; set; }

        public ICollection<ConveyedAcknowledgement> ConveyedAcknowledgements { get; set; }

        public ICollection<AcknowledgedBy> AcknowledgedBies { get; set; }

        //The date/time that the sending system created the transmission.
        //Usage Notes: </i>If the time zone is specified, it will be used throughout the transmission as the default time zone
        public string CreationTime { get; set; }

        //Usage Notes: This attribute is specified for applications to implement security features for a transmission. 
        //Its use is not further specified at this time.
        public string SecurityText { get; set; }

        //The transmission mode with which a receiver should communicate its receiver responsibilities.
        //The receiver may respond in a non-immediate manner; 
        //the receiver is required send an immediate response; 
        //the receiver shall keep any application responses in a queue until such time as the queue is polled.
        public Code ResponseModeCode { get; set; }

        //The identifier of the V3 interaction that constrains the transmission.
        //Design Comment: Original comment deleted, as Batch no longer has this attribute: 
        //This attribute is also present in the sibling class, Batch.This change was made rather than moving this attribute to their common ancestor class, Transmission.
        //This decision was taken because we do not have all the methodology and backwards compatibility issues worked out. 
        //Once we have established our backwards compatibility, we should promote this attribute to the parent. 
        //The problem is the sequencing of attributes within the HDF and their impact on the ITSs.&quot;</p></body>
        public string InteractionId { get; set; }


        //Definition: The identifier of the profile(s) that constrain the transmission. 
        //Includes the version of the specification(s) with which a given instance is compliant.
        //Usage Constraint:    When multiple profiles are specified, the transmission instance MUST be valid against all of them.
        //However, a receiver MAY choose to validate against only the first one recognized. 
        //For this reason, &amp; apospreferred&amp;apos or more-rigorous profiles SHOULD be listed first.
        //When declaring conformance against an HL7 International specification, the profileId root should be 2.16.840.1.113883.1.9.  
        //The extension should be in the form [YYYY] NE for Normative Editions and[YYYYMM] DE for development (ballot) editions.  
        //Conformance against versions of affiliate and other specifications should be documented using patterns declared as part of those specifications.
        //Usage Notes: </i>The transmission profile allows a given implementation to explicitly state how it differs from the standard interaction definition.</p></body>
					
        public List <Profile> Profiles { get; set; }

        public List<AttentionLine> AttentionLines { get; set; }

        public List<Batch> Batchs { get; set; }

        public List<CommunicationFunction> CommunicationFunctions { get; set; }

        public List<Attachment> Attachments { get; set; }

        public List<Attachment> InboundRelationship { get; set; }

        public List<Attachment> OutboundRelationship { get; set; }

        //
        public string CommunicationFunctionId { get; set; }
        public CommunicationFunction CommunicationFunction { get; set; }
    }
}
