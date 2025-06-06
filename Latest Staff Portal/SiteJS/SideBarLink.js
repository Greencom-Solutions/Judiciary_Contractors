function navigateTo(url) {
    ShowProgress();
    window.location = url;
}
var DashBoardLink = function () { navigateTo("/Dashboard/Dashboard"); }
var PersonalProfile = function () { navigateTo("/Dashboard/PersonalProfile"); }
var Sharepoint = function () { navigateTo("/ViewDocuments/SharepointIntegration"); }
var ProjectsLink = function () { navigateTo("/Dashboard/Projects"); }

var OpenUserRequestLink = function () { navigateTo("/ProjectManagement/UserRequests?status=Open"); }
var SubmittedUserRequestLink = function () { navigateTo("/ProjectManagement/UserRequests?status=Submitted"); }
var ReceivedUserRequestLink = function () { navigateTo("/ProjectManagement/UserRequests?status=Received"); }
var TeamLeadSelectionLink = function () { navigateTo("/ProjectManagement/TeamLeadSelection"); }
var ProjectTeamSelectionLink = function () { navigateTo("/ProjectManagement/ProjectTeamSelection"); }
var FirstStakeholderMeetingLink = function () { navigateTo("/ProjectManagement/FirstStakeholderMeetingList"); }
var ApprovedDesignControlLink = function () { navigateTo("/ProjectManagement/ApprovedDesignControlList"); }
var SubmittedDesignControlLink = function () { navigateTo("/ProjectManagement/SubmittedDesignControlList"); }
var FinalDesignControlLink = function () { navigateTo("/ProjectManagement/FinalDesignControlList"); }
var PreliminaryDesignControlLink = function () { navigateTo("/ProjectManagement/PreliminaryDesignControlList"); }
var AnnexSevenLink = function () { navigateTo("/ProjectManagement/AnnexSevenList"); }
var ProjectProposalLink = function () { navigateTo("/Projects/ProjectProposal"); }

var DesignControl = function () { navigateTo("/Projects/DesignControl"); }


//contractor portal
var ContractsLink = function () { navigateTo("/Contracts/ContractsList"); }
var ActiveProjectsLink = function () { navigateTo("/Projects/ActiveProjectsList"); }
var ExtensionRequestLink = function (status) { navigateTo("/contractor/ContractorExtensionRequests?status=" + status); };
var AmmendedRequestLink = function (status) { navigateTo("/contractor/ContractorAmmendedRequests?status=" + status); };
var InstructionRequestLink = function (status) { navigateTo("/contractor/ContractorInstructionsRequests?status=" + status); };
var ApprovalRequestLink = function (status) { navigateTo("/contractor/ContractorApprovalRequests?status=" + status); };
var PaymentRequestLink = function (status) { navigateTo("/contractor/ContractorPaymentRequests?status=" + status); };
var HOSRequestLink = function (status) { navigateTo("/contractor/ContractorHOSRequests?status=" + status); };
var PMCommunicationLink = function (status) { navigateTo("/contractor/ProjectManagerCommunication?status=" + status); };
var ProjectMeetingsLink = function (status) { navigateTo("/contractor/ProjectMeetings?status=" + status); };


var PracticalRequestLink = function (status) { navigateTo("/contractor/PracticalCompletionCertification?status=" + status); };
var MakingGoodDefectsRequestLink = function (status) { navigateTo("/contractor/MakingGoodDefectRequests?status=" + status); };
var ProjectClosureLink = function () { navigateTo("/contractor/ProjectClosure"); };
var ClosedProjectsLink = function () { navigateTo("/contractor/ClosedProjects"); };