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
    if (string.IsNullOrEmpty(categoria) || string.IsNullOrEmpty(gastoDesc))
    {
        Console.WriteLine($"Hay algunos campos en blanco. Intentalo de nuevo.");
        return;
    }

    Console.WriteLine("Introduce el importe en euros (€): ");
    bool isNumber = int.TryParse(Console.ReadLine(), out int importe);
    if (!isNumber)
    {
        Console.WriteLine("Introduce un número válido.");
        return;
    }

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
    string[,] csv;
    int cImporte = 3;
    int resultado = 0;


    try
    {
        csv = LeerCsv(archivo);
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("No se encontró un archivo csv. Añade un gasto primero.");
        return;
    }
    for (int f = 0; f < csv.GetLength(0); f++)
    {
        int importeObtenido = int.Parse(csv[f, cImporte]);
        resultado += importeObtenido;
    }
    Console.WriteLine($"Tu balance total es de: {resultado}");
    
}