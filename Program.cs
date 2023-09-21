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
      WriteLine("2. Sök produkt");
      WriteLine("3. Avsluta");


      var keyPressed = ReadKey(true);

      Clear();

      switch (keyPressed.Key)
      {
        case ConsoleKey.D1:
        case ConsoleKey.NumPad1:

          AddProductView();

          break;

        case ConsoleKey.D2:
        case ConsoleKey.NumPad2:

          SearchProductView();

          break;
        case ConsoleKey.D3:
        case ConsoleKey.NumPad3:

          Environment.Exit(0);

          return;
      }

      Clear();
    }
  }

  private static void SearchProductView()
  {
    string sku = GetUserInput("SKU");

    Clear();

    var product = GetProductBySKU(sku);

    if (product is not null)
    {
      DisplayProductDetails(product);

      WriteLine("(R)adera");

      while (true)
      {
        var keyPressed = ReadKey(true);

        Clear();

        switch (keyPressed.Key)
        {
          case ConsoleKey.Escape:

            return;

          case ConsoleKey.R:

            DeleteProduct(product);

            return;
        }
      }
    }
    else
    {
      WriteLine("Produkt finns ej");

      Thread.Sleep(2000);
    }
  }

  private static void DisplayProductDetails(Product product)
  {
    WriteLine($"Namn: {product.Name}");
    WriteLine($"SKU: {product.Sku}");
    WriteLine($"Beskrivning: {product.Description}");
    WriteLine($"Bild (URL): {product.Image}");
    WriteLine($"Pris: {product.Price}");
    WriteLine();
  }

  private static void DeleteProduct(Product product)
  {
    using var context = new ApplicationDbContext();

    DisplayProductDetails(product);

    WriteLine("Radera produkt? (J)a (N)ej");

    while (true)
    {
      var keyPressed = ReadKey(true);

      Clear();

      switch (keyPressed.Key)
      {
        case ConsoleKey.J:

          context.Product.Remove(product);

          context.SaveChanges();

          WriteLine("Produkt raderad");

          Thread.Sleep(2000);

          return;

        case ConsoleKey.N:

          DisplayProductDetails(product);

          WriteLine("(R)adera");

          break;
      }
      switch (keyPressed.Key)
      {
        case ConsoleKey.R:

          DeleteProduct(product);

          return;

        case ConsoleKey.Escape:

          return;
      }
    }
  }

  private static Product? GetProductBySKU(string sku)
  {
    using var context = new ApplicationDbContext();

    var product = context.Product.FirstOrDefault(x => x.Sku == sku);

    return product;
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

          try
          {
            SaveProduct(product);

            WriteLine("Produkt sparad");
          }
          catch
          {
            WriteLine("Produkt med samma SKU finns redan registrerat");
          }

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
