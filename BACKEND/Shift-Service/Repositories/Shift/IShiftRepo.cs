using Shared.Dtos;

namespace Shift_Service.Repositories.Shift
{
    public interface IShiftRepo
    {
        public Task<ShiftResponse> AddShift(ShiftRequest shift);
        public IQueryable<Models.Shift> GetAllShifts();
        public Task DeleteShifte(int id);
        public Task<ShiftResponse> Updateshift(int id, ShiftRequest shiftReq);
        public Task<Models.Shift> GetShiftById(int id);

    }
}
