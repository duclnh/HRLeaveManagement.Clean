using Blazored.Toast.Services;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Edit
    {
        [Inject]
        ILeaveTypeService _client { get; set; }

        [Inject]
        NavigationManager _navManager { get; set; }

        [Parameter]
        public int id { get; set; }
        public string Message { get; private set; }

        LeaveTypeVM leaveType = new LeaveTypeVM();

        [Inject]
        IToastService _toastService { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            leaveType = await _client.GetLeaveTypeDetails(id);
        }

        async Task EditLeaveType()
        {
            var response = await _client.UpdateLeaveType(id, leaveType);
            if (response.Success)
            {
                _toastService.ShowSuccess("Update leave type successfully");
                _navManager.NavigateTo("/leavetypes/");
            }
            else
            {
                _toastService.ShowError("Update leave type fail");
                Message = response.Message;
            }
        }
    }
}