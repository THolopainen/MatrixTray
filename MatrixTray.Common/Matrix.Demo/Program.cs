// See https://aka.ms/new-console-template for more information
using MatrixTray.Common;

System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();   
sw.Reset();
sw.Start();
var data = new TrayData(new TrayDefinition(10, 10));
var proc = new TrayDataProcessor(data);
sw.Stop();
var tiks = sw.ElapsedTicks;

TrayCoord start = new(10, 10);
TrayCoord offset = new(2, 3);
proc.InitializeMatrixCoordinates(start, offset);

proc.AdjustXOffsetForColumn(3, 0.5f);

var dir = new TrayProcessingOrder(data);

dir.Calculate(TrayStartCorner.LowerLeft, TrayDirection.YDirection);
var ans = dir.GetProcessingOrder();

dir.Calculate(TrayStartCorner.UpperRight, TrayDirection.YDirection);
ans = dir.GetProcessingOrder();

Console.ReadKey();
