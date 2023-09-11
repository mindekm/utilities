// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace Utilities;

using System.Diagnostics;

public readonly struct ValueStopwatch
{
    private static readonly double TimestampToTicks = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;

    private readonly long startTimestamp;

    private ValueStopwatch(long startTimestamp)
    {
        this.startTimestamp = startTimestamp;
    }

    public bool IsActive => startTimestamp != 0;

    public static ValueStopwatch StartNew() => new ValueStopwatch(Stopwatch.GetTimestamp());

    public TimeSpan GetElapsedTime()
    {
        // Start timestamp can't be zero in an initialized ValueStopwatch. It would have to be literally the first thing executed when the machine boots to be 0.
        // So it being 0 is a clear indication of default(ValueStopwatch)
        if (!IsActive)
        {
            throw new InvalidOperationException("An uninitialized, or 'default', ValueStopwatch cannot be used to get elapsed time.");
        }

        long end = Stopwatch.GetTimestamp();
        long timestampDelta = end - startTimestamp;
        long ticks = (long)(TimestampToTicks * timestampDelta);
        return new TimeSpan(ticks);
    }
}
