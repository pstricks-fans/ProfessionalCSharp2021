﻿SpanOnTheHeap();
SpanOnTheStack();
SpanOnNativeMemory();
SpanExtensions();

unsafe void SpanOnNativeMemory()
{
    Console.WriteLine(nameof(SpanOnNativeMemory));
    const int nbytes = 100;
    nint p = (nint)NativeMemory.Alloc(nbytes);
    try
    {
        int* p2 = (int*)p;

        Span<int> span = new(p2, nbytes >> 2);
        span.Fill(42);

        int max = nbytes >> 2;
        for (int i = 0; i < max; i++)
        {
            Console.Write($"{span[i]} ");
        }
        Console.WriteLine();
    }
    finally
    {
        NativeMemory.Free((void*)p);
    }
    Console.WriteLine();
}

unsafe void SpanOnTheStack()
{
    Console.WriteLine(nameof(SpanOnTheStack));

    long* lp = stackalloc long[20];
    Span<long> span1 = new(lp, 20);

    for (int i = 0; i < 20; i++)
    {
        span1[i] = i;
    }

    Console.WriteLine(string.Join(", ", span1.ToArray()));
    Console.WriteLine();
}

void SpanOnTheHeap()
{
    Console.WriteLine(nameof(SpanOnTheHeap));
    Span<int> span1 = (new int[] { 1, 5, 11, 71, 22, 19, 21, 33 }).AsSpan();

    // span1.Slice(start: 4, length: 3).Fill(42);
    span1[4..7].Fill(42);

    Console.WriteLine(string.Join(", ", span1.ToArray()));
    Console.WriteLine();
}

void SpanExtensions()
{
    Console.WriteLine(nameof(SpanExtensions));
    Span<int> span1 = (new int[] { 1, 5, 11, 71, 22, 19, 21, 33 }).AsSpan();
    Span<int> span2 = span1[3..7];

    bool overlaps = span1.Overlaps(span2);
    Console.WriteLine($"span1 overlaps span2: {overlaps}");
    span1.Reverse();
    Console.WriteLine($"span1 reversed: {string.Join(", ", span1.ToArray())}");
    Console.WriteLine($"span2 (a slice) after reversing span1: {string.Join(", ", span2.ToArray())}");
    int index = span1.IndexOf(span2);
    Console.WriteLine($"index of span2 in span1: {index}");
    Console.WriteLine();

    string s1 = "the quick brown fox";
    var spantostring = s1.AsSpan();
}
