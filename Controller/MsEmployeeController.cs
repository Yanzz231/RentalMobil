using Database.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MsEmployees.Controllers
{
    [Route("api/msemployee")]
    [ApiController]
    public class MsEmployees : ControllerBase
    {
        private readonly AppDbContext _context;

        public MsEmployees(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Database.Models.MsEmployee request)
        {
            try
            {
                // KALO NULL BERARTI YA KGK ADA
                if (
                    request.Name == ""
                    || request.Position == ""
                    || request.Email == ""
                    || request.Phone_number == ""
                )
                {
                    return BadRequest(new { message = "Ada Value yang kosong" });
                }

                // MENAMBANG DATA KE TABLE
                _context.Add(request);

                // SAVING DATA KE TABLE
                await _context.SaveChangesAsync();

                return Ok(new { message = "Sukses Menambah Data", data = request });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message}" });
            }
        }

        [HttpGet("view")]
        // FROMQUERY ITU BUAT LIAT VALUE DARI QUERY
        public async Task<IActionResult> GetAll([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var view = await _context.MsEmployee.FirstOrDefaultAsync(p =>
                        p.Employee_id == id
                    );
                    if (view == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    return Ok(new { message = "Menampilkan Data", data = view });
                }
                // KALO ELSE INI BERARPI LIAT FULL DATA
                else
                {
                    var viewall = await _context.MsEmployee.ToListAsync();
                    if (viewall == null || !viewall.Any())
                    {
                        return NotFound(new { message = "Data tidak ada" });
                    }

                    return Ok(new { message = "Menampilkan Semua Data", data = viewall });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message}" });
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var delete = await _context.MsEmployee.FirstOrDefaultAsync(p =>
                        p.Employee_id == id
                    );
                    if (delete == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    _context.MsEmployee.Remove(delete);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Data berhasil dihapus.", data = delete });
                }
                else
                {
                    return Ok(new { message = "Tidak ada parameter" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message}" });
            }
        }
    }
}
