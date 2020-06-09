using MOS_Management.Models.CLassesMos;
using MOS_Management.Models.TypeDonnées.Complexes;
using MOS_Management.Models.TypeDonnées.Simple;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

//A record of something that is being done, has been done, can be done, or is intended or requested to be done. 
//Examples: The kinds of acts that are common in health care include(1) clinical observations, 
//                                                                  (2) assessments of health condition(such as problems and diagnoses), 
//                                                                  (3) healthcare goals, 
//                                                                  (4) treatment services(such as medication, surgery, physical and psychological therapy), 
//                                                                  (5) acts of assisting, monitoring or attending, 
//                                                                  (6) training and education services to patients and their next of kin, 
//                                                                  (7) notary services(such as advanced directives or living will), 
//                                                                  (8) editing and maintaining documents, and many others. 

//Rationale: Acts are the pivot of the RIM: domain information and process records are represented primarily in Acts.
//Any profession or business, including healthcare, is primarily constituted of intentional and occasionally non-intentional actions, performed and recorded by responsible actors. 
//An Act-instance is a record of such an action. 

//An Act-instance represents a " statement," according to Rector and Nowlan(1991) [Foundations for an electronic medical record.Methods Inf Med. 30.] 
//An activity in the real world may progress from definition, through planning and ordering to execution: these stages are represented as the moods of the Act.
//Even though one might think of a single activity as progressing through these stages, 
//the "attributable statement" model of Act entails that this progression be reflected by multiple Act-instances, 
//each having one and only one mood, and that this mood not change during the Act-instance's life cycle.
//This is because the attribution and content of speech acts along this progression of an activity may be different, 
//and it is critical that a permanent and faithful record be maintained of this progression.
//The specification of orders or promises or plans must not be overwritten by the specification of what was actually done, so as to allow recipients of the information to compare actions 
//with their earlier specifications.Act-instances that describe this progression of the same real world activity are linked through the ActRelationships (of the relationship category "sequel")

//Acts as statements are the only representations of real world facts or processes in the HL7 RIM. 
//The truth about the real world is constructed through the combination (and arbitration) of such attributed statements only, 
//and there is no class in the RIM whose objects represent "objective state of affairs" or "real processes" independent from attributed statements.
//A factual statement may be made about recent (but past) activities, authored (and signed) by the performer of such activities, e.g.a surgical procedure report, clinic note, etc.
//Similarly, a status update may be made about an activity that is presently in progress, authored by the performer (or a close observer), and later superseded by a full procedure report.
///Both status update and procedure report are acts, distinguished by mood and state (see Act.statusCode) and completeness of information:
//neither has any epistemological priority over the other except as judged by the recipient of the information. 

//Usage Notes: Acts connect to Entities in their Roles through Participations, and they connect to other Acts through ActRelationships.
//Participations indicate the performers, authors, and other responsible parties as well as subjects and beneficiaries(including tools 
//and material used in the performance of the act, which are also subjects). 
//The moodCode distinguishes among Acts that are meant as factual records, records of intended or ordered services, and other modalities in which acts can be recorded.
		

namespace RIM_Management.Models.FoundationClasses.ActDossier
{
    public class Act
    {   
        //A unique identifier for the Act.
        //Usage Notes: Successful communication only requires that an act have a single identifier assigned to it. 
        //However, it is recognized that as different systems maintain different databases, there may be different instance identifiers assigned by different systems.
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ActId { get; set; }

        //The major class of Acts to which an Act-instance belongs.
        //Formal Constraint: Every Act-instance SHALL have a classCode.If the act class is not further specified, the most general Act.classCode(ACT) is used. 
        //Usage Notes: For Act-instances that have an Act.code, the Act.code SHALL be a specialization of the Act.classCode.
        //The Act.code, however, cannot alter the meaning of the Act.classCode.
        ///This attribute provides a tightly controlled vocabulary of Act class "types" that is balloted with the RIM, and can be used to represent a type enumeration 
        ///that might have been represented as a physical class in the RIM, 
        //but was not because while it had unique meaning, it did not require unique attributes or unique patterns of associations.
        //The "code" attribute defines a specific sub-type of this Act type, and is intended to allow use of rich terminologies such as LOINC and SNOMED to represent these sub-types.
		
        [Required]
        public Code CodeAct { get; set; }

        //The intended use of the Act statement: as a report of fact, a command, a possibility, a goal, etc.
        //Examples: To illustrate the effect of mood code, consider a "blood glucose" observation.

        //The Definition mood specifies the Act of "obtaining blood glucose." 
        //Participations describe in general the characteristics of the people who must be involved in the act, and the required objects, e.g., specimen, facility, equipment, etc. involved. 
        //The Observation.value specifies the absolute domain (range) of the observation (e.g., 15-500 mg/dl).
        
        //In Intent mood the author of the intent expresses the intent that he or someone else "should obtain blood glucose." 
        //The participations are the people actually or supposedly involved in the intended act, especially the author of the intent 
        //or any individual assignments for group intents, and the objects actually or supposedly involved in the act (e.g., specimen sent, equipment requirements, etc.). 
        //The Observation.value is usually not specified, since the intent is to measure blood glucose, not to measure blood glucose in a specific range. 
        //(But compare with GOAL below).
        
        //In Request mood, a kind of intent, the author requests "please measure blood glucose." 
        //The Participations identify the people actually and supposedly involved in the act, especially the order placer and the designated filler, 
        //and the objects actually or supposedly involved in the act (e.g., specimen sent, equipment requirements, etc.). 
        //The Observation.value is usually not specified, since the order is not to measure blood glucose in a specific range.
        
        //In Event mood, the author states that "blood glucose was measured.
        //Participations indicate the people actually involved in the act, and the objects actually involved (e.g., specimen, facilities, equipment). 
        //The Observation.value is the value actually obtained (e.g., 80 mg/dL, or &<15 mg/dL).
        
        //In Event Criterion (not to be confused with Criterion) mood, an author considers a certain class of "obtaining blood glucose" possibly with a certain value (range) as outcome. 
        //The Participations constrain the criterion, for instance, to a particular patient. 
        //The Observation.value is the range in which the criterion would hold (e.g. >; 180 mg/dL or 200-300 mg/dL).
        
        //In Goal mood (a kind of criterion), the author states that "our goal is to be able to obtain blood glucose with the given value (range)" 
        //The Participations are similar to those in Intent mood, especially the author of the goal and the patient for whom the goal is made. 
        //The Observation.value is the range which defines when the goal is met (e.g. 80-120 mg/dl).
        
        //Usage Notes: To describe the progression of a business activity from definition to planning to execution, etc., one must instantiate Act-instances 
        //in each of the required moods and link them using ActRelationship of general type "sequel" (See ActRelationship.typeCode.)
        //Since the mood code is a determining factor for the meaning of an entire Act object, the mood must always be known. 
        //This means that whenever an act object is instantiated, the mood attribute SHALL be assigned to a valid code, and the mood assignment SHALL NOT change throughout the lifetime of the act object.
        //The Act.moodCode modifies the meaning of the Act class in a controlled way, just as in natural language, grammatical form of a verb modifies the meaning of a sentence in defined ways. 
        //For example, if the mood is factual (event), then the entire act object represents a known fact. 
        //If the mood expresses a plan (intent), the entire act object represents the expectation of what should be done. 
        //As the meaning of an Act-instance is factored in the mood code, the mood code affects the interpretation of the entire Act object and with it every property (attributes and associations). 
        
        //Note that the mood code affects the interpretation of the act object, and the meaning of the act object in turn determines the meaning of the attributes. 
        //However, the mood code does not arbitrarily change the meaning of individual attributes.
        //Acts have two kinds of act properties, inert and descriptive.
        
        //Inert properties are not affected by the mood, but descriptive properties follow the mood of the object. 
        //For example, there is an identifier attribute Act.id, which gives a unique identification to an act object. 
        //Being a unique identifier for the object is in no way dependent on the mood of the act object. Therefore, the "interpretation" of the Act.id attribute is inert with respect to the act object's mood.
        
        //By contrast, most of the Act class attributes describe what the Act statement expresses. 
        //Descriptive properties of the Act class answer the questions who, whom, where, with what, how and when the action is done. 
        //The questions who, whom, with what, and where are answered by Participations, while how and when are answered by descriptive attributes and ActRelationships. 
        //The interpretation of a descriptive attribute is aligned with the interpretation of the entire act object, and controlled by the mood. 
        
        //OpenIssue: In the May 2009 ballot, a strong Negative vote was lodged against several of the concept definitions in the vocabulary used for Act.moodCode. 
        //The vote was found "Persuasive With Mod", with the understanding that M&M would undertake a detailed review of these concept definitions for a future release of the RIM

        [Required]
        public Code MoodCode { get; set; }

        //TODO LIAISON A FAIRE
        public List<Identifiant> Identifiants { get; set; }

        //The particular kind of Act that the Act-instance represents within its class. 
        //Examples: Physical examination, serum potassium, inpatient encounter, charge financial transaction, etc.
        //Usage Constraint: Act.code, if used, SHALL be a specialization of the Act.classCode.
        //Usage Notes: Act.code is not a required attribute of Act.Rather than naming the kind of Act using an Act.code, 
        //one can specify the Act using only the class code and other attributes and properties of the Act.
        //In general and more commonly, the kind of Act is readily specified by an ActRelationship specifying that this Act instantiates another Act in definition mood.  
        //Even without reference to an act definition, the act may be readily described by other attributes, ActRelationships and Participations.For example, 
        //the kind of SubstanceAdministration may be readily described by referring to the specific drug, as the Participation of an Entity representing that drug.

		//This attribute defines a specific sub-type of a given Act type (determined by the " classCode" attribute).
        //It allows the use of rich terminologies such as LOINC and SNOMED to represent sub-types of the limited set of Act types defined by "classCode"
        //Act.classCode and Act.code are not modifiers of each other.
        //The Act.code concept should imply the Act.classCode concept. 
        //For a negative example, it is not appropriate to use an Act.code "potassium"; together with and Act.classCode for "laboratory observation" to some how mean 
        //"potassium laboratory observation" and then use the same Act.code for " potassium" together with Act.classCode for "medication" to mean "substitution of potassium". 
        //This mutually modifying use of Act.code and Act.classCode is not permitted.

        //Design Comment: The superstructure of the ActCode domain should reflect the structure of ActClass domain, in order that individual codes or externally referenced vocabularies 
        //within ActCode be subordinated to the ActClass structure.

		//Explain criteria for when it would be appropriate to use code rather than ActRelationship.
        
        public Code CodeActSup {get; set; }

        //An indicator specifying that the Act statement is a negation of the Act in Event mood as described by the descriptive attributes.
        //Examples: When used with event mood, allows communicating "Surgery was not performed" or "Consent was not given". 
        //When used in order mood, allows communicating "Do not administer this substance". When used in EVN.CRIT mood allows you to say "If the patient is not admitted . . ."
        //Usage Notes: The actionNegationInd works as a negative existence quantifier on the actual, intended or described Act event.
        //In Event mood, it indicates the defined act did not occur. 
        //In Intent mood, it indicates the defined act is not intended/desired to occur. 
        //In Criterion mood, it indicates that the condition is based on the non-occurrence of the event. 
        //It is nonsensical to have a negationInd of true for acts with a mood of definition.
        //The actionNegationInd negates the Act as described by the descriptive properties (including Act.code, Act.effectiveTime, Observation.value, Act.doseQty, etc.) and any of its components. 
        //The document characteristic properties such as Act.id, Act.moodCode, Act.confidentialityCode, and particularly the Author-Participation are NOT negated. 
        //These document characteristic properties always have the same meaning: i.e., the author remains to be the author of the negative observation. 
        //Also, most ActRelationships (except for components) are not included in the negation. 
        //Refer to the attribute isDocumentCharacteristic property and the ActRelationshipType and ParticipationType code system isDocumentCharacteristic properties for specific guidance.
        //For example, a highly confidential order written by Dr. Jones, to explicitly NOT give "succinyl choline" for the "reason" (ActRelationship) of a history of malignant hyperthermia 
        //(Observation) negates the descriptive properties "give succinyl choline" (Act.code), 
        //but it is still positively an order and written by Dr. Jones and for patient John Smith, 
        //and the reason for this order is the patient's history of malignant hyperthermia. However, additional detail in descriptive attributes will be part of the negation 
        //which then limits the effectiveness of the negated statement. For example, had the order "not to give a substance" included a doseQuantity, 
        //it would mean that the substance should not be given at that particular dose (but any other dose might still be O.K.).
        //An act statement with actionNegationInd is still a statement about the specific fact described by the Act. 
        //For instance, a negated "patient had an appendectomy on July 1" means that the author positively denies that appendectomy occurred on July 1, 
        //and that he takes the same responsibility for such statement and the same requirement to have evidence for such statement than if he had not used negation. 
        //Conversely, the action negation indicator does NOT just negate that the fact was affirmed or that the statement had been made. 
        //This holds for all moods in the same way, e.g., a negated order is an order NOT to do the described act, not just the lapidary statement that there is no such order. 
        //Such lapidary statements are handled by negating the control act that created the subject act. I.e. "An order of this type (DEFN mood) with an author of Dr. Smith was not created."
        //Note that for Observations, actionNegationInd indicates that the act itself did not occur. I.e. no observation took place. 
        //To indicate that an observation did occur but the finding was negative, use Observation.valueNegationInd
        
        public Indicateur ActionNegationInd { get; set; }

       //>A word or phrase by which a specific Act may be known among people.
        //Examples: Name of a research study (e.g., "Scandinavian Simvastatin Study"), 
        //          name of a court case (e.g., "Brown v. Board of Education"), 
        //          name of another kind of work project or operation. For acts representing documents, this is the title of the document.
        //Formal Constraint:  Previous to release 2.05 of the RIM, this attribute bore the datatype ST. 
        //From release 2.05 onwards, the datatype was extended to a CONSTRAINED restriction of the ED datatype. 
        //The constraints to be imposed are identical to those for the ST datatype, except that the mediaType shall be "text/x-hl7-title+xml" or "text/plain". 
        //The intent is to allow sufficient mark-up to convey the semantics of scientific phrases, such as chemical compounds. 
        //This markup must not be used to convey simple display preferences. The default mediaType should be "text/plain".
        //Usage Notes: This is not a formal identifier but rather a human-recognizable common name. 
        //However it is similar to the id attribute in that it refers to a specific Act rather than a 'kind' of act. 
        //(For definition mood, the title refers to that specific definition, rather than to a broad category that might be conveyed with Act.code.)
        
        public string Title { get; set; }

        //A renderable textual or multimedia description (or reference to a description) of the complete information which would reasonably be expected to be displayed to a human reader conveyed by the Act.
        //Examples: For act definitions, the Act.text can contain textbook-like information about that act. 
        //          For act orders, the description will contain particular instructions pertaining only to that order.0 
        //Usage Notes: The content of the description is not considered part of the functional information communicated between computer systems. 
        //For Acts that involve human readers and performers, however, computer systems must show the Act.text field to a human user, who has responsibility for the activity; 
        //or at least must indicate the existence of the Act.text information and allow the user to see that information.
        //Free text descriptions are used to help individuals interpret the content and context of acts, but all information relevant for automated functions SHALL be communicated using the proper attributes 
        //and associated objects.
        //A user SHOULD be able to read Act.text alone, without seeing any of the encoded information, and have no risk of misinterpreting or lacking full understanding of the full content of the Act. 
        //For example, II.root, or CD.codeSystem would not normally be displayed to a human and thus would not need to be exposed as part of Act.text.
        //The rendering is expected to include all 'descendent' ActRelationships and Participations, recursively navigating child Acts as exposed in that particular'snapshot'
        //However, there are several data elements which are NOT expected to be included in the rendering. 
        //These are #Component Sections (ActRelationship=COMP, 
        //          #classCode &<= DOCSECT)
        //          #The title attribute
        //          #Anything attached to ActRelationship=XFRM)
        //          #Previous versions (ActRelationship=RPLC)
        //          #Act.text MAY include information that is not in the other attributes/associations, but SHALL include all information that is in such attributes or associations, 
        //          with the exception of those identified above.
        //Act.text SHALL NOT be used for the sharing of computable information. Computable information SHALL be conveyed using discrete attributes. 
        //Any information which Act.text contains not elsewhere exposed in encoded information will be opaque to computer systems. 
        //For this reason, Act.text SHALL not be used to contain information which negates or significantly modifies the understanding of information encoded in discrete attributes.
        //To communicate "supplemental text," an act relationship (e.g. "component" or "subject of") should be created to a separate Act with a bare Act.text attribute to convey the supplemental information,
        //possibly with a code indicating "annotation" or some similar concept.
        
        //Usage Notes: Clarify strength of "Act.text SHALL NOT be used for the sharing of computable information": should this be a constraint?
        
        public string Text { get; set; }

        //The state of the Act.
        //Usage Notes: The status reflects the state of the activity.In the case of an Observation, this is the status of the activity of observing (e.g., " new," "complete," "cancelled"), 
        //not the status of what is being observed(e.g., disease status, " Active" allergy to penicillin).  
        //To convey the status of the subject being observed, consider coordinating it into the code or value attribute of the Observation or using a related Observation.
        
        public string StatusCode { get; set; }

        //The clinically or operationally relevant time of an act, exclusive of administrative activity. 
        //Examples: For clinical Observations, the effectiveTime is the time at which the observation holds (is effective) for the patient.
    	//          For contracts, the effectiveTime is the time for which the contract is in effect.
        //          For consents, the effectiveTime is the time for which the consent is valid.
        //          For substance administrations, the effective time is the time over which the substance is to be administered, including the frequency of administration (e.g., TID for 10 days)
        //          For a surgical procedure(operation), the effectiveTime is the time relevant for the patient, i.e., between incision and last suture. 
        //          For transportation acts, the effective time is the time the transported payload is en route.
        //          For patient encounters, this is the " administrative" time, i.e., the encounter start and end date required to be chosen by business rules, 
        //              as opposed to the actual time the healthcare encounter related work is performed.

		//Usage Notes: The effectiveTime is also known as the "primary" time(Arden Syntax) or the "biologically relevant time" (HL7 v2.x). This attribute is distinguished from activityTime.
        //For observations, the time of the observation activity may be much later than the time of the observed feature.
        //For instance, in a Blood Gas Analysis (BGA), a result will not be available for several minutes after the specimen was taken, meanwhile the patient s physiological state may have changed significantly.  
		//For essentially physical activities(surgical procedures, transportations, etc.), the effective time is the time of interest for the Act's intention, i.e., 
        //  since the intention of a transportation is to deliver a payload from location A to B, the effectiveTime is the time this payload is underway from A to B.However, 
        //  the Act usually also includes accidental work which is necessary to perform the intention of the Act, but is not relevant for the Act's purpose. 

		//For example, the time a driver needs to go to the pick-up location A and then return from drop-off location B to some home base, is included in the physical activity (as activityTime), 
        //but it does not matter from the perspective of the payload&apos;s transportation and is excluded from effectiveTime.Another example: a person's work hours(effectiveTime) 
        //may be from 8 AM to 5 PM, no matter whether that person needs 10 minutes for the commute or 2 hours.The commute is necessary to be at work, but it is not part of the working time.
        
        public MosDateTime EffectiveTimes { get; set; }

        //A time expression specifying when an Observation, Procedure, or other Act occurs, or, depending on the mood, is supposed to occur, scheduled to occur, etc.
        //The activityTime includes the times of component actions(such as preparation and clean-up).  
        //For Procedures and SubstanceAdministrations, the activityTime can provide a needed administrative function by providing a more inclusive time to be anticipated in scheduling. 

        //Usage Notes: The activityTime is primarily of administrative rather than clinical use.
        //The clinically relevant time is the effectiveTime.
        //When an observation of a prior symptom is made, the activityTime describes the time the observation is made, as opposed to effectiveTime which is the time the symptom is reported to have occurred. 
        //Thus the activityTime may be entirely different from the effectiveTime of the same Act. 
        //However, even apart from clinical use cases, designers should first consider effectiveTime as the primary relevant time for an Act.

        //ActivityTime indicates when an Act occurs, not when it is recorded.Many applications track the time an observation is recorded rather than the precise time during which an observation is made, in which case Participation.time (e.g.of the Author) should be used.These recorded observations can take place during an encounter, and the time of the encounter often provides enough information so that activityTime isn&apos;t clinically relevant.

        //ActivityTime is a descriptive attribute: like effectiveTime, it always describes the Act event as it does or would occur. 
        //For example, when a procedure is requested, the activityTime describes the requested total time of the procedure, which may differ from the time recorded for the resulting event. 
        //By contrast, the author Participation.time is inert, i.e., author participation time on an order specifies when the order was written and has nothing to do with when the event might actually occur.
        
        public MosDateTime ActivityTimes { get; set; }

        //The point in time at which information about Act-instance(regardless of mood) first became available to a system reproducing this Act.
        //The availabilityTime is metadata describing the record, not the Act. 
        //Rationale: An Act might record that a patient had a right-ventricular myocardial infarction effective three hours ago (see Act.effectiveTime), 
        //but we may only know about this unusual condition a few minutes ago (Act.availabilityTime). 
        //Thus, any interventions from three hours ago until a few minutes ago may have assumed the more common left-ventricular infarction, 
        //which can explain why these interventions(e.g., nitrate administration) were taken, even though they may not have been appropriate in light of the more recent knowledge. 

        //Usage Notes: The availabilityTime is added (or changed) by any system that receives this Act, and is not attributed to the author of the act statement (it would not be included in the material the author would attest to with a signature). 
        //The system reproducing the Act is often not the same as the system originating the Act, but a system that received this Act from somewhere else, 
        //and, upon receipt of the Act, values the availabilityTime to convey the time since the users of that particular system could have known about this Act-instance. 
        //A system evaluates availabilityTime on receipt (or creation) of information, and must be able to produce the availabilityTime of the information if and when it communicates that information further. 

        //Design Comment: Clarify: Does the act acquire a new availability time with each transmission? 
        //                         Does this value indicate to which system it refers? Or is it always defined as the availability time for the transmitting system in the context of a message, 
        //                           any further transmission either dropping or overwriting it, and recording, if necessary, previous transmission times as separate observations? 
        //                         Deleted text indicates availabilityTime is "attributed to the author of an act that includes or refers to the act." 
        //                         It is not clear why this attribute should require special conduction rules: are they different from the rules for other attributes?
        
        public MosDateTime AvailabilityTimes { get; set; }

        //The urgency under which the Act happened, can happen, is happening, is intended to happen, or is requested/demanded to happen. 
        //Examples: Routine, elective, emergency.

		//Usage Notes: This attribute is used in orders to indicate the ordered priority, and in event documentation it indicates the actual priority used to perform the act.
        //In definition mood it indicates the available priorities, hence the open cardinality.

        public ICollection<Code> PriorityCodes { get; set; }

        //Constraints around appropriate disclosure of information about this Act, regardless of mood.

        public Code ConfodentialityCode { get; set; }

  //      	An interval of integer numbers stating the minimal and maximal number of repetitions of the Act.

		//Examples: An oral surgeon&apos; s advice to a patient after tooth extraction might be: &quot;replace the gauze every hour for 1 to 3 times until bleeding has stopped completely.&quot; This translates to repeatNumber with low boundary 1 and high boundary 3.

		//Usage Notes: This attribute is a member of the workflow control suite of attributes. 

		//The number of repeats is additionally constrained by time.The act will repeat at least the minimal number of times and at most, the maximal number of times, unless the time exceeds the maximal Act.effectiveTime, at which point the repetitions will terminate

		//On an Act in Event mood, the repeatNumber is usually 1.  If greater than 1, the Act represents a summary of several event occurrences occurring over the time interval described by effectiveTime.These occurrences are not otherwise distinguished. 

		//To distinguish occurrences of acts within a sequence of repetitions, use distinct instances of Act related by ActRelationships using ActRelationship.sequenceNumber.
        public string RepeatNumber { get; set; }

  //      <body>An indication that the Act is interruptible by asynchronous events.

		//Usage Notes: This attribute is part of the suite of workflow control attributes. Act events that are currently active can be interrupted in various ways. Interrupting events include (1) an explicit abort request against the Act, (2) expiration of the time allotted to this Act(timeout), (3) a &quot;through condition&quot; ceases to hold true for this Act(see ActRelationship.checkpointCode), (4) the Act is a component with the joinCode &quot;kill&quot; and all other components in that same group have terminated(see Act.joinCode), and(5) a containing Act is interrupted.  

		//If an Act receives an interrupt and the Act itself is interruptible, but it has currently active component-Acts that are non-interruptible, the Act will be interrupted when all of its currently active non-interruptible component-acts have terminated.</body>


        public Indicateur Interruptible { get; set; }

        //An indicator specifying whether the Act can be manipulated independently of other Acts or only through a super-ordinate composite Act that has this Act as a component. 
        //Examples: An order may have a component that cannot be aborted independently of the other components. Usage Notes:  By default the independentInd should be true. 
        //An Act definition is sometimes marked with independentInd = false if the business rules would not allow this act to be ordered without ordering the containing act group.
        public Indicateur Independent { get; set; }

  //      <body><p>The motivation, cause, or rationale of an Act, when such rationale is not reasonably represented as an ActRelationship of type &quot;has reason&quot; linking to another Act.</p>

		//<p><i>Examples: </i>Example reasons that might qualify for being coded in this field might be: &quot; routine requirement,&quot; &quot;infectious disease reporting requirement,&quot; &quot;on patient request,&quot; &quot;required by law.&quot;</p>

		//<p><i>Usage Notes: </i></p>

		//<p>Most reasons for acts can be clearly expressed by linking the new Act to another prior Act record using an ActRelationship of type &quot;has reason.&quot; This simply states that the prior Act is a reason for the new Act(see ActRelationship). The prior act can then be a specific existing act or a textual explanation.This works for most cases, and the more specific the reason data is, the more should this reason ActRelationship be used instead of the reasonCode. </p>

		//<p>The reasonCode remains as a place for common reasons that are not related to a prior Act or any other condition expressed in Acts.Indicators that something was required by law or was on the request of a patient may qualify.However, if that piece of legislation, regulation, or the contract or the patient request can be represented as an Act (and they usually can), such a representation is preferable to the reasonCode.</p></body>
		//</ownedComment>

        public ICollection<Code> ReaspnCodes { get; set; }

        //The primary language in which this Act statement is specified, particularly the language of the Act.text.

        public Langue Language { get; set; }

        //<body><p>If true, indicates that the data conveyed by the act, including outbound associations, represent &quot;criteria&quot; for some other act, not a &quot;real&quot; act.I.e.If an Act exists with a classCode of ACT and a moodCode of RQO and isCriterionInd is true, it does not represent an order for an act.Rather, it represents a criteria that will match on all orders.</p> <p><i>Constraint: </i>Act-relationships directed to any Act with &quot; isCriterionInd=true&quot; SHALL have &quot;conductible=false&quot; unless the source Act also has isCriterionInd=true.</p></body>

        public Indicateur isCriterionInd { get; set; }

        public ICollection<Participation> Participations { get; set; }

        public ICollection<ActRelationship> InBound{ get; set; }

        public ICollection<ActRelationship> OutBound { get; set; }





}
}
