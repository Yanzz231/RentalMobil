using Database.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MsCarsImages.Controllers
{
    [Route("api/mscarimages")]
    [ApiController]
    public class MsCarsImages : ControllerBase
    {
        private readonly AppDbContext _context;

        public MsCarsImages(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Database.Models.MsCarImages request)
        {
            try
            {
                // KALO NULL BERARTI YA KGK ADA
                if (request.Image_link == "")
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
        public async Task<IActionResult> GetAll([FromQuery] int? id, int? car_id)
        {
            try
            {
                if (id.HasValue)
                {
                    var view = await _context.MsCarImages.FirstOrDefaultAsync(p =>
                        p.Image_Car_Id == id
                    );
                    if (view == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    return Ok(new { message = "Menampilkan Data", data = view });
                }
                else if (car_id.HasValue)
                {
                    var views = await _context
                        .MsCarImages.Where(p => p.Car_id == car_id)
                        .ToListAsync(); 

                    if (views == null || !views.Any())
                    {
                        return NotFound(new { message = $"Data untuk car_id {car_id} tidak ada" });
                    }

                    return Ok(new { message = "Menampilkan Data", data = views });
                }
                // KALO ELSE INI BERARPI LIAT FULL DATA
                else
                {
                    var viewall = await _context.MsCarImages.ToListAsync();
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
        public async Task<IActionResult> Delete([FromQuery] int? id, int? car_id)
        {
            try
            {
                if (id.HasValue)
                {
                    var delete = await _context.MsCarImages.FirstOrDefaultAsync(p =>
                        p.Image_Car_Id == id
                    );
                    if (delete == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    _context.MsCarImages.Remove(delete);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Data berhasil dihapus.", data = delete });
                }
                else if (car_id.HasValue)
                {
                    var delete = await _context.MsCarImages.FirstOrDefaultAsync(p =>
                        p.Car_id == car_id
                    );
                    if (delete == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    _context.MsCarImages.Remove(delete);
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
