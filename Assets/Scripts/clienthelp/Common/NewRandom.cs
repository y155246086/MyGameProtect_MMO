using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class NewRandom
    {
	    private long seed;

	    private const long multiplier = 0x5DEECE66DL;
	    private const long addend = 0xBL;
	    private const long mask = (1L << 48) - 1;

	    public NewRandom(long seed)
	    {
		    SetSeed(seed);
	    }

	    private void SetSeed(long seed)
	    {
		    seed = (seed ^ multiplier) & mask;
		    this.seed = seed;
	    }

	    private int Next(int bits)
	    {
		    long oldseed, nextseed;
		    oldseed = seed;
		    nextseed = (oldseed * multiplier + addend) & mask;
		    seed = nextseed;
            return (int)(MoveByte(nextseed, 48 - bits));
	    }

	    public int NextInt()
	    {
		    return Next(32);
	    }

        public int NextInt(int n)
	    {
		    if (n <= 0)
			    throw new Exception("n must be positive");

		    if ((n & -n) == n) // i.e., n is a power of 2
			    return (int) ((n * (long) Next(31)) >> 31);

		    int bits, val;
		    do
		    {
			    bits = Next(31);
			    val = bits % n;
		    } while (bits - val + (n - 1) < 0);
		    return val;
	    }

	    public long NextLong()
	    {
		    // it's okay that the bottom word remains signed.
            return ((long)(Next(32)) << 32) + Next(32);
	    }

        public bool NextBoolean()
	    {
            return Next(1) != 0;
	    }

        public float NextFloat()
	    {
            return Next(24) / ((float)(1 << 24));
	    }

        public double NextDouble()
	    {
            return (((long)(Next(26)) << 27) + Next(27)) / (double)(1L << 53);
	    }

        public static long MoveByte(long value, int pos)
        {
            if (value < 0)
            {
                string s = Convert.ToString(value, 2);    // 转换为二进制
                for (int i = 0; i < pos; i++)
                {
                    s = "0" + s.Substring(0, 63);
                }
                return Convert.ToInt64(s, 2);            // 将二进制数字转换为数字
            }
            else
            {
                return value >> pos;
            }
        }
    }

}
