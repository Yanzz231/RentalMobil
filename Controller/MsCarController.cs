using Database.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MsCars.Controllers
{
    [Route("api/mscar")]
    [ApiController]
    public class MsCars : ControllerBase
    {
        private readonly AppDbContext _context;

        public MsCars(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Database.Models.MsCar request)
        {
            try
            {
                // KALO NULL BERARTI YA KGK ADA
                if (
                    request.Name == ""
                    || request.Model == ""
                    || request.License_Plate == ""
                    || request.Transmission == ""
                    || request.Status == ""
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
        public async Task<IActionResult> GetAll(
            [FromQuery] int? id,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 8
        )
        {
            try
            {
                if (id.HasValue)
                {
                    var view = await _context.Mscar.FirstOrDefaultAsync(p => p.Car_id == id);
                    if (view == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    return Ok(new { message = "Menampilkan Data", data = view });
                }
                else
                {
                    // Retrieve all records and apply pagination
                    var allData = await _context.Mscar.ToListAsync();
                    if (allData == null || !allData.Any())
                    {
                        return NotFound(new { message = "Data tidak ada" });
                    }

                    // Calculate total items and pages
                    var totalItems = allData.Count;
                    var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                    // Get paginated items
                    var paginatedData = allData.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    return Ok(
                        new
                        {
                            message = "Menampilkan Semua Data",
                            data = paginatedData,
                            totalItems = totalItems,
                            totalPages = totalPages,
                            currentPage = page,
                            pageSize = pageSize,
                        }
                    );
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
                    var delete = await _context.Mscar.FirstOrDefaultAsync(p => p.Car_id == id);
                    if (delete == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    _context.Mscar.Remove(delete);
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
