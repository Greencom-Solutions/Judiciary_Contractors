function navigateTo(url) {
    ShowProgress();
    window.location = url;
}
var DashBoardLink = function () {
    navigateTo("/Dashboard/Dashboard");
};
var PersonalProfile = function () {
    navigateTo("/Dashboard/PersonalProfile");
};
var ProgrammeListLink = function () {
    navigateTo("/Programme/ProgrammeList");
};
var DepositReceiptsLink = function () {
    navigateTo("/Deposits/DepositReceipt");
};
var ReceiptsLink = function () {
    navigateTo("/Receipts/NormalReceipt?status=Open");
};
var ApprovedReceipts = function () {
    navigateTo("/Receipts/NormalReceipt?status=Released");
};
var PostedReceipts = function () {
    navigateTo("/Receipts/NormalReceipt?status=Posted");
};
var ArchivedReceipts = function () {
    navigateTo("/Receipts/NormalReceipt?status=Archived");
};
var UtilizationReceiptLink = function () {
    navigateTo("/Deposits/UtilizationReceipt?status=Open");
};
var ApprovedUtilizationReceiptLink = function () {
    navigateTo("/Deposits/UtilizationReceipt?status=Released");
};
var PostedUtilizationReceipts = function () {
    navigateTo("/Deposits/UtilizationReceipt?status=Posted");
};
var ArchivedUtilizationReceipts = function () {
    navigateTo("/Deposits/UtilizationReceipt?status=Archived");
};
var RevenueReceiptLink = function () {
    navigateTo("/Deposits/RevenueReceipt");
};
var ForfeitureLink = function () {
    navigateTo("/Deposits/ForfeitureRefund?status=Open");
};
var ArchivedForfeitureLink = function () {
    navigateTo("/Deposits/ForfeitureRefund?status=Archived");
};
var PostedForfeitureLink = function () {
    navigateTo("/Deposits/ForfeitureRefund");
};
var RefundLink = function () {
    navigateTo("/Deposits/FullRefund?status=Open");
};
var ArchivedRefundLink = function () {
    navigateTo("/Deposits/FullRefund?status=Archived");
};
var ApprovedRefundLink = function () {
    navigateTo("/Deposits/FullRefund?status=Released");
};
var PostedRefundLink = function () {
    navigateTo("/Deposits/FullRefund?status=Posted");
};
var CourtCollection = function () {
    navigateTo("/Deposits/CourtCollectionVoucher?status=Open");
};
var ArchivedCourtCollection = function () {
    navigateTo("/Deposits/CourtCollectionVoucher?status=Archived");
};
var ApprovedCourtCollection = function () {
    navigateTo("/Deposits/CourtCollectionVoucher?status=Released");
};
var PostedCourtCollection = function () {
    navigateTo("/Deposits/CourtCollectionVoucher?status=Posted");
};
var UtilizationLink = function () {
    navigateTo("/Deposits/FullUtilization?status=Open");
};
var ArchivedUtilizationLink = function () {
    navigateTo("/Deposits/FullUtilization?status=Archived");
};
var ApprovedUtilizationLink = function () {
    navigateTo("/Deposits/FullUtilization?status=Released");
};
var ApprovedForfeitureLink = function () {
    navigateTo("/Deposits/FullRefund?status=Released");
};
var PostedUtilizationLink = function () {
    navigateTo("/Deposits/FullUtilization?status=Posted");
};
var PurchaseInvoice = function () {
    navigateTo("/PurchaseInvoice/PurchaseInvoice?status=Released");
};
var SubmittedPurchaseOder = function () {
    navigateTo("/Purchase/PurchaseOrdersList?stage=accl");
};
var PostedPurchaseInvoices = function () {
    navigateTo("/PurchaseInvoice/PurchaseInvoice?status=Posted");
};
var ArchivedPurchaseInvoices = function () {
    navigateTo("/PurchaseInvoice/PurchaseInvoice?status=Archived");
};
var PartTimeRequisitionLink = function () {
    navigateTo("/Lecturer/PartTimeRequistionList");
};
var CourseAllocationLink = function () {
    navigateTo("/Lecturer/CourseAllocationUnits");
};
var VoteBookBalanceLink = function () {
    navigateTo("/ViewDocuments/ViewDocuments?DocT=VOTEBOOK");
};
var LeaveRequisitionLink = function () {
    navigateTo("/Leave/LeaveRequisitionList");
};
var LeaveCommutationLink = function () {
    navigateTo("/Leave/LeaveCommutation");
};
var ConsolidatedLeaveCommutationLink = function () {
    navigateTo("/Leave/ConsolidatedLeaveCommutation");
};

var LeaveDashboardLink = function () {
    navigateTo("/Dashboard/HumanResource");
};
var PurchaseRequisitionLink = function () {
    navigateTo("/Purchase/PurchaseRequisitionList");
};
var ApprovedPurchaseRequisitionLink = function () {
    navigateTo("/Purchase/ApprovedPurchaseRequisition");
};
var RequestForQuotationsLink = function () {
    navigateTo("/Purchase/RequestForQuotations");
};
var FrameworkContractLink = function () {
    navigateTo("/Purchase/FrameworkContract");
};
var Sharepoint = function () {
    navigateTo("/ViewDocuments/SharepointIntegration");
};
var DirectProcurementsLink = function () {
    navigateTo("/Purchase/DirectProcurements");
};
var IFSTenderCommitteeLink = function () {
    navigateTo("/Purchase/IFSTenderCommittee");
};
var PoolCommitteeLink = function () {
    navigateTo("/Purchase/PoolCommittee");
};
var AssignedPurchaseRequisitionLink = function () {
    navigateTo("/Purchase/AssignedPurchaseRequisition");
};
var PurchaseOrderLink = function () {
    navigateTo("/Purchase/PurchaseOrdersList?stage=scm");
};
var StoreRequisitionLink = function () {
    navigateTo("/Store/StoreRequisition");
};
var StoreRequisitionListLink = function (status) { navigateTo("/Store/StoreRequisition?status=" + status); };
var BidOpeningRegisterLink = function () {
    navigateTo("/BidResponseEvaluation/BidOpeningRegisterList");
};
var ImprestMemoLink = function () {
    navigateTo("/Imprest/ImprestMemo");
};
var ImprestSurrenderLink = function () {
    navigateTo("/ImprestSurrender/ImprestSurrender");
};
var StandingImprestLink = function () {
    navigateTo("/Imprest/StandingImprest");
};
var ImprestWarrantiesLink = function () {
    navigateTo("/Imprest/ImprestWarrant");
};
var CommitteeLink = function () {
    navigateTo("/Asset/Committee");
};
var FADueDisposalLink = function () {
    navigateTo("/Asset/FADueDisposal");
};
var SpecialImprestLink = function () {
    navigateTo("/Imprest/SpecialImprest");
};
var WorkShopAdvanceLink = function () {
    navigateTo("/WorkShopAdvance/WorkShopAdvanceRequisitionList");
};
var WorkShopAdvanceSurrenderLink = function () {
    navigateTo("/WorkshopLiquidation/WorkShopAdvanceLiquidationList");
};
var PettyCashLink = function () {
    navigateTo("/PettyCash/PettyCashRequisitionList");
};
var StaffClaimsLink = function () {
    navigateTo("/StaffClaim/StaffClaims");
};
var OpenStaffClaimsLink = function () {
    navigateTo("/StaffClaim/StaffClaims?status=Open");
};
var PendingStaffClaimsLink = function () {
    navigateTo("/StaffClaim/StaffClaims?status=Pending Approval");
};
var ReleasedStaffClaimsLink = function () {
    navigateTo("/StaffClaim/StaffClaims?status=Released");
};
var AlltimeDepositLink = function () {
    navigateTo("/PowerBi/AllTimeDeposits");
};
var DailyMonthlyLink = function () {
    navigateTo("/PowerBi/DailyMonthlyTrends");
};
var TodaysDepostLink = function () {
    navigateTo("/PowerBi/TodaysDeposit");
};
var AllReportsLink = function () {
    navigateTo("/PowerBi/AllReports");
};
var PowerBiLink = function () {
    navigateTo("/PowerBi/Deposits");
};
var AppraisalRequisitionLink = function () {
    navigateTo("/Appraisal/MyAppraisalList");
};
var SupervisorRequisitionReviewLink = function () {
    navigateTo("/Appraisal/MyReviewAppraisalList");
};
var TransportRequisitionLink = function () {
    navigateTo("/Transport/TransportRequisitionList");
};
var TrainingRequisitionLink = function () {
    navigateTo("/Training/TrainingRequisitionList");
};
var ConsolidatedTrainingNeeds = function () {
    navigateTo("/Training/ConsolidatedTrainingNeeds");
};
var TrainingExtensionLink = function () {
    navigateTo("/Training/TrainingExtensionRequisitionList");
};
var TrainingNeedsLink = function () {
    navigateTo("/Training/TrainingNeedsList");
};
var TrainingFeedbackLink = function () {
    navigateTo("/Training/TrainingFeedbackRequisitionList");
};
var TrainingEvaluationLink = function () {
    navigateTo("/Training/TrainingEvaluationRequisitionList");
};
var FuelRequisitionLink = function () {
    navigateTo("/Fuel/FuelCardList");
};
var FuelRequestLink = function () {
    navigateTo("/Fuel/FuelRequisitionList");
};
var VehicleServicingMaintenanceLink = function () {
    navigateTo("/Maintenance/MaintenanceRequisitionList");
};
var ICTRequisitionLink = function () {
    navigateTo("/ICT/ICTRequisitionList");
};
var ICTAssetRequisitionLink = function () {
    navigateTo("/ICT/ICTAssetTransferList");
};
var ICTAssetServicingMaintenanceLink = function () {
    navigateTo("/ICT/ICTServMntList");
};
var ICTAssignmentLink = function () {
    navigateTo("/ICT/ICTAssignmentList");
};
var NewVisitorLink = function () {
    navigateTo("/Visitors/NewVisitorsList");
};
var ActiveVisitorsLink = function () {
    navigateTo("/Visitors/ActiveVisitorsList");
};
var ClearedVisitorsLink = function () {
    navigateTo("/Visitors/ClearedVisitorsList");
};
var GatePassLink = function () {
    navigateTo("/GatePass/GatePassList");
};
var ApprovedGatePassLink = function () {
    navigateTo("/GatePass/ApprovedGatePassList");
};
var AssignedAssetLink = function () {
    navigateTo("/Asset/AssignedAssetList");
};
var AssetTransferLink = function () {
    navigateTo("/Asset/AssetTransferList");
};
var FixedAssetLink = function () {
    navigateTo("/Asset/FixedAsset");
};
var PayslipViewLink = function () {
    navigateTo("/ViewDocument/DocumentViewPayslip");
};
var P9ViewLink = function () {
    navigateTo("/ViewDocument/DocumentViewp9");
};
var LeaveStatementLink = function () {
    navigateTo("/ViewDocument/GetLeaveStatementReport");
};
var DocumentApprovalSummaryLink = function () {
    navigateTo("/DocumentApproval/DocumentForApprovalSummery?rn=Open");
};
var ApprovedDocLink = function () {
    navigateTo("/DocumentApproval/DocumentForApprovalSummery?rn=Approved");
};
var RejectedDocLink = function () {
    navigateTo("/DocumentApproval/DocumentForApprovalSummery?rn=Rejected");
};
var ChangePasswordLink = function () {
    navigateTo("/Settings/ChangePassword");
};
var PCALink = function () {
    navigateTo("/PCA/PCAs");
};
var TimeSheetLink = function () {
    navigateTo("/TimeSheet/TimeSheetList");
};
var EmployeeRequisitionLink = function () {
    navigateTo("/EmployeeRequistion/EmployeeRequisitionList");
};
var StaffInductionLink = function () {
    navigateTo("/StaffInduction/StaffInduction");
};
var ExitInterviewLink = function () {
    navigateTo("/StaffClearance/ExitInterview");
};
var StaffClearanceLink = function () {
    navigateTo("/StaffClearance/StaffClearance");
};
var LeaveReimbursement = function () {
    navigateTo("/LeaveReimbursement/LeaveReimbursementList");
};
var TimeOffLieu = function () {
    navigateTo("/TimeOffLieu/TimeOffLieuList");
};
var EmployeesOnLeaveLink = function () {
    navigateTo("/Leave/EmployeesOnLeaveList");
};
var EmployeesApprovedLeaveLink = function () {
    navigateTo("/Leave/EmployeesApprovedLeaveList");
};
var CarryForward = function () {
    navigateTo("/TimeOffLieu/CarryForwardList");
};
var LeaveAllocationLink = function () {
    navigateTo("/TimeOffLieu/LeaveAllocationList");
};
var SelfEmpTransferLink = function () {
    navigateTo("/Mobility/EmployeeTransferList");
};
var HRDisciplinaryCaseLink = function () {
    navigateTo("/Disciplinary/HRDicsiplinaryCasesList");
};
var ManagementTransferLink = function () {
    navigateTo("/Mobility/ManagementTransferList");
};
var PropertyListLink = function () {
    navigateTo("/FacilitiesAdministration/PropertyList");
};
var WorkTicketLink = function () {
    navigateTo("/Transport/WorkTicket");
};
var ReservationListLink = function () {
    navigateTo("/FacilitiesAdministration/ReservationRequestList");
};
var ApprovedListLink = function () {
    navigateTo("/FacilitiesAdministration/AprrovedReservationList");
};
var PerformanceAppraisalSystem = function () {
    navigateTo("/Pas/IndividualScorecardList");
};
var ArchivedPurchaseOrderLink = function () {
    navigateTo("/Purchase/ArchivedPurchaseOrder");
};
var ProcurementDashboardLink = function () {
    navigateTo("/Dashboard/Procurement");
};
var PMMULink = function () {
    navigateTo("/Performance/PMMU");
};
var AppointmentCommitteeLink = function () {
    navigateTo("/Performance/CommitteeAppointmentVouchers");
};
var LeavePlanner = function () {
    navigateTo("/Leave/LeavePlanner");
};
var CarLoan = function () {
    navigateTo("/WelfareManagement/CarLoanApplicationsList");
};
var MortgageLoan = function () {
    navigateTo("/WelfareManagement/MortgageLoanApplicationsList");
};
var AdvancedSalary = function () {
    navigateTo("/WelfareManagement/SalaryAdvanceLoanApplicationsList");
};
var MedicalCardReplacement = function () {
    navigateTo("/WelfareManagement/MedicalCardReplacement");
};
var VehicleIncidence = function () {
    navigateTo("/Transport/VehicleIncidentView");
};
var DependantsChangeRequest = function () {
    navigateTo("/WelfareManagement/DependantChangeRequests");
};
var HumanResourceLink = function () {
    navigateTo("/Dashboard/HumanResource");
};
var ProjectsLink = function () {
    navigateTo("/Dashboard/Projects");
};
var DSPOPLink = function () {
    navigateTo("/Dashboard/DSPOP");
};
var DASSLink = function () {
    navigateTo("/Dashboard/DASS");
};
var FAQsLink = function () {
    navigateTo("/Common/FAQsView");
};
var HRInformation = function () {
    navigateTo("/Common/HRInformation");
};
var AttendanceList = function () {
    navigateTo("/Dashboard/Attendance");
};
var FinanceLink = function () {
    navigateTo("/Dashboard/Finance");
};
var AccountsLink = function () {
    navigateTo("/Dashboard/Accounts");
};

var ProcurementLink = function () {
    navigateTo("/BidEvaluation/EvaluationProcess");
};
var FunctionalDisposalPlanLink = function () {
    navigateTo("/Asset/FunctionalDisposalPlan");
};
var ConsolidatedDisposalPlanLink = function () {
    navigateTo("/Asset/ConsolidatedDisposalPlan");
};
ProcurementDashboard = function () {
    navigateTo("/Dashboard/Procurement");
};
var ResourceRequirementsLink = function () {
    navigateTo("/Workplans/ResourceRequirements");
};
var ExpenseRequisitionLink = function () {
    navigateTo("/ExpenseRequisition/ExpenseRequisition");
};
var AIE = function () {
    navigateTo("/ExpenseRequisition/AieView?aieType=Normal");
};
var SpecialAie = function () {
    navigateTo("/ExpenseRequisition/AieView?aieType=Special");
};
var FunctionalProcurementPlanLink = function () {
    navigateTo("/Purchase/FunctionalProcurementPlan");
};

var RevisionProcurementPlanLink = function () {
    navigateTo("/Purchase/RevisionVouchers");
};

var ProcurementPlanAmmendmentsLink = function () {
    navigateTo("/Purchase/ProcurementPlanAmmendments");
};
var DraftWorkPlansLink = function () {
    navigateTo("/Workplans/DraftWorkPlans");
};
var SuppWorkPlansLink = function () {
    navigateTo("/Workplans/SupplementaryWorkPlans");
};
var ReallocationLink = function () {
    navigateTo("/Workplans/BudgetReallocation");
};
var OpenUserRequestLink = function () {
    navigateTo("/ProjectManagement/UserRequests?status=Open");
};
var SubmittedUserRequestLink = function () {
    navigateTo("/ProjectManagement/UserRequests?status=Submitted");
};
var ReceivedUserRequestLink = function () {
    navigateTo("/ProjectManagement/UserRequests?status=Received");
};
var TeamLeadSelectionLink = function () {
    navigateTo("/ProjectManagement/TeamLeadSelection");
};
var ProjectTeamSelectionLink = function () {
    navigateTo("/ProjectManagement/ProjectTeamSelection");
};
var FirstStakeholderMeetingLink = function () {
    navigateTo("/ProjectManagement/FirstStakeholderMeetingList");
};


var SubmittedDesignControlLink = function () {
    navigateTo("/ProjectManagement/SubmittedDesignControlList");
};


var PreliminaryDesignControlLink = function (status) { navigateTo("/ProjectManagement/PreliminaryDesignControlList?status=" + status); }
var AnnexSevenLink = function () { navigateTo("/ProjectManagement/AnnexSevenList"); };
var DesignControlProjectsLink = function (status) { navigateTo("/ProjectManagement/DesignControlProjectsList?status=" + status); }
var FinalDesignControlLink = function (status) { navigateTo("/ProjectManagement/FinalDesignControlList?status=" + status); }

var FinalConceiptNoteLink = function (status) { navigateTo("/Budgeting/FinalConceiptNote?status=" + status); }
var FinalConceiptNoteConsolidationLink = function (status) { navigateTo("/Budgeting/FinalConceiptNoteConsolidation?status=" + status); }
var DraftWorkPlanLink = function (status) { navigateTo("/Budgeting/DraftWorkPlans?status=" + status); }
var AuthorityToExpensesLink = function (status) { navigateTo("/Budgeting/AieView?status=" + status); }
var BatchAuthorityToExpensesLink = function (status) { navigateTo("/Budgeting/BatchAieView?status=" + status); }
var ConsolidationAuthorityToExpensesLink = function (status) { navigateTo("/Budgeting/ConsolidationAieView?status=" + status); }
var SpecialAuthorityToExpensesLink = function (status) { navigateTo("/Budgeting/AieView?aie=Special&status=" + status); }

var ExpenditureRequisitionsLink = function (status) { navigateTo("/Budgeting/ExpenseRequisition?status=" + status); }


var BudgetReallocations = function (status) { navigateTo("/Budgeting/BudgetReallocation"); }
var ConsolidatedReallocations = function (status) { navigateTo("/Budgeting/ConsolidatedReallocation"); }
var SupplimentaryBudgetLink = function (status) { navigateTo("/Budgeting/SupplimentaryBudget"); }

var ContractorPaymentControlLink = function () {
    navigateTo("/ProjectManagement/ContractorPaymentControlList");
};

var ProjectProposalLink = function () {
    navigateTo("/Projects/ProjectProposal");
};

var PaymentRequestLink = function (status) { navigateTo("/Procurement/ContractorPaymentRequests?status=" + status); }

var DesignControl = function () {
    navigateTo("/Projects/DesignControl");
};

var ContractorPaymentLink = function (status) {
    navigateTo(
        "/Contractor/ContractorPaymentRequests?status=" + status
    );
};

var ContractorPaymentTeamApprovalLink = function (teamApprovalStatus) {
    navigateTo(
        "/Contractor/ContractorPaymentRequests?status=" +
        encodeURIComponent("Team Approval") +
        "&teamApprovalStatus=" +
        encodeURIComponent(teamApprovalStatus)
    );
};



var ContractorExtensionLink = function (status) {
    navigateTo("/contractor/ContractorExtensionRequests?status=" + status);
};

var ContractorExtensionTeamApprovalLink = function (teamApprovalStatus) {
    navigateTo(
        "/contractor/ContractorExtensionRequests?status=" +
        encodeURIComponent("Team Approval") +
        "&teamApprovalStatus=" +
        encodeURIComponent(teamApprovalStatus)
    );
};


var ContractorAmmendedLink = function (status) {
    navigateTo("/contractor/ContractorAmmendedRequests?status=" + status);
};

var ContractorAmmendedTeamApprovalLink = function (teamApprovalStatus) {
    navigateTo(
        "/contractor/ContractorAmmendedRequests?status=" +
        encodeURIComponent("Team Approval") +
        "&teamApprovalStatus=" +
        encodeURIComponent(teamApprovalStatus)
    );
};
var ContractorAmmendedContractorLink = function (ContractorStatus) {
    navigateTo(
        "/contractor/ContractorAmmendedRequests?status=" +
        encodeURIComponent("Contractor") +
        "&teamApprovalStatus=" +
        encodeURIComponent(ContractorStatus)
    );
};


var ContractorInstructionLink = function (status) {
    navigateTo("/contractor/ContractorInstructionRequests?status=" + status);
};


var ContractorApprovalLink = function (status) {
    navigateTo("/contractor/ContractorApprovalRequests?status=" + status);
};

var ContractorHOSLink = function (status) {
    navigateTo("/contractor/ContractorHOSRequests?status=" + status);
};

var ContractorPMCommunicationLink = function (status) {
    navigateTo("/contractor/ContractorPMCommunication?status=" + status);
};

var ContractorPracticalRequestLink = function (status) {
    navigateTo("/contractor/ContractorPracticalCompletion?status=" + status);
};

var ContractorPracticalTeamApprovalLink = function (teamApprovalStatus) {
    navigateTo(
        "/contractor/ContractorPracticalCompletion?status=" +
        encodeURIComponent("Team Approval") +
        "&teamApprovalStatus=" +
        encodeURIComponent(teamApprovalStatus)
    );
};


var ContractorGoodDefectsRequestLink = function (status) {
    navigateTo("/contractor/ContractorGoodDefects?status=" + status);
};

var ContractorGoodDefectsTeamApprovalLink = function (teamApprovalStatus) {
    navigateTo(
        "/contractor/ContractorGoodDefects?status=" +
        encodeURIComponent("Team Approval") +
        "&teamApprovalStatus=" +
        encodeURIComponent(teamApprovalStatus)
    );
};

ProjectClosureLink = function () { navigateTo("/contractor/ProjectClosure"); };
ClosedProjectsLink = function () { navigateTo("/contractor/ClosedProjects"); };


InspectionLink = function () { navigateTo("/Procurement/InspectionList"); };
InspectionCommitteeLink = function () { navigateTo("/Purchase/InspectionCommittee"); };


var ProjectAssignmentEntriesLink = function (DocType) {
    navigateTo("/contractor/ProjectAssignmentEntries?DocType=" + DocType);
};

var ExtensionRequestLink = function (status) { navigateTo("/Procurement/ContractorExtensionRequests?status=" + status); }
var PracticalRequestLink = function (status) { navigateTo("/Procurement/PracticalCompletionRequests?status=" + status); }
var ExtensionRequestLink = function (status) {
    navigateTo("/Procurement/ContractorExtensionRequests?status=" + status);
};
var PracticalRequestLink = function (status) {
    navigateTo(
        "/Procurement/PracticalCompletionRequests?status=" + status
    );
};
var AmmendedRequestLink = function (status) {
    navigateTo("/Procurement/ContractorAmmendedRequests?status=" + status);
};
var MakingGoodDefectsRequestLink = function (status) {
    navigateTo("/Procurement/ContractorMakingGoodDefects?status=" + status);
};

var BankAccountsLink = function () {
    navigateTo("/Deposits/BankLedgerEntries");
};
var ContractRequisitionLink = function () {
    navigateTo("/Contract/ContractList");
};
var PvLink = function () {
    navigateTo("/PaymentVoucher/PaymentVoucherView");
};
var TPvLink = function () {
    navigateTo("/PaymentVoucher/TaxPaymentVoucherView");
};
var Retention = function () {
    navigateTo("/Retention/RetentionView");
};
var EftLink = function () {
    navigateTo("/Eft/EftView");
};
var DraftTenderInvitationLink = function () {
    navigateTo("/Purchase/InvitationToTender");
};
var ProfessionalOpinionLink = function () {
    navigateTo("/BidResponseEvaluation/ProfessionalOpinion");
};
var BidResponseLink = function () {
    navigateTo("/BidResponseEvaluation/BidResponseList");
};
var EvaluationTemplateLink = function () {
    navigateTo("/BidResponseEvaluation/EvaluationTemplate");
};
var PremBidEvaluation = function () {
    navigateTo("/BidResponseEvaluation/PreliminaryEvaluationView");
};
var TechnicalBidEvaluation = function () {
    navigateTo("/BidResponseEvaluation/TechnicalEvaluationView");
};
var FinanceBidEvaluation = function () {
    navigateTo("/BidResponseEvaluation/FinancialEvaluationView");
};
var FinalEvaluation = function () {
    navigateTo("/BidResponseEvaluation/FinalEvaluationView");
};
var ScoreCardListLink = function () {
    navigateTo("/Pas/IndividualScoreCardList");
};
var SelfSponsoredTrainingLink = function () {
    navigateTo("/Training/SelfSponsored");
};
var SupervisorRequisitiontReviewLink = function () {
    navigateTo("/Pas/SupervisorsApprisals");
};
var PASReportLink = function () {
    navigateTo("/Pas/EmployeePASReport");
};
var TargetNotSetReportLink = function () {
    navigateTo("/Pas/TargetNotSetReport");
};
var FunctionalDisposalLstLink = function () {
    navigateTo("/Asset/FunctionalDisposalPlan");
};
var ExtensionRequestLink = function (status) {
    navigateTo("/Procurement/ContractorExtensionRequests?status=" + status);
};
var AmmendedRequestLink = function (status) {
    navigateTo("/Procurement/ContractorAmmendedRequests?status=" + status);
};
var MakingGoodDefectsRequestLink = function (status) {
    navigateTo("/Procurement/ContractorMakingGoodDefects?status=" + status);
};

var BankAccountsLink = function () {
    navigateTo("/Deposits/BankLedgerEntries");
};
var ContractRequisitionLink = function () {
    navigateTo("/Contract/ContractList");
};
var ContractsLink = function () {
    navigateTo("/Contracts/ContractsList");
};
var PvLink = function () {
    navigateTo("/PaymentVoucher/PaymentVoucherView");
};
var TPvLink = function () {
    navigateTo("/PaymentVoucher/TaxPaymentVoucherView");
};
var Retention = function () {
    navigateTo("/Retention/RetentionView");
};
var EftLink = function () {
    navigateTo("/Eft/EftView");
};
var DraftTenderInvitationLink = function () {
    navigateTo("/Purchase/InvitationToTender");
};
var ProfessionalOpinionLink = function () {
    navigateTo("/BidResponseEvaluation/ProfessionalOpinion");
};
var BidResponseLink = function () {
    navigateTo("/BidResponseEvaluation/BidResponseList");
};
var EvaluationTemplateLink = function () {
    navigateTo("/BidResponseEvaluation/EvaluationTemplate");
};
var PremBidEvaluation = function () {
    navigateTo("/BidResponseEvaluation/PreliminaryEvaluationView");
};
var TechnicalBidEvaluation = function () {
    navigateTo("/BidResponseEvaluation/TechnicalEvaluationView");
};
var FinanceBidEvaluation = function () {
    navigateTo("/BidResponseEvaluation/FinancialEvaluationView");
};
var FinalEvaluation = function () {
    navigateTo("/BidResponseEvaluation/FinalEvaluationView");
};
var ScoreCardListLink = function () {
    navigateTo("/Pas/IndividualScoreCardList");
};
var SelfSponsoredTrainingLink = function () {
    navigateTo("/Training/SelfSponsored");
};
var SupervisorRequisitiontReviewLink = function () {
    navigateTo("/Pas/SupervisorsApprisals");
};
var PASReportLink = function () {
    navigateTo("/Pas/EmployeePASReport");
};
var TargetNotSetReportLink = function () {
    navigateTo("/Pas/TargetNotSetReport");
};
var FunctionalDisposalLstLink = function () {
    navigateTo("/Asset/FunctionalDisposalPlan");
};

var PurchaseOrdersLink = function (stage) {
    var stageParam = stage === "SCM" ? "scm" : stage === "Accounts" ? "accl" : "";
    navigateTo(`/Purchase/PurchaseOrdersList?stage=${stageParam}`);
};
var Registrationrequests = function () {
    navigateTo("/Procurement/RegistrationRequestsList");
};
var VerifiedRequests = function () {
    navigateTo("/Procurement/VerifiedRequestList");
};
var ConsolidatedSupplier = function () {
    navigateTo("/Procurement/ConsolidatedSupplierList");
};

var DisposalLink = function () {
    navigateTo("/Asset/FADueDisposal");
};
