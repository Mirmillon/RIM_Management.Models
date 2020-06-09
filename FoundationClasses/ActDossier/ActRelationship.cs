using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

//A directed association between a source Act and a target Act. 

//Examples: has component, fulfills, has reason.

//		1) An electrolyte observation panel may have sodium, potassium, pH, and bicarbonate observations as components.
//      The composite electrolyte panel would then have 4 outbound ActRelationships of type "has component," which would be inbound to their target sodium, potassium, pH, and bicarbonate observations. 

//		2) The electrolyte panel event has been performed in fulfillment of an observation order.The electrolyte panel event has an outbound ActRelationship of type "fulfills" with the order as target. 

//		3) A Procedure "cholecystectomy" may be performed for the reason of an Observation of "cholelithiasis." 
//      The procedure has an outbound ActRelationship of type "has reason," which would be inbound to the cholelithiasis observation. 

//		Usage Notes: The ActRelationship class is used to construct systems of acts to represent complex observations, action plans, and to represent clinical reasoning or judgments about action relationships.
//      Prior actions can be linked as the reasons for more recent actions.Supporting evidence can be linked with current clinical hypotheses.
//      Problem lists and other networks of related judgments about clinical events are represented by the ActRelationship link. 

//		Every ActRelationship instance is like an arrow with a point (headed to the target) and a butt(coming from the source). 
//      The functions that source and target Acts play in that association are defined for each ActRelationship type differently.
//      For instance, in a composition relationship, the source is the composite and the targets are the components. 
//      In a reason-relationship the source is any Act and the target is the reason or indication for the source-Act. 
//		The relationships associated with an Act are considered properties of the source act object. 
//      This means that the author of an Act-instance is also considered the author of all of the act relationships that have this Act as their source, 
//      (though not necessarily of the target Acts of those relationships). There are no exceptions to this rule. 

//		The meaning and purpose of an ActRelationship is specified in the ActRelationship.typeCode attribute. </body>

namespace RIM_Management.Models.FoundationClasses.ActDossier
{
    public class ActRelationship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ActRelationshipId { get; set; }
    }
}
