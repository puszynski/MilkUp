using MilkUp.Repositories;
using MilkUp.Repositories.Interfaces;
using MilkUp.ViewModels.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.Services
{
    public class CowDeleteService
    {
        readonly ICowRepository _cowRepository;
        readonly ILactationRepository _lactationRepository;

        public CowDeleteService(ICowRepository cowRepository,
                                ILactationRepository lactationRepository)
        {
            _cowRepository = cowRepository;
            _lactationRepository = lactationRepository;
        }

        public async Task<NotificationViewModel> Delete(int cowID)
        {
            var lactations = await _lactationRepository.GetCowLactations(cowID);

            if (lactations.Any())
                foreach (var item in lactations)
                    _lactationRepository.Delete(item);

            await _cowRepository.Delete(cowID);

            return new NotificationViewModel()
            {
                Message = "Usunięto krowę",
                NotificationType = Enums.ENotificationType.Success
            };
        }
    }
}
