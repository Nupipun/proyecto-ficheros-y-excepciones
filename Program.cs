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

//TODO: Cambiar listas a arrays con los siguientes nombres
//TODO: Parsear el .csv como un array BIDIMENSIONAL en vez de como una lista.
//{
//  FECHA_HORA: (fecha)
//  CATEGORIA: (cat)
//  DESCRIPCION: (desc)
//  IMPORTE: (imp)
//}

using System.Collections;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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
                    BalanceTotal(archivo);
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

static string[,] LeerCsv(string archivo)
{
    string readText;
    int filas = 4;

    readText = File.ReadAllText(archivo);
 
    Console.WriteLine("Tus gastos son los siguientes: ");
    //var csvalues = new List<string>(readText.Split("\n"));
    List<string> csValues = new List<string>(readText.Split("\n"));
    csValues.Remove("");
    List<string> formattedCsValues = new List<string>();
    string[,] csValuesArray = new string[csValues.Count(), filas];
    // Iteramos sobre cada entrada del .csv
    for (int i = 0; i < csValues.Count; i++)
    {
        string value = csValues[i];
        formattedCsValues.AddRange(value.Split("|"));
        for (int v = 0; v < 4; v++)
        {
            csValuesArray[i, v] = formattedCsValues[0];
            formattedCsValues.RemoveAt(0);
        }
    }
    return csValuesArray;


}

static void CheckGasto(string archivo)
{

    string[,] valor;

    try
    {
        valor = LeerCsv(archivo);
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("No se encontró un archivo csv. Añade un gasto primero.");
        return;
    }

    for (int f = 0; f < valor.GetLength(0); f++)
    {
        for (int c = 0; c < valor.GetLength(1); c++)
        {
            switch (c)
            {
                case 0:
                    Console.Write($"| Fecha: {valor[f, c]} ");
                    break;
                case 1:
                    Console.Write($"| Descripción: {valor[f, c]} ");
                    break;
                case 2:
                    Console.Write($"| Categoría: {valor[f, c]} ");
                    break;
                case 3:
                    Console.Write($"| Importe: {valor[f, c]} ");
                    break;
            }


        }
        Console.WriteLine();
    }
    //Console.WriteLine($"Fecha: {valor[0, 0]} Descripción: {valor[0, 1]}, Categoría: {valor[0, 2]}, Importe: {valor[0, 3]}");

}

static void BalanceTotal(string archivo)
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
    var csvalues = new List<string>(readText.Split("\n"));
    var eachvalues = new List<string>();
    foreach (var value in csvalues)
    {
       // eachvalues.Append(value.Split("|"));
    }
    Console.WriteLine(eachvalues);

    
}