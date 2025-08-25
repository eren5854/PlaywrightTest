
// Playwright başlat
using Microsoft.Playwright;

using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
{
    Headless = false,
    Channel = "chrome" // Chrome gibi davranır
});


var context = await browser.NewContextAsync(new BrowserNewContextOptions
{
    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                "AppleWebKit/537.36 (KHTML, like Gecko) " +
                "Chrome/119.0.0.0 Safari/537.36"
});
var page = await context.NewPageAsync();


string url = "https://www.momox.de/offer/";
//string ean = "9783895811562";
while (true)
{
    Console.WriteLine("Lütfen barkod kodunu giriniz: ");
    string ean = Console.ReadLine();

    if (string.IsNullOrEmpty(ean))
    {
        Console.WriteLine("Barkod kodu boş bırakılamaz.");
        return;
    }

    // İlgili ürüne git
    await page.GotoAsync($"{url}{ean}");
    // (Burada EAN ile ürün sayfasını açıyoruz)

    await page.WaitForSelectorAsync("h1");
    var productName = await page.InnerTextAsync("h1");

    await page.WaitForSelectorAsync(".searchresult-price");
    var price = await page.InnerTextAsync(".searchresult-price");

    Console.WriteLine($"Ürün Adı: {productName}");
    Console.WriteLine($"Fiyat: {price}");

}