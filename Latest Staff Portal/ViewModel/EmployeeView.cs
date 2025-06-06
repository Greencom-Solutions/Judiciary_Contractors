using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Latest_Staff_Portal.ViewModel
{
    //public class EmployeeView
    //{
    //    public string No { get; set; }
    //    public string Name { get; set; }
    //    public string IDNo { get; set; }
    //    public string Gender { get; set; }
    //    public string MaritalStatus { get; set; }
    //    public string Nationality { get; set; }
    //    public string County { get; set; }
    //    public string DoB { get; set; }
    //    public string Address1 { get; set; }
    //    public string Address2 { get; set; }
    //    public string City { get; set; }
    //    public string PostalCode { get; set; }
    //    public string Country { get; set; }
    //    public string HomeTelNo { get; set; }
    //    public string PhoneNo { get; set; }
    //    public string WorkTel { get; set; }
    //    public string CompanyEmail { get; set; }
    //    public string PersonalEmail { get; set; }
    //    public string DateOfJoin { get; set; }
    //    public string ProbationDate { get; set; }
    //    public string PenSchemeJoinDate { get; set; }
    //    public string JobTitle { get; set; }
    //    public string EmpStatus { get; set; }
    //    public string JobCat { get; set; }
    //    public string Department { get; set; }
    //    public string Campus { get; set; }
    //    public string Bank { get; set; }
    //    public string Branch { get; set; }
    //    public string AccountNo { get; set; }
    //    public string PinNo { get; set; }
    //    public string NSSFNo { get; set; }
    //    public string NHIFNo { get; set; }
    //    public string AllocatedDays { get; set; }
    //    public string CarryForawrd { get; set; }
    //    public string ReimbDays { get; set; }
    //    public string LeaveTaken { get; set; }
    //    public string EarnedLeaveDays { get; set; }
    //    public string LeaveBal { get; set; }

    //}
    public class EmployeeView2
    {
        public string No { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public string RecordType { get; set; }
        public string CurrentPositionID { get; set; }
        public string JobTitle2 { get; set; }
        public string SearchName { get; set; }
        public string CountyofOrigin { get; set; }
        public string CountyofOriginName { get; set; }
        public string CountyofResidence { get; set; }
        public string CountyofResidenceName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public bool Disabled { get; set; }
        public string DisabilityNo { get; set; }
        public DateTime DisabilityCertExpiry { get; set; }
        public bool InsuranceCertificate { get; set; }
        public string EthnicOrigin { get; set; }
        public DateTime LastDateModified { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }

        public string DepartmentName { get; set; }
        public string ResponsibilityCenter { get; set; }
        public bool HeadofStation { get; set; }
        public bool HOD { get; set; }
        public bool IsChiefJustice { get; set; }
        public bool IsPartOfDisciplinaryTeam { get; set; }
        public bool ICTHelpDeskAdmin { get; set; }
        public string UserID { get; set; }
        public string Supervisor { get; set; }
        public string ReliverCode { get; set; }
        public string SalaryScale { get; set; }
        public string Present { get; set; }
        public string EmployeePostingGroup1 { get; set; }
        public DateTime IncrementDate { get; set; }
        public string IncrementalMonth { get; set; }
        public DateTime LastIncrementDate { get; set; }
        public string DirectorateCode { get; set; }
        public string ImplementingUnitName { get; set; }
        public string DepartmentCode { get; set; }
        public string DutyStation { get; set; }
        public string OrgUnit { get; set; }
        public string OrganisationUnitName { get; set; }
        public string AccountType { get; set; }
        public string Division { get; set; }
        public string EmployeeJobType { get; set; }
        public string CourtLevel { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string CountryRegionCode { get; set; }
        public string CitizenshipType { get; set; }
        public string EmployeeType { get; set; }
        public string PhoneNo { get; set; }
        public string Extension { get; set; }
        public string MobilePhoneNo { get; set; }
        public string Pager { get; set; }
        public string PhoneNo2 { get; set; }
        public string Email { get; set; }
        public string CompanyEmail { get; set; }
        public string AltAddressCode { get; set; }
        public DateTime AltAddressStartDate { get; set; }
        public DateTime AltAddressEndDate { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string Ext { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime EndOfProbationDate { get; set; }
        public bool Inducted { get; set; }
        public DateTime RetirementDate { get; set; }
        public string FullPartTime { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public DateTime ReEmploymentDate { get; set; }
        public string NoticePeriod { get; set; }
        public string Status { get; set; }
        public string EmployeeStatus2 { get; set; }
        public DateTime InactiveDate { get; set; }
        public string CauseofInactivityCode { get; set; }
        public string EmplymtContractCode { get; set; }
        public string ResourceNo { get; set; }
        public string SalespersPurchCode { get; set; }
        public string Disciplinarystatus { get; set; }
        public string Reasonfortermination { get; set; }
        public DateTime TerminationDate { get; set; }
        public DateTime DateOfLeaving { get; set; }
        public string ExitInterviewConducted { get; set; }
        public DateTime ExitInterviewDate { get; set; }
        public string ExitInterviewDoneby { get; set; }
        public int BondingAmount { get; set; }
        public string ExitStatus { get; set; }
        public bool AllowReEmploymentInFuture { get; set; }
        public string CurrencyCode { get; set; }
        public bool Paystax_x003F_ { get; set; }
        public bool PayWages { get; set; }
        public string PayMode { get; set; }
        public string PIN { get; set; }
        public string NHIFNo { get; set; }
        public string SocialSecurityNo { get; set; }
        public string IDNumber { get; set; }
        public string PostingGroup { get; set; }
        public int ClaimLimit { get; set; }
        public string BankAccountNumber { get; set; }
        public string Employeex0027sBank { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankBranchName { get; set; }
        public string Employeex0027sBank2 { get; set; }
        public string BankName2 { get; set; }
        public string BankBranch2 { get; set; }
        public string BankBranchName2 { get; set; }
        public string BankAccountNo2 { get; set; }
        public bool AllowNegativeLeave { get; set; }
        public int OffDays { get; set; }
        public int LeaveDaysBF { get; set; }
        public int AllocatedLeaveDays { get; set; }
        public int TotalLeaveDays { get; set; }
        public int TotalLeaveTaken { get; set; }
        public string AdministrativeUnit { get; set; }
        public int LeaveOutstandingBal { get; set; }
        public int LeaveBalance { get; set; }
        public int AcruedLeaveDays { get; set; }
        public int CashperLeaveDay { get; set; }
        public int CashLeaveEarned { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveTypeFilter { get; set; }
        public string LeavePeriodFilter { get; set; }
        public bool OnLeave { get; set; }
        public string DateFilter { get; set; }
        public List<EmployeeDependant> EmployeeDependants { get; set; }
        public List<EmployeeQualification> EmployeeQualifications { get; set; }
        public List<AssignedDimensions> EmployeeDimensions { get; set; }
        public int AllocatedAssets { get; set; }
        public string UserRole { get; set; }
        public bool IsSupervisor { get; set; }
        public int AssignedPurchaseReq { get; set; }

    }

    public class EmployeeView
    {
        public string No { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Search_Name { get; set; }
        public string Name_2 { get; set; }
        public string Responsibility_Center { get; set; }
        public string Location_Code { get; set; }
        public string Post_Code { get; set; }
        public string Country_Region_Code { get; set; }
        public string Phone_No { get; set; }
        public string Fax_No { get; set; }
        public string Email { get; set; }
        public string E_Mail { get; set; }
        public string IC_Partner_Code { get; set; }
        public string Contact { get; set; }
        public string Purchaser_Code { get; set; }
        public string Vendor_Posting_Group { get; set; }
        public string Gen_Bus_Posting_Group { get; set; }
        public string VAT_Bus_Posting_Group { get; set; }
        public string Payment_Terms_Code { get; set; }
        public string Fin_Charge_Terms_Code { get; set; }
        public string Currency_Code { get; set; }
        public string Language_Code { get; set; }
        public string Blocked { get; set; }
        public bool Privacy_Blocked { get; set; }
        public string Last_Date_Modified { get; set; }
        public string Application_Method { get; set; }
        public string Shipment_Method_Code { get; set; }
        public string Lead_Time_Calculation { get; set; }
        public string Base_Calendar_Code { get; set; }
        public int Balance_LCY { get; set; }
        public int Balance_Due_LCY { get; set; }
        public int Payments_LCY { get; set; }
        public bool Coupled_to_CRM { get; set; }
        public string Global_Dimension_1_Filter { get; set; }
        public string Global_Dimension_2_Filter { get; set; }
        public string Currency_Filter { get; set; }
        public string Date_Filter { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string Title { get; set; }

        public string FullName { get; set; }

        public string RecordType { get; set; }
        public string CurrentPositionID { get; set; }
        public string JobTitle2 { get; set; }
        public string SearchName { get; set; }
        public string CountyofOrigin { get; set; }
        public string CountyofOriginName { get; set; }
        public string CountyofResidence { get; set; }
        public string CountyofResidenceName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public bool Disabled { get; set; }
        public string DisabilityNo { get; set; }
        public DateTime DisabilityCertExpiry { get; set; }
        public bool InsuranceCertificate { get; set; }
        public string EthnicOrigin { get; set; }
        public DateTime LastDateModified { get; set; }

        public string DepartmentName { get; set; }
        public string ResponsibilityCenter { get; set; }
        public bool HeadofStation { get; set; }
        public bool HOD { get; set; }
        public bool IsChiefJustice { get; set; }
        public bool IsPartOfDisciplinaryTeam { get; set; }
        public bool ICTHelpDeskAdmin { get; set; }
        public string Supervisor { get; set; }
        public string ReliverCode { get; set; }
        public string SalaryScale { get; set; }
        public string Present { get; set; }
        public string EmployeePostingGroup1 { get; set; }
        public DateTime IncrementDate { get; set; }
        public string IncrementalMonth { get; set; }
        public DateTime LastIncrementDate { get; set; }
        public string DirectorateCode { get; set; }
        public string ImplementingUnitName { get; set; }
        public string DepartmentCode { get; set; }
        public string DutyStation { get; set; }
        public string OrgUnit { get; set; }
        public string OrganisationUnitName { get; set; }
        public string AccountType { get; set; }
        public string Division { get; set; }
        public string EmployeeJobType { get; set; }
        public string CourtLevel { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string CountryRegionCode { get; set; }
        public string CitizenshipType { get; set; }
        public string EmployeeType { get; set; }
        public string PhoneNo { get; set; }
        public string Extension { get; set; }
        public string MobilePhoneNo { get; set; }
        public string Pager { get; set; }
        public string PhoneNo2 { get; set; }
        public string CompanyEmail { get; set; }
        public string AltAddressCode { get; set; }
        public DateTime AltAddressStartDate { get; set; }
        public DateTime AltAddressEndDate { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string Ext { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime EndOfProbationDate { get; set; }
        public bool Inducted { get; set; }
        public DateTime RetirementDate { get; set; }
        public string FullPartTime { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public DateTime ReEmploymentDate { get; set; }
        public string NoticePeriod { get; set; }
        public string Status { get; set; }
        public string EmployeeStatus2 { get; set; }
        public DateTime InactiveDate { get; set; }
        public string CauseofInactivityCode { get; set; }
        public string EmplymtContractCode { get; set; }
        public string ResourceNo { get; set; }
        public string SalespersPurchCode { get; set; }
        public string Disciplinarystatus { get; set; }
        public string Reasonfortermination { get; set; }
        public DateTime TerminationDate { get; set; }
        public DateTime DateOfLeaving { get; set; }
        public string ExitInterviewConducted { get; set; }
        public DateTime ExitInterviewDate { get; set; }
        public string ExitInterviewDoneby { get; set; }
        public int BondingAmount { get; set; }
        public string ExitStatus { get; set; }
        public bool AllowReEmploymentInFuture { get; set; }
        public string CurrencyCode { get; set; }
        public bool Paystax_x003F_ { get; set; }
        public bool PayWages { get; set; }
        public string PayMode { get; set; }
        public string PIN { get; set; }
        public string NHIFNo { get; set; }
        public string SocialSecurityNo { get; set; }
        public string IDNumber { get; set; }
        public string PostingGroup { get; set; }
        public int ClaimLimit { get; set; }
        public string BankAccountNumber { get; set; }
        public string Employeex0027sBank { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankBranchName { get; set; }
        public string Employeex0027sBank2 { get; set; }
        public string BankName2 { get; set; }
        public string BankBranch2 { get; set; }
        public string BankBranchName2 { get; set; }
        public string BankAccountNo2 { get; set; }
        public bool AllowNegativeLeave { get; set; }
        public int OffDays { get; set; }
        public int LeaveDaysBF { get; set; }
        public int AllocatedLeaveDays { get; set; }
        public int TotalLeaveDays { get; set; }
        public int TotalLeaveTaken { get; set; }
        public string AdministrativeUnit { get; set; }
        public int LeaveOutstandingBal { get; set; }
        public int LeaveBalance { get; set; }
        public int AcruedLeaveDays { get; set; }
        public int CashperLeaveDay { get; set; }
        public int CashLeaveEarned { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveTypeFilter { get; set; }
        public string LeavePeriodFilter { get; set; }
        public bool OnLeave { get; set; }
        public string DateFilter { get; set; }
        public List<EmployeeDependant> EmployeeDependants { get; set; }
        public List<EmployeeQualification> EmployeeQualifications { get; set; }
        public List<AssignedDimensions> EmployeeDimensions { get; set; }
        public int AllocatedAssets { get; set; }
        public string UserRole { get; set; }
        public bool IsSupervisor { get; set; }
        public int AssignedPurchaseReq { get; set; }
    }



    public class AssignedDimensions
    {
        public string GlobalDim1 { get; set; }
        public string GlobalDim2 { get; set; }
    }
    public class EmpInitial
    {
        public string Code { get; set; }
    }

    public class LeaveBalView
    {
        public string No { get; set; }
        public string Name { get; set; }
        public string IDNo { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string LeaveType { get; set; }
        public string CarryForward { get; set; }
        public string LeaveTaken { get; set; }
        public string TotalBal { get; set; }
        public string Available { get; set; }
        public string Earned { get; set; }
        public string Allocated { get; set; }
    }

    public class EmployeeDependant
    {
        public string EmployeeCode { get; set; }
        public string Type { get; set; }
        public string Relationship { get; set; }
        public string SurName { get; set; }
        public string OtherNames { get; set; }
        public int LineNo { get; set; }
        public string MemberID { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Category { get; set; }
        public string CardNo { get; set; }
        public string NumberInTheFamily { get; set; }
    }

    public class EmployeeQualification
    {
        public string EmployeeNo { get; set; }
        public int LineNo { get; set; }
        public string QualificationCategory { get; set; }
        public string QualificationCode { get; set; }
        public string Description { get; set; }
        public string InstitutionCompany { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Type { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Cost { get; set; }
        public string Year { get; set; }
        public string Specialization { get; set; }
        public string CourseGrade { get; set; }
        public bool Comment { get; set; }
    }

    public class TalentManagementCard
    {
        public string No { get; set; }
        public string Fullname { get; set; }

        public string Talent9BoxCode { get; set; }
        public string PotentialLevel { get; set; }
        public string PerformanceLevel { get; set; }
    }

    public class TrainingHistory
    {
        public string TrainingNo { get; set; }
        public int LineNo { get; set; }
        public string EmployeeNo { get; set; }
        public string Training { get; set; }
        public string Institution { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Funding { get; set; }
    }

    public class Attendance
    {
        public int EntryNo { get; set; }
        public DateTime ClockinDate { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public TimeSpan ClockinTime { get; set; }
        public TimeSpan ClockoutTime { get; set; }
    }

    public class ESSRoleSetup
    {
        public string OdataEtag { get; set; }
        public string User_ID { get; set; }
        public string UserName { get; set; }
        public string Employee_Name { get; set; }
        public string Employee_No { get; set; }
        public bool Accounts_User { get; set; }
        public bool Accounts_Approver { get; set; }
        public bool Audit_Officer { get; set; }
        public bool Audit__x0026__Risk_Officer { get; set; }
        public bool HQ_Accountant { get; set; }
        public bool HQ_Finance_Officer { get; set; }
        public bool HQ_Procurement_Officer { get; set; }
        public bool Station_Accountant { get; set; }
        public bool Station_Procurement_Office { get; set; }
        public bool DAAS_Officer { get; set; }
        public bool HR_Welfare_Officer { get; set; }
        public bool Mobility_Officer { get; set; }
        public bool Procurement_officer { get; set; }
        public bool Recruitment_Officer { get; set; }
        public bool Revenue_Officer { get; set; }
        public bool Transport_Officer { get; set; }
    }
}