namespace MatrixTray.Common;

/// <summary>
/// <para>Class calculates the processing order for given tray matrix.</para>
/// <para>Processing order depends on which corner we want to start and to which direction to proceed.</para>
/// <para>the sorted list if IDs can be read from the ProcessingOrder after calculation.</para>
/// </summary>
public sealed class MatrixProcessingOrder
{
    private readonly MatrixId[] _processOrder;

    public MatrixProcessingOrder(MatrixData Data)
    {
        _ = Data ?? throw new ArgumentNullException(nameof(Data));
        _ = Data.IDS ?? throw new ArgumentNullException(nameof(Data));

        _processOrder = new MatrixId[Data.Definition.MatrixSize];
        Data.IDS.CopyTo(_processOrder, 0);
    }

    /// <summary>
    /// <para>Returns a 'normal' reference to the ID array.</para>
    /// </summary>
    /// <returns></returns>
    public MatrixId[] GetProcessingOrder() 
        => _processOrder;

    /// <summary>
    /// <para>Return stack-bound reference to the ID array.</para>
    /// </summary>
    /// <returns></returns>
    public ref readonly MatrixId[] GetProcessingOrderRef() 
        => ref _processOrder;

    /// <summary>
    /// <para>Calculate the Processing order based on start corner and direction.</para>
    /// </summary>
    /// <param name="Corner">
    /// <para>Start corner</para>
    /// </param>
    /// <param name="Dir">
    /// <para>Direction where to proceed on the tray.</para>
    /// </param>
    /// <returns>
    /// <para>Task which performs calculation async.</para>
    /// <para>after completion, the result is readable from the ProcessingOrder.</para>
    /// </returns>
    public async Task CalculateAsync(MatrixStartCorner Corner, MatrixDirection Dir)
    {
        await Task.Run(() => Calculate(Corner, Dir));
    }

    /// <summary>
    /// <para>Calculate the processing order based on start corner and direction.</para>
    /// </summary>
    /// <param name="Corner">
    /// <para>Start corner.</para>
    /// </param>
    /// <param name="Dir">
    /// <para>Direction where to proceed on the tray.</para>
    /// </param>
    public void Calculate(MatrixStartCorner Corner, MatrixDirection Dir)
    {
        Array.Sort(_processOrder, GetComparer(Corner, Dir));
    }

    private static IComparer<MatrixId> GetComparer(MatrixStartCorner c, MatrixDirection d) 
        => (c, d) switch
    {
        (MatrixStartCorner.UpperLeft, MatrixDirection.Xdirection)
            => new FromTopLeftRowsFirst(),
        (MatrixStartCorner.UpperLeft, MatrixDirection.YDirection)
            => new FromTopLeftColumnsFirst(),

        (MatrixStartCorner.UpperRight, MatrixDirection.Xdirection)
            => new FromTopRightRowsFirst(),
        (MatrixStartCorner.UpperRight, MatrixDirection.YDirection)
            => new FromTopRightColumnsFirst(),

        (MatrixStartCorner.LowerLeft, MatrixDirection.Xdirection)
            => new FromBottomLeftColumnsFirst(),
        (MatrixStartCorner.LowerLeft, MatrixDirection.YDirection)
            => new FromBottomLeftRowsFirst(),

        (MatrixStartCorner.LowerRight, MatrixDirection.Xdirection)
            => new FromBottomRightRowsFirst(),
        (MatrixStartCorner.LowerRight, MatrixDirection.YDirection)
            => new FromBottomRightColumnsFirst(),

        (_, _) => throw new InvalidOperationException()
    };
}

internal sealed class FromBottomRightColumnsFirst : IComparer<MatrixId>
{
    public int Compare(MatrixId a, MatrixId b)
    {
        int comparation;
        comparation = a.Xpos.CompareTo(b.Xpos);

        if (comparation < 0)
            return 1;

        if (comparation > 0)
            return -1;

        comparation = a.Ypos.CompareTo(b.Ypos);

        if (comparation < 0)
            return 1;

        if (comparation > 0)
            return -1;

        return 0;
    }
}
internal sealed class FromBottomRightRowsFirst : IComparer<MatrixId>
{
    public int Compare(MatrixId a, MatrixId b)
    {
        int comparation; 
        comparation = a.Ypos.CompareTo(b.Ypos);

        if(comparation < 0)
            return 1;

        if (comparation > 0)
            return -1;

        comparation = a.Xpos.CompareTo(b.Xpos);

        if (comparation < 0)
            return 1;

        if (comparation > 0)
            return -1;

        return 0;
    }
}

internal sealed class FromTopRightColumnsFirst : IComparer<MatrixId>
{
    public int Compare(MatrixId a, MatrixId b)
    {
        int comparation;
        comparation = a.Xpos.CompareTo(b.Xpos);

        if (comparation < 0)
            return 1;

        if (comparation > 0)
            return -1;

        comparation = a.Ypos.CompareTo(b.Ypos);

        if (comparation < 0)
            return -1;

        if (comparation > 0)
            return 1;

        return 0;
    }
}
internal sealed class FromTopRightRowsFirst : IComparer<MatrixId>
{
    public int Compare(MatrixId a, MatrixId b)
    {
        int comparation;
        comparation = a.Ypos.CompareTo(b.Ypos);

        if (comparation < 0)
            return 1;

        if (comparation > 0)
            return -1;

        comparation = a.Xpos.CompareTo(b.Xpos);

        if (comparation < 0)
            return -1;

        if (comparation > 0)
            return 1;

        return 0;
    }
}

internal sealed class FromTopLeftColumnsFirst : IComparer<MatrixId>
{
    public int Compare(MatrixId a, MatrixId b)
    {
        int comparation;
        comparation = a.Xpos.CompareTo(b.Xpos);

        if (comparation < 0)
            return -1;

        if (comparation > 0)
            return 1;

        comparation = a.Ypos.CompareTo(b.Ypos);

        if (comparation < 0)
            return -1;

        if (comparation > 0)
            return 1;

        return 0;
    }
}
internal sealed class FromTopLeftRowsFirst : IComparer<MatrixId>
{
    public int Compare(MatrixId a, MatrixId b)
    {
        int comparation;
        comparation = a.Ypos.CompareTo(b.Ypos);

        if (comparation < 0)
            return -1;

        if (comparation > 0)
            return 1;

        comparation = a.Xpos.CompareTo(b.Xpos);

        if (comparation < 0)
            return -1;

        if (comparation > 0)
            return 1;

        return 0;
    }
}

internal sealed class FromBottomLeftColumnsFirst : IComparer<MatrixId>
{
    public int Compare(MatrixId a, MatrixId b)
    {
        int comparation;
        comparation = a.Xpos.CompareTo(b.Xpos);

        if (comparation < 0)
            return -1;

        if (comparation > 0)
            return 1;

        comparation = a.Ypos.CompareTo(b.Ypos);

        if (comparation < 0)
            return 1;

        if (comparation > 0)
            return -1;

        return 0;
    }
}
internal sealed class FromBottomLeftRowsFirst : IComparer<MatrixId>
{
    public int Compare(MatrixId a, MatrixId b)
    {
        int comparation;
        comparation = a.Ypos.CompareTo(b.Ypos);

        if (comparation < 0)
            return -1;

        if (comparation > 0)
            return 1;

        comparation = a.Xpos.CompareTo(b.Xpos);

        if (comparation < 0)
            return 1;

        if (comparation > 0)
            return -1;

        return 0;
    }
}
