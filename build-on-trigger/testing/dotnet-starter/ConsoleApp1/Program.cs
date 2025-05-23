// See https://aka.ms/new-console-template for more information

using SqlOnAir.DotNet.Lib.DataClasses;

Console.WriteLine("Hello, World!");

var cell1 = new Cell()
{
    CellId = $"{Guid.NewGuid()}"
};

var cell2 = new Cell()
{
    CellId = $"{Guid.NewGuid()}"
};

Console.WriteLine($"Cell1 ID: {cell1.CellId}");
Console.WriteLine($"Cell2 ID: {cell2.CellId}");