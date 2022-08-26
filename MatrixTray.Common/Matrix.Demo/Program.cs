// See https://aka.ms/new-console-template for more information
using MatrixTray.Common;

System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();   
sw.Reset();
sw.Start();
var data = new MatrixData(new MatrixDefinition(10, 10));
var proc = new MatrixDataProcessor(data);
sw.Stop();
var tiks = sw.ElapsedTicks;

MatrixCoord start = new(10, 10);
MatrixCoord offset = new(2, 3);
proc.InitializeMatrixCoordinates(start, offset);

proc.AdjustXOffsetForColumn(3, 0.5f);

var dir = new MatrixProcessingOrder(data);

dir.Calculate(MatrixStartCorner.LowerLeft, MatrixDirection.YDirection);
var ans = dir.GetProcessingOrder();

dir.Calculate(MatrixStartCorner.UpperRight, MatrixDirection.YDirection);
ans = dir.GetProcessingOrder();

Console.ReadKey();
