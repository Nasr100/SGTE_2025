using Gridify;
using Shared.Dtos;

namespace Shift_Service.Services.Shift
{
    public interface IShiftService
    {
        public Task<ShiftResponse> AddShift(ShiftRequest shift);
        public Paging<ShiftResponse> GetAllShifts(GridifyQuery grifigy);
        public Task<ShiftResponse> GetShifteById(int id);
        public  Task DeleteShift(int id);
        public Task<ShiftResponse> UpdateShift(int id, ShiftRequest shiftRequest);

    }
}
