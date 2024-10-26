using Database.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MsCustomers.Controllers
{
    [Route("api/mscustomer")]
    [ApiController]
    public class MsCustomers : ControllerBase
    {
        private readonly AppDbContext _context;

        public MsCustomers(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Database.Models.MsCustomer request)
        {
            try
            {
                // KALO NULL BERARTI YA KGK ADA
                if (
                    request.Name == ""
                    || request.Phone_number == ""
                    || request.Address == ""
                    || request.Driver_license_number == ""
                    || request.Email == ""
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
        public async Task<IActionResult> GetAll(
            [FromQuery] int? id,
            string? username,
            string? password
        )
        {
            try
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    var view = await _context.MsCustomer.FirstOrDefaultAsync(p =>
                        p.Name == username && p.Password == password
                    );

                    if (view == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    return Ok(new { message = "Menampilkan Data", data = view });
                }
                else if (id.HasValue)
                {
                    var view = await _context.MsCustomer.FirstOrDefaultAsync(p =>
                        p.Customer_id == id
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
                    var viewall = await _context.MsCustomer.ToListAsync();
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
                    var delete = await _context.MsCustomer.FirstOrDefaultAsync(p =>
                        p.Customer_id == id
                    );
                    if (delete == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    _context.MsCustomer.Remove(delete);
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
