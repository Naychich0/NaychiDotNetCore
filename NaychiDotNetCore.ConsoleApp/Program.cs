﻿// See https://aka.ms/new-console-template for more information
using NaychiDotNetCore.ConsoleApp.AdoDotNetCoreExamples;
using NaychiDotNetCore.ConsoleApp.DapperExamples;
using NaychiDotNetCore.ConsoleApp.EFCoreExamples;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello,NCC");

//Ctrl + . => suggestion
//F9 => break point
//F10 => summary
//F11 => detail

//AdoDotNetCoreExample adoDotNetExample = new AdoDotNetCoreExample();
//adoDotNetExample.Run();
//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Run();

Console.ReadKey();