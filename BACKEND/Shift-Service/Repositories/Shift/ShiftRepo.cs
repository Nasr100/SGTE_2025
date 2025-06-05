using Mapster;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shift_Service.Data;

namespace Shift_Service.Repositories.Shift
{
    public class ShiftRepo : IShiftRepo
    {
        private readonly ShiftServiceContext _context;
        public ShiftRepo(ShiftServiceContext context)
        { 
            this._context = context;
        }

        public async Task<ShiftResponse> AddShift(ShiftRequest shift)
        {
                var shiftModel = shift.Adapt<Models.Shift>();
                var x = await _context.Shifts.AddAsync(shiftModel);
                await _context.SaveChangesAsync();
                return shiftModel.Adapt<ShiftResponse>();
        }

        public IQueryable<Models.Shift> GetAllShifts()
        {
            var shifts = _context.Shifts.Where(r => !r.IsDeleted);
            return shifts;
        }
        public async Task<Models.Shift> GetShiftById(int id)
        {
            var shift = await _context.Shifts.Where(r => !r.IsDeleted).FirstOrDefaultAsync(r => r.Id == id) ?? throw new Exception("shift with id " + id + "not found");
            return shift;
        }
        public async Task DeleteShifte(int id)
        {
            var shift = await GetShiftById(id);
            shift.IsDeleted = true;
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
        }
        public async Task<ShiftResponse> Updateshift(int id, ShiftRequest shiftReq)
        {
            var shift = await GetShiftById(id);
            shiftReq.Adapt(shift);
            await _context.SaveChangesAsync();
            return shift.Adapt<ShiftResponse>();
        }
    }
}
