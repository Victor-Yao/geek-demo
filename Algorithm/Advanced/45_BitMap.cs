namespace Advanced;

/// <summary>
/// BitMap
/// </summary>
public class BitMap
{
    private byte[] _bytes;
    private int _nbits;

    public BitMap(int nbits)
    {
        _nbits = nbits;
        _bytes = new byte[nbits / 8 + 1];
    }

    public void Set(int k)
    {
        if (k > _nbits)
            return;

        int byteIndex = k / 8;
        int bitIndex = k % 8;
        _bytes[byteIndex] |= (byte)(1 << bitIndex);
    }

    public bool Get(int k)
    {
        if (k > _nbits)
            return false;

        int byteIndex = k / 8;
        int bitIndex = k % 8;
        return (_bytes[byteIndex] & (1 << bitIndex)) != 0;
    }
}

public class BloomFilter
{
    private BitMap _bm;
    private int _nbits;
    private int _salt;

    public BloomFilter(int nbits)
    {
        _bm = new BitMap(nbits);
        _nbits = nbits;
        _salt = new Random().Next(1, 10);
    }

    private int HashFunc1(int k)
    {
        return k;
    }

    private int HashFunc2(int k)
    {
        k *= _salt;
        if (k > _nbits)
            k = k / _nbits;
        return k;
    }

    private int HashFunc3(int k)
    {
        k += 10 * _salt;
        if (k > _nbits)
            k = k / _nbits;
        return k;
    }

    public void Set(int k)
    {
        if (k > _nbits)
            return;

        _bm.Set(HashFunc1(k));
        _bm.Set(HashFunc2(k));
        _bm.Set(HashFunc3(k));
    }

    public bool Get(int k)
    {
        return _bm.Get(HashFunc1(k)) && _bm.Get(HashFunc2(k)) && _bm.Get(HashFunc3(k));
    }
}