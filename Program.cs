// 8===========D
// (.)(.)

// Menú
// Añadir nuevo gasto
//Descripción, categoría, importe en €, guardar en archivo gastos.csv
// Consultar gastos
//Leer gastos.csv
// Balance total
//Procesar gastos.csv
// Salir

// Formato archivo ".csv"
// try/catch
// Validar entrada

using System.Linq.Expressions;
using System.Xml.Serialization;

string csvPath = "gastos.csv";

Menu(csvPath);

static void Menu(string archivo)
{
    bool salir = false;
    while (!salir)
    {
        Console.WriteLine("Elige una opción: ");
        Console.WriteLine("Menú de gastos");
        Console.WriteLine("1. Añadir nuevo gasto");
        Console.WriteLine("2. Consultar gastos");
        Console.WriteLine("3. Balance total");
        Console.WriteLine("4. Salir");
        int choice;
        bool isNumber = int.TryParse(Console.ReadLine(), out choice);

        if (isNumber && choice <= 4)
        {
            Console.WriteLine($"Has elegido la opción: {choice}");
            switch (choice)
            {
                case 1:
                    AddGasto(archivo);
                    break;
                case 2:
                    CheckGasto(archivo);
                    break;
                case 3:
                    // TODO: Balance total
                    break;
                case 4:
                    Console.WriteLine("Saliendo...");
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

        }
        else
        {
            Console.WriteLine("Elige una opción válida.");
        }
    }

}

static void AddGasto(string archivo)
{
    //Descripción
    //Categoría
    //Importe en euros
    //Guardar el registro en archivo de texto.
try
    {
        string readText = File.ReadAllText(archivo);
    }
catch (FileNotFoundException)
    {
        Console.WriteLine("Archivo no encontrado. Creando...");
        File.WriteAllText(archivo, "");
    }
    DateTime now = DateTime.Now;

    Console.WriteLine("Introduce la descripción del gasto: ");
    string gastoDesc = Console.ReadLine() ?? string.Empty;

    Console.WriteLine("Introduce la categoría del gasto: ");
    string categoria = Console.ReadLine() ?? string.Empty;

    Console.WriteLine("Introduce el importe en euros (€): ");
    string importe = Console.ReadLine() ?? string.Empty;

    string data = $"{now}|{gastoDesc}|{categoria}|{importe}\n";

    File.AppendAllText(archivo, data);

}

static void CheckGasto(string archivo)
{
    string readText;

    try
    {
        readText = File.ReadAllText(archivo);
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("No se encontró ningún archivo. Empieza añadiendo un nuevo gasto.");
        return;
    }

    Console.WriteLine("Tus gastos son los siguientes: ");
    var csvalues = new List<string>(readText.Split("\n"));
    foreach (var value in csvalues)
    {
        var newValue = value.Replace("|", " ");
        Console.WriteLine(newValue);
    }

}