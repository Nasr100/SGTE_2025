using Gridify;
using Mapster;
using Shared.Dtos;
using Shift_Service.Repositories.Shift;

namespace Shift_Service.Services.Shift
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepo _shiftrepo;
        public ShiftService(IShiftRepo _shiftrepo)
        {
            this._shiftrepo = _shiftrepo;
        }
        public async Task<ShiftResponse> AddShift(ShiftRequest shift)
        {
            var shiftResponse = await this._shiftrepo.AddShift(shift);
            return shiftResponse;
        }

        public Paging<ShiftResponse> GetAllShifts(GridifyQuery grifigy)
        {
            var shifts = _shiftrepo.GetAllShifts().Gridify(grifigy);
            return shifts.Adapt<Paging<ShiftResponse>>();
        }

        public async Task<ShiftResponse> GetShifteById(int id)
        {
            var shift = await _shiftrepo.GetShiftById(id);
            return shift.Adapt<ShiftResponse>();
        }

        public async Task DeleteShift(int id)
        {
            await _shiftrepo.DeleteShifte(id);
        }

        public async Task<ShiftResponse> UpdateShift(int id, ShiftRequest shiftRequest)
        {
            var shift = await _shiftrepo.Updateshift(id, shiftRequest);
            return shift;
        }



    }
}
