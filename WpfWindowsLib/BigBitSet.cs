/**************************************************************************************

WpfWindowsLib.BigBitSet
=======================

BitSet with more than 32 bits

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace WpfWindowsLib {


  public class BigBitSet {

    #region Properties
    //      ----------

    public int BitsCount { get; private set; }


    public const int WordLength = 32;//bits


    uint[] bits;
    static readonly uint[] setBits = makeSetBits();
    static readonly uint[] clearBits = makeMasks();


    private static uint[] makeSetBits() {
      var setBits = new uint[WordLength];
      uint bit = 1;
      for (int bitIndex = 0; bitIndex < WordLength; bitIndex++) {
        setBits[bitIndex] = bit;
        bit <<= 1;
      }
      return setBits;
    }


    private static uint[] makeMasks() {
      var masks = new uint[WordLength];
      for (int bitIndex = 0; bitIndex < WordLength; bitIndex++) {
        masks[bitIndex] = uint.MaxValue^setBits[bitIndex];
      }
      return masks;
    }


    public BigBitSet(int bitsCount = WordLength) {
      bitsCount = Math.Max(bitsCount, WordLength);
      var size = calcSize(bitsCount);
      bits = new uint[size];
      BitsCount = bits.Length * WordLength;
    }


    private int calcSize(int bitsCount) {
      var size = bitsCount / WordLength;
      if (size * WordLength < bitsCount) size++;
      return size;
    }


    public void DoubleSize() {
      var newBits = new uint[2 * bits.Length];
      for (int i = 0; i < bits.Length; i++) {
        newBits[i] = bits[i];
      }
      bits = newBits;
      BitsCount = bits.Length * WordLength;
    }


    public virtual bool this[int bitIndex] {
      get {
        var wordIndex = bitIndex / WordLength;
        var bitOffset = bitIndex % WordLength;
        return (bits[wordIndex] & setBits[bitOffset])>0;
      }

      set {
        var wordIndex = bitIndex / WordLength;
        var bitOffset = bitIndex % WordLength;
        if (value) {
          bits[wordIndex] = bits[wordIndex] | setBits[bitOffset];
        } else {
          bits[wordIndex] = bits[wordIndex] & clearBits[bitOffset];
        }
      }
    }
  }


  public class BigBitSetAuto: BigBitSet {

    public BigBitSetAuto(int bitsCount = WordLength) : base(bitsCount) { }


    public override bool this[int bitIndex] {
      get {
        if (bitIndex>=BitsCount) return false;

        return base[bitIndex];
      }

      set {
        while (bitIndex>=BitsCount) {
          DoubleSize();
        }
        base[bitIndex] = value;
      }

    }
    #endregion
  }
}