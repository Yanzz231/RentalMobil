using Microsoft.AspNetCore.Mvc.RazorPages;

public class CarModel : PageModel
{
    public int CarId { get; set; }

    public void OnGet(int id)
    {
        CarId = id;
    }
}
