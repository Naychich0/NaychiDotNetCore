// See https://aka.ms/new-console-template for more information
using NaychiDotNetCore.ConsoleApp.AdoDotNetCoreExamples;
using NaychiDotNetCore.ConsoleApp.DapperExamples;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello,World");

//Ctrl + . => suggestion
//F9 => break point
//F10 => summary
//F11 => detail

//AdoDotNetCoreExample adoDotNetExample = new AdoDotNetCoreExample();
//adoDotNetExample.Run();

DapperExample dapperExample = new DapperExample();
dapperExample.Run();

Console.ReadKey();