// See https://aka.ms/new-console-template for more information
using MatrixTray.Common;

System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();   
sw.Reset();
sw.Start();
var data = new TrayData(new TrayDefinition(6, 8));
var proc = new TrayDataProcessor(data);
sw.Stop();
var tiks = sw.ElapsedTicks;

TrayCoord start = new(10, 10);
TrayCoord offset = new(2, 3);
proc.InitializeMatrixCoordinates(start, offset);

proc.AdjustXOffsetForColumn(3, 0.5f);

var dir = new TrayProcessingOrder(data);

sw.Reset();
sw.Start();
dir.Calculate(TrayStartCorner.LowerLeft, TrayDirection.Y);
var ans = dir.GetProcessingOrder();
sw.Stop();
tiks = sw.ElapsedTicks;

dir.Calculate(TrayStartCorner.UpperRight, TrayDirection.Y);
ans = dir.GetProcessingOrder();

Console.ReadKey();
