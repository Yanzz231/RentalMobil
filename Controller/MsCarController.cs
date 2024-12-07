using Database.Data;
using Database.Models;
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
            [FromQuery] int pageSize = 8,
            [FromQuery] int? sortBy = 0,
            [FromQuery] string? PickDate = null,
            [FromQuery] string? ReturnDate = null,
            [FromQuery] string? Year = null
        )
        {
            try
            {
                if (id.HasValue)
                {
                    var view = await _context.MsCar.FirstOrDefaultAsync(p => p.Car_id == id);
                    if (view == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    return Ok(new { message = "Menampilkan Data", data = view });
                }
                else if (PickDate != null || ReturnDate != null || Year != null)
                {
                    var allData = _context.MsCar.AsQueryable();

                    if (Year != null && int.TryParse(Year, out int yearValue))
                    {
                        allData = allData.Where(car => car.Year == yearValue);
                    }

                    if (PickDate != null)
                    {
                        allData =
                            from car in allData
                            join rental in _context.TrRental on car.Car_id equals rental.Car_id
                            where rental.Rental_Date == PickDate
                            select car;
                    }

                    if (ReturnDate != null)
                    {
                        allData =
                            from car in allData
                            join rental in _context.TrRental on car.Car_id equals rental.Car_id
                            where rental.Return_Date == ReturnDate
                            select car;
                    }


                    var totalItems = await allData.CountAsync();
                    var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                    var paginatedData = await allData
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                    return Ok(
                        new
                        {
                            message = "Menampilkan Data berdasarkan Filter",
                            data = paginatedData,
                            totalItems = totalItems,
                            totalPages = totalPages,
                            currentPage = page,
                            pageSize = pageSize,
                        }
                    );
                }
                else
                {
                    var allData = _context.MsCar.AsQueryable();

                    // SORTING
                    if (sortBy == 1)
                    {
                        allData = allData.OrderByDescending(car => car.Price_per_day);
                    }
                    else if (sortBy == 2)
                    {
                        allData = allData.OrderBy(car => car.Price_per_day);
                    }

                    if (page == 0)
                    {
                        var fullData = await allData.ToListAsync();
                        return Ok(
                            new
                            {
                                message = "Menampilkan Semua Data",
                                data = fullData,
                                totalItems = fullData.Count,
                            }
                        );
                    }
                    else
                    {
                        // NGHITUNG PAGE
                        var totalItems = await allData.CountAsync();
                        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                        var paginatedData = await allData
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

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
                    var delete = await _context.MsCar.FirstOrDefaultAsync(p => p.Car_id == id);
                    if (delete == null)
                    {
                        return NotFound(new { message = $"Data {id} Tidak ada" });
                    }

                    _context.MsCar.Remove(delete);
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
