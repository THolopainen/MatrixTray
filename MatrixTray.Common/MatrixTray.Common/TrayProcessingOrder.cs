namespace MatrixTray.Common;

/// <summary>
/// <para>Class calculates the processing order for given tray matrix.</para>
/// <para>Processing order depends on which corner we want to start and to which direction to proceed.</para>
/// <para>the sorted list if IDs can be read from the ProcessingOrder after calculation.</para>
/// </summary>
public sealed class TrayProcessingOrder
{
    private readonly TrayID[] _processOrder;

    public TrayProcessingOrder(TrayData Data)
    {
        _ = Data ?? throw new ArgumentNullException(nameof(Data));
        _ = Data.IDS ?? throw new ArgumentNullException(nameof(Data));

        _processOrder = new TrayID[Data.Definition.MatrixSize];
        Data.IDS.CopyTo(_processOrder, 0);
    }

    /// <summary>
    /// <para>Returns a 'normal' reference to the ID array.</para>
    /// </summary>
    /// <returns></returns>
    public TrayID[] GetProcessingOrder() 
        => _processOrder;

    /// <summary>
    /// <para>Return stack-bound reference to the ID array.</para>
    /// </summary>
    /// <returns></returns>
    public ref readonly TrayID[] GetProcessingOrderRef() 
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
    public async Task CalculateAsync(TrayStartCorner Corner, TrayDirection Dir)
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
    public void Calculate(TrayStartCorner Corner, TrayDirection Dir)
    {
        Array.Sort(_processOrder, GetComparer(Corner, Dir));
    }

    private static IComparer<TrayID> GetComparer(TrayStartCorner c, TrayDirection d) 
        => (c, d) switch
    {
        (TrayStartCorner.UpperLeft, TrayDirection.Xdirection)
            => new FromTopLeftRowsFirst(),
        (TrayStartCorner.UpperLeft, TrayDirection.YDirection)
            => new FromTopLeftColumnsFirst(),

        (TrayStartCorner.UpperRight, TrayDirection.Xdirection)
            => new FromTopRightRowsFirst(),
        (TrayStartCorner.UpperRight, TrayDirection.YDirection)
            => new FromTopRightColumnsFirst(),

        (TrayStartCorner.LowerLeft, TrayDirection.Xdirection)
            => new FromBottomLeftColumnsFirst(),
        (TrayStartCorner.LowerLeft, TrayDirection.YDirection)
            => new FromBottomLeftRowsFirst(),

        (TrayStartCorner.LowerRight, TrayDirection.Xdirection)
            => new FromBottomRightRowsFirst(),
        (TrayStartCorner.LowerRight, TrayDirection.YDirection)
            => new FromBottomRightColumnsFirst(),

        (_, _) => throw new InvalidOperationException()
    };
}

internal sealed class FromBottomRightColumnsFirst : IComparer<TrayID>
{
    public int Compare(TrayID a, TrayID b)
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
internal sealed class FromBottomRightRowsFirst : IComparer<TrayID>
{
    public int Compare(TrayID a, TrayID b)
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

internal sealed class FromTopRightColumnsFirst : IComparer<TrayID>
{
    public int Compare(TrayID a, TrayID b)
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
internal sealed class FromTopRightRowsFirst : IComparer<TrayID>
{
    public int Compare(TrayID a, TrayID b)
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

internal sealed class FromTopLeftColumnsFirst : IComparer<TrayID>
{
    public int Compare(TrayID a, TrayID b)
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
internal sealed class FromTopLeftRowsFirst : IComparer<TrayID>
{
    public int Compare(TrayID a, TrayID b)
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

internal sealed class FromBottomLeftColumnsFirst : IComparer<TrayID>
{
    public int Compare(TrayID a, TrayID b)
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
internal sealed class FromBottomLeftRowsFirst : IComparer<TrayID>
{
    public int Compare(TrayID a, TrayID b)
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
