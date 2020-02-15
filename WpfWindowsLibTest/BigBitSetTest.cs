/**************************************************************************************

WpfWindowsLibTest.BigBitSetTest
===============================

Test for BigBitSet

Written in 2020 by Jürgpeter Huber 
Contact: PeterCode at Peterbox dot com

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfWindowsLib {


  [TestClass]
  public class BigBitSetTest {


    [TestMethod]
    public void TestBigBitSet() {
      var bits = new BigBitSet(1);
      assert(bits);
      bits = new BigBitSet(32);
      assert(bits);
      bits = new BigBitSet(33);
      assert(bits);
      bits = new BigBitSet(64);
      assert(bits);
      bits = new BigBitSet(65);
      assert(bits);
    }


    private void assert(BigBitSet bits) {
      var expectedBits = new bool[bits.BitsCount];
      for (int bitIndex = 0; bitIndex < bits.BitsCount; bitIndex++) {
        bits[bitIndex] = true;
        expectedBits[bitIndex] = true;
        assert(expectedBits, bits);
      }
      for (int bitIndex = 0; bitIndex < bits.BitsCount; bitIndex++) {
        bits[bitIndex] = false;
        expectedBits[bitIndex] = false;
        assert(expectedBits, bits);
      }
    }


    private void assert(bool[] expectedBits, BigBitSet bits) {
      for (int bitIndex = 0; bitIndex < bits.BitsCount; bitIndex++) {
        Assert.AreEqual(expectedBits[bitIndex], bits[bitIndex]);
      }
    }


    [TestMethod]
    public void TestBigBitSetAuto() {
      var bits = new BigBitSetAuto();
      var index = 0;
      var value = false;
      for (int i = 0; i < 20; i++) {
        for (int j = 0; j < i; j++) {
          bits[index++] = value;
        }
        value = !value;
      }

      index = 0;
      value = false;
      for (int i = 0; i < 20; i++) {
        for (int j = 0; j < i; j++) {
          Assert.AreEqual(value, bits[index++]);
        }
        value = !value;
      }
    }
  }
}
