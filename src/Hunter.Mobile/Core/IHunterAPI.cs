using Hunter.Mobile.Models;
using System.Threading.Tasks;

namespace Hunter.Mobile.Core
{
    interface IHunterAPI
    {
        Task<ResponseModel<Tasks[]>> GetDeviceTasks();
        Task<ResponseModel<User>> Authenticate(LoginModel loginModel);
        Task PostTasksData(TaskData taskData);

    }
}