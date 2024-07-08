using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FastestEnumCollectionContains;

internal static class Ext
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsEnum<T>(this ReadOnlySpan<T> haystack, T needle)
        where T : struct, Enum
    {
        var underlying = typeof(T).GetEnumUnderlyingType();
        if (underlying == typeof(byte)) return Contains<byte>(haystack, needle);
        if (underlying == typeof(sbyte)) return Contains<sbyte>(haystack, needle);
        if (underlying == typeof(short)) return Contains<short>(haystack, needle);
        if (underlying == typeof(ushort)) return Contains<ushort>(haystack, needle);
        if (underlying == typeof(int)) return Contains<int>(haystack, needle);
        if (underlying == typeof(uint)) return Contains<uint>(haystack, needle);
        if (underlying == typeof(long)) return Contains<long>(haystack, needle);
        if (underlying == typeof(ulong)) return Contains<ulong>(haystack, needle);
        throw new NotSupportedException();
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool Contains<U>(ReadOnlySpan<T> haystack, T needle)
            where U : unmanaged, IEquatable<U>
        {
            var primitive = Unsafe.BitCast<T, U>(needle);
            return MemoryMarshal.Cast<T, U>(haystack).Contains(primitive);
        }
    }
}