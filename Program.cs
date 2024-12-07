var builder = WebApplication.CreateBuilder(args);

// Tambahkan Razor Pages
builder.Services.AddRazorPages();

// Konfigurasi CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin() 
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Konfigurasi untuk lingkungan non-development
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Gunakan middleware CORS
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
