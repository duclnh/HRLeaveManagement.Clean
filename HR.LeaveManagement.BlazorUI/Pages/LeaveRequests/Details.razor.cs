using Blazored.Toast.Services;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Details
    {
        [Inject] public required ILeaveRequestService leaveRequestService { get; set; }
        [Inject]public required NavigationManager navigationManager { get; set; }
        [Inject] public required IToastService toastService { get; set; }
        [Parameter] public int id { get; set; }

        string? ClassName;
        string? HeadingText;

        public LeaveRequestVM Model { get; private set; } = new LeaveRequestVM();

        protected override async Task OnParametersSetAsync()
        {
                Model = await leaveRequestService.GetLeaveRequest(id);
        }

        protected override async Task OnInitializedAsync()
        {
            if (Model.Approved == null)
            {
                ClassName = "warning";
                HeadingText = "Pending Approval";
            }
            else if (Model.Approved == true)
            {
                ClassName = "success";
                HeadingText = "Approved";
            }
            else
            {
                ClassName = "danger";
                HeadingText = "Rejected";
            }
        }

        async Task ChangeApproval(bool approvalStatus)
        {
           var response = await leaveRequestService.ApproveLeaveRequest(id, approvalStatus);
            if (response.Success)
            {
                toastService.ShowSuccess("Approval leave request successfully");
                navigationManager.NavigateTo("/leaverequests/");
            }
            else
            {
                toastService.ShowError("Approval leave request fail");
            }
        }
    }
}