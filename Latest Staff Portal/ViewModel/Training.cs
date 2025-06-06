using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class TrainingLists
    {
        public string Application_No { get; set; }
        public string Application_Date { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string User_ID { get; set; }
        public string Supervisor { get; set; }
        public string Training_Category { get; set; }
        public string Course_Title { get; set; }
        public string Course_Desc { get; set; }
        public string Directorate { get; set; }
        public string Department { get; set; }
        public string RespC { get; set; }
        public string Trainer { get; set; }
        public string Sponsor { get; set; }
        public string Cost { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }
        public List<SelectListItem> ListOfDirectorate { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfCourses { get; set; }
        public List<SelectListItem> ListOfTrainers { get; set; }
        public List<SelectListItem> ListOfTrainingPlan { get; set; }
        public string TrainingPlan { get; set; }
        public List<SelectListItem> ListOfTrainingVeneu { get; set; }
        public string TrainingVenue { get; set; }
        public List<SelectListItem> ListOfCostCentre { get; set; }
        public string CostCentre { get; set; }
    }
    public class TrainingApplications
    {
        public string Code { get; set; }
        public string CourseTitle { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string Status { get; set; }
        public int Duration { get; set; }
        public decimal Cost { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public string Provider { get; set; }
        public string EmployeeNo { get; set; }
        public string ApplicationDate { get; set; }
        public string NoSeries { get; set; }
        public string EmployeeDepartment { get; set; }
        public string EmployeeName { get; set; }
        public string ProviderName { get; set; }
        public int NoOfParticipants { get; set; }
        public decimal ApprovedCost { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string ActualStartDate { get; set; }
        public string ActualEndDate { get; set; }
        public decimal EstimatedCost { get; set; }
        public bool ImprestCreated { get; set; }
        public decimal TrainingPlanCost { get; set; }
        public decimal Budget { get; set; }
        public decimal Actual { get; set; }
        public decimal Commitment { get; set; }
        public string GLAccount { get; set; }
        public string BudgetName { get; set; }
        public decimal AvailableFunds { get; set; }
        public string Local { get; set; }
        public bool RequiresFlight { get; set; }
        public string SupervisorNo { get; set; }
        public string SupervisorName { get; set; }
        public string TrainingPlanNo { get; set; }
        public string TrainingVenueRegionCode { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfDirectorate { get; set; }
        public List<SelectListItem> ListOfCourses { get; set; }
        public List<SelectListItem> ListOfTrainers { get; set; }
        public List<SelectListItem> ListOfTrainingPlan { get; set; }
        public List<SelectListItem> ListOfTrainingVeneu { get; set; }
        public List<SelectListItem> ListOfCostCentre { get; set; }
    }

    public class Trainees
    {
        public string No { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> ListOfEmployee { get; set; }
        public List<SelectListItem> ListOfExpenseType { get; set; }
        public string ExpenseCode { get; set; }
        public string ExpenseDesc { get; set; }
        public string ExpenseType { get; set; }
        public string Destination { get; set; }
        public string DestinationName { get; set; }
        public string No_of_Days { get; set; }
        public string Total_Amount { get; set; }
        public string LineNo { get; set; }
    }

    public class TraineeList
    {
        public string Status { get; set; }
        public List<Trainees> ListOfTrainees { get; set; }
    }

    public class TrainingDocument
    {
        public TrainingApplications DocHeader { get; set; }
        public List<Trainees> ListOfTrainees { get; set; }
        public List<TrainingCost> ListOfTraininingCost { get; set; }
    }

    public class TrainingCost
    {
        public string No { get; set; }
        public string Item { get; set; }
        public string Cost { get; set; }
    }

    public class NewTrainingNeedsDocument
    {
        public string Department { get; set; }
        public string Directorate { get; set; }
        public string RespC { get; set; }
        public string Course { get; set; }
        public string Trainer { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfDirectorate { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfCourses { get; set; }
        public List<SelectListItem> ListOfTrainers { get; set; }
        public List<SelectListItem> ListOfTrainingPlan { get; set; }
        public string TrainingPlan { get; set; }
        public List<SelectListItem> ListOfTrainingVeneu { get; set; }
        public string TrainingVenue { get; set; }
        public List<SelectListItem> ListOfCostCentre { get; set; }
        public string CostCentre { get; set; }
    }

    public class NeedsRequest
    {
        public string DocNo { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> ListOfNeeds { get; set; }
        public List<SelectListItem> ListOfExpenseType { get; set; }
        public string Course_ID { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Comments { get; set; }

        public string LineNo { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Department { get; set; }
        public string Job_Title { get; set; }

    }

    public class NeedsRequestList
    {
        public string Status { get; set; }
        public List<NeedsRequest> ListOfNeedsRequest { get; set; }
    }

    public class CurrentSkillsAssessment
    {
        public string DocNo { get; set; }
        public string Major_Tasks { get; set; }
        public List<SelectListItem> ListOfInstitution { get; set; }
        public List<SelectListItem> ListOfExpenseType { get; set; }
        public string Training_Required { get; set; }
        public string Existing_Training_Needs { get; set; }
        public string Training_Mode { get; set; }
        public string Start_Date { get; set; }
        public string Institution_No { get; set; }
        public string Institution_Name { get; set; }

        public string LineNo { get; set; }
    }

    public class CurrentSkillsAssessmentList
    {
        public string Status { get; set; }
        public List<CurrentSkillsAssessment> ListOfCurrentSkills { get; set; }
    }

    public class TNAQuestions
    {
        public string DocNo { get; set; }
        public string Question_ID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string LineNo { get; set; }
    }

    public class TNAQuestionsList
    {
        public string Status { get; set; }
        public List<TNAQuestions> ListOfTNAQuestions { get; set; }
    }

    public class TrainingExtension
    {
        public string No { get; set; }
        public string ApplicationNo { get; set; }
        public string Employee { get; set; }
        public string CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string ExtensionReason { get; set; }
        public string AdditionalInfo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public List<SelectListItem> ListOfDirectorate { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfCourses { get; set; }
        public List<SelectListItem> ListOfTrainers { get; set; }
    }

    public class NewTrainingExtension
    {
        public string Training_No { get; set; }
        public string Department { get; set; }
        public string Directorate { get; set; }
        public string RespC { get; set; }
        public string Course { get; set; }
        public string Trainer { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfDirectorate { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfCourses { get; set; }
        public List<SelectListItem> ListOfTraining { get; set; }
        public List<SelectListItem> ListOfTrainingPlan { get; set; }
        public string TrainingPlan { get; set; }
        public List<SelectListItem> ListOfTrainingVeneu { get; set; }
        public string TrainingVenue { get; set; }
        public List<SelectListItem> ListOfCostCentre { get; set; }
        public string CostCentre { get; set; }
    }

    public class TrainingFeedBackList
    {
        public string Application_No { get; set; }
        public string Application_Date { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string User_ID { get; set; }
        public string Training_No { get; set; }
        public string Trainer { get; set; }
        public string Sponsor { get; set; }
        public string Course_Title { get; set; }
        public string Purpose { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public List<SelectListItem> ListOfDirectorate { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfCourses { get; set; }
        public List<SelectListItem> ListOfTrainers { get; set; }
    }

    public class NewTrainingFeedBack
    {
        public string Training_No { get; set; }
        public string Department { get; set; }
        public string Directorate { get; set; }
        public string RespC { get; set; }
        public string Course { get; set; }
        public string Trainer { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfDirectorate { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfCourses { get; set; }
        public List<SelectListItem> ListOfTraining { get; set; }
        public List<SelectListItem> ListOfTrainingPlan { get; set; }
        public string TrainingPlan { get; set; }
        public List<SelectListItem> ListOfTrainingVeneu { get; set; }
        public string TrainingVenue { get; set; }
        public List<SelectListItem> ListOfCostCentre { get; set; }
        public string CostCentre { get; set; }
    }

    public class FeedbackSuggestions
    {
        public string DocNo { get; set; }
        public string Training_Category { get; set; }
        public List<SelectListItem> ListOfTrainingCategory { get; set; }
        public List<SelectListItem> ListOfFacilitatorPunctuality { get; set; }
        public string Category_Description { get; set; }
        public string Facilitator { get; set; }
        public string Facilitator_Punctuality { get; set; }
        public string Training_Userfullness { get; set; }
        public List<SelectListItem> ListOfUserfullness { get; set; }
        public string Facilitator_Rating { get; set; }
        public List<SelectListItem> ListOfFacilitatorRating { get; set; }
        public string Training_Time_Allocation { get; set; }
        public List<SelectListItem> ListOfTrainingTime { get; set; }
        public string Training_Materials { get; set; }
        public List<SelectListItem> ListOfTrainingMaterials { get; set; }
        public string Interactivity { get; set; }
        public List<SelectListItem> ListOfInteractivity { get; set; }
        public string Presentation_Quality { get; set; }
        public List<SelectListItem> ListOfPresentationQuality { get; set; }
        public string Comments { get; set; }
        public string LineNo { get; set; }
    }

    public class FeedbackSuggestionList
    {
        public string Status { get; set; }
        public List<FeedbackSuggestions> ListOfFeedbackSuggestion { get; set; }
    }


    public class TrainingNeedRequests
    {
        public string Code { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string DutyStation { get; set; }
        public string JobTitle { get; set; }
        public string FinancialYear { get; set; }
        public bool Disabled { get; set; }
        public string Description { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string TrainingPlanNo { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CourseId { get; set; }
        public List<SelectListItem> ListOfCourses { get; set; }
        public string CourseDescription { get; set; }
        public string Source { get; set; }
        public int SourceNo { get; set; }
        public string Comments { get; set; }
        public List<TNAQuestions> ListOfTNAQuestions { get; set; }
    }


    public class SelfSponsoredTrainingApplication
    {
        public string Training_Venue_Region;
        public string Code { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Employee_Department { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Training_Responsibility { get; set; }
        public string Course_Title { get; set; }

        public List<SelectListItem> ListOfCourseTitle { get; set; }
        public string Description { get; set; }
        public string Start_DateTime { get; set; }
        public string End_DateTime { get; set; }
        public int Duration { get; set; }
        public string Provider_Name { get; set; }
        public string Training_Type { get; set; }
        public string Training_Category { get; set; }
        public string Created_By { get; set; }
        public string Created_On { get; set; }
        public string Status { get; set; }
        public int Cost { get; set; }
        public string Purpose { get; set; }
        public string Training_Time { get; set; }
        public string Training_Types { get; set; }
        public string Funding_Source { get; set; }
    }

    public class CourseList
    {
        public string Code { get; set; }
        public string Descritpion { get; set; }
    }
    public class DutyStationNeeds
    {
        public string No { get; set; }
        public string DutyStation { get; set; }
        public string DutyStationName { get; set; }
        public string FinancialYear { get; set; }
        public int NumberOfEmployees { get; set; }
        public int SubmittedNeeds { get; set; }
        public bool Submitted { get; set; }
        public List<SelectListItem> ListOfDutyStations { get; set; }
    }
    public class ConsolidatedTrainingNeeds
    {
        public string No { get; set; }
        public string FinancialYear { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Committee { get; set; }
    }
    public class ConsolidatedTrainingNeedsLines
    {
        public string HeaderNo { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public int EmployeesApplied { get; set; }
        public string Verdict { get; set; }
        public string Comments { get; set; }
        public string FinancialYearFilter { get; set; }
    }
    public class NeedsRequests
    {
        public string CourseId { get; set; }
        public string Description { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string DutyStation { get; set; }
        public string JobTitle { get; set; }
        public string Source { get; set; }
    }


}