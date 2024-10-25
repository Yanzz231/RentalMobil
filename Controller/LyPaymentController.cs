using Database.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LtPayment.Controllers
{
    [Route("api/ltpayment")]
    [ApiController]
    public class LtPaymentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LtPaymentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Database.Models.LtPayments request)
        {
            try
            {
                // KALO NULL BERARTI YA KGK ADA
                if (request.Payment_method == "")
                {
                    return BadRequest(new { message = "Value dari Payment method tidak ada" });
                }

                // MENAMBANG DATA KE TABLE
                request.Payment_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // BIKIN SI PAYMENT_DATE MENJADI WAKTU SEKARANG
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
        public async Task<IActionResult> GetAll([FromQuery] int? rental_id, int? id)
        {
            try
            {
                // NGLIAT DATA PAKE RENTAL ID || JADI QUERYNYA PAKE RENTAL ID ( FIND BY RENTAL ID )
                if (rental_id.HasValue)
                {
                    var view = await _context.LtPayment.FirstOrDefaultAsync(p =>
                        p.Rental_id == rental_id
                    );
                    if (view == null)
                    {
                        return NotFound(new { message = $"Data {rental_id} Tidak ada" });
                    }

                    return Ok(new { message = "Menampilkan Data", data = view });
                }
                // NGLIAT DATA PAKE PAYMENT ID || GW PAKE ID BUAT CHECK PAYMENT_ID
                else if (id.HasValue)
                {
                    var view = await _context.LtPayment.FirstOrDefaultAsync(p =>
                        p.Payment_id == id
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
                    var viewall = await _context.LtPayment.ToListAsync();
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
        public async Task<IActionResult> Delete([FromQuery] int? id, int? rental_id)
        {
            try
            {
                if (id.HasValue)
                {
                    var delete = await _context.LtPayment.FirstOrDefaultAsync(p =>
                        p.Payment_id == id
                    );
                    if (delete == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    _context.LtPayment.Remove(delete);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Data berhasil dihapus.", data = delete });
                }
                else if (rental_id.HasValue)
                {
                    var delete = await _context.LtPayment.FirstOrDefaultAsync(p =>
                        p.Payment_id == id
                    );
                    if (delete == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    _context.LtPayment.Remove(delete);
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
