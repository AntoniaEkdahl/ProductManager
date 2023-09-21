using ProductManager.Domain;
using ProductManager.Data;
using static System.Console;

namespace ProductManager;

class Program
{
  public static void Main()
  {
    Title = "Product Manager";
    CursorVisible = false;

    while (true)
    {
      WriteLine("1. Ny produkt");

      var keyPressed = ReadKey(true);

      Clear();

      switch (keyPressed.Key)
      {
        case ConsoleKey.D1:
        case ConsoleKey.NumPad1:

          AddProductView();

          break;
      }

      Clear();
    }
  }
  private static void AddProductView()
  {
    var name = GetUserInput("Namn");
    var sku = GetUserInput("SKU");
    var description = GetUserInput("Beskrivning");
    var image = GetUserInput("Bild (URL)");
    var price = GetUserInput("Pris");

    var product = new Product
    {
      Name = name,
      Sku = sku,
      Description = description,
      Image = image,
      Price = price
    };

    WriteLine();

    WriteLine("Är detta korrekt? (J)a (N)ej");

    while (true)
    {
      var keyPressed = ReadKey(true);

      Clear();

      switch (keyPressed.Key)
      {
        case ConsoleKey.J:

          SaveProduct(product);

          WriteLine("Produkt sparad");

          Thread.Sleep(2000);

          return;

        case ConsoleKey.N:

          AddProductView();

          return;
      }
    }
  }

  private static void SaveProduct(Product product)
  {
    using var context = new ApplicationDbContext();

    context.Product.Add(product);

    context.SaveChanges();
  }

  private static string GetUserInput(string label)
  {
    Write($"{label}: ");

    return ReadLine() ?? "";
  }
}
