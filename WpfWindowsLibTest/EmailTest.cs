﻿/**************************************************************************************

WpfWindowsLibTest.BigBitSetTest
===============================

Tests for EmailTextBox

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


  [TestClass()]
  public class EmailTest {


    [TestMethod()]
    public void TestEmailChar() {
      //default settings, must be reapplied because another test might have changed them
      EmailTextBox.SetAsciiSpecialCharsDefault();
      EmailTextBox.IsBlankAllowed = false;
      EmailTextBox.IsInternationalCharSetAllowed = false;
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('\x00'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('\x1F'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('!'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('"'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('#'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('$'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('%'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('&'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('\''));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('('));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar(')'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('*'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('+'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar(','));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('-'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('.'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('/'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar(' '));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('0'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('1'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('8'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('9'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar(':'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar(';'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('<'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('='));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('>'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('?'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('@'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('A'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('B'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('Y'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('Z'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('['));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('\\'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar(']'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('^'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('_'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('`'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('a'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('b'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('y'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('z'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('{'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('|'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('}'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('~'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('\x7F'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('试'));

      EmailTextBox.SetExtendedAsciiSpecialChars();
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('\x00'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('\x1F'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('!'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('"'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('#'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('$'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('%'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('&'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('\''));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('('));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar(')'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('*'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('+'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar(','));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('-'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('.'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('/'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar(' '));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('0'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('1'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('8'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('9'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar(':'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar(';'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('<'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('='));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('>'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('?'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('@'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('A'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('B'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('Y'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('Z'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('['));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('\\'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar(']'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('^'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('_'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('`'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('a'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('b'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('y'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('z'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('{'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('|'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('}'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('~'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('\x7F'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('试'));

      EmailTextBox.SetExtendedQuotedAsciiSpecialChars();
      EmailTextBox.IsBlankAllowed = true;
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('\x00'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('\x1F'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('!'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('"'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('#'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('$'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('%'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('&'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('\''));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('('));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar(')'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('*'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('+'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar(','));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('-'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('.'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('/'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar(' '));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('0'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('1'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('8'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('9'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar(':'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar(';'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('<'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('='));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('>'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('?'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('@'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('A'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('B'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('Y'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('Z'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('['));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('\\'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar(']'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('^'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('_'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('`'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('a'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('b'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('y'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('z'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('{'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('|'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('}'));
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('~'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('\x7F'));
      Assert.IsFalse(EmailTextBox.IsValidEmailChar('试'));

      EmailTextBox.IsInternationalCharSetAllowed = true;
      Assert.IsTrue(EmailTextBox.IsValidEmailChar('试'));
    }


    [TestMethod()]
    public void TestIsValidDnsChar() {
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('\x00'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('\x1F'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('!'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('"'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('#'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('$'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('%'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('&'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('\''));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('('));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault(')'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('*'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('+'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault(','));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('-'));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('.'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('/'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault(' '));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('0'));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('1'));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('8'));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('9'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault(':'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault(';'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('<'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('='));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('>'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('?'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('@'));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('A'));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('B'));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('Y'));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('Z'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('['));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('\\'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault(']'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('^'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('_'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('`'));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('a'));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('b'));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('y'));
      Assert.IsTrue(EmailTextBox.IsValidEmailDnsDefault('z'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('{'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('|'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('}'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('~'));
      Assert.IsFalse(EmailTextBox.IsValidEmailDnsDefault('\x7F'));
    }

    [TestMethod()]
    public void TestEmailAdr() {
      //default settings, must be reapplied because another test might have changed them
      EmailTextBox.SetAsciiSpecialCharsDefault();
      EmailTextBox.IsBlankAllowed = false;
      EmailTextBox.IsInternationalCharSetAllowed = false;
      Assert.IsFalse(EmailTextBox.IsValidEmail(""));
      Assert.IsFalse(EmailTextBox.IsValidEmail("@"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@b"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@b."));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@b.c"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("ab.cd@"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@b..cD"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z+@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z+Y@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z-@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z-Y@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z.@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z.Y@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z.Y@b.cd.ef"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ@abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("01234567890123456789012345678901234567890123456789012345678901234@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail(new string('a', 64) + "@" + "a." + new string('a', 255-2)));
      Assert.IsFalse(EmailTextBox.IsValidEmail(new string('a', 64) + "@" + "a." + new string('a', 256-2)));
      Assert.IsFalse(EmailTextBox.IsValidEmail(new string('a', 65) + "@" + "a." + new string('a', 255-2)));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a!Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a#Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a$Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a%Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a&Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a'Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a*Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a/Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a=Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a?Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a^Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a`Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a{Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a|Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a}Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a~Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a\"Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a(Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a)Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a,Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a:Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a;Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a<Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a>Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a[Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a\\Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a]Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("\"a Z\"@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("ABC@[192.168.0.1]"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("ABC@[IPv6:2001:db8:1ff::a0b:dbd0]"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("punnycode@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("试.یشی@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));

      EmailTextBox.SetExtendedAsciiSpecialChars();
      Assert.IsFalse(EmailTextBox.IsValidEmail(""));
      Assert.IsFalse(EmailTextBox.IsValidEmail("@"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@b"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@b."));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@b.c"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("ab.cd@"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@b..cD"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z+@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z+Y@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z-@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z-Y@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z.@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z.Y@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z.Y@b.cd.ef"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ@abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("01234567890123456789012345678901234567890123456789012345678901234@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail(new string('a', 64) + "@" + "a." + new string('a', 255-2)));
      Assert.IsFalse(EmailTextBox.IsValidEmail(new string('a', 64) + "@" + "a." + new string('a', 256-2)));
      Assert.IsFalse(EmailTextBox.IsValidEmail(new string('a', 65) + "@" + "a." + new string('a', 255-2)));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a!Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a#Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a$Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a%Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a&Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a'Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a*Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a/Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a=Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a?Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a^Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a`Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a{Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a|Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a}Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a~Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a\"Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a(Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a)Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a,Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a:Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a;Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a<Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a>Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a[Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a\\Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a]Z@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("\"a Z\"@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("ABC@[192.168.0.1]"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("ABC@[IPv6:2001:db8:1ff::a0b:dbd0]"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("punnycode@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("试.یشی@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));

      EmailTextBox.SetExtendedQuotedAsciiSpecialChars();
      EmailTextBox.IsBlankAllowed = true;
      Assert.IsFalse(EmailTextBox.IsValidEmail(""));
      Assert.IsFalse(EmailTextBox.IsValidEmail("@"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@b"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@b."));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@b.c"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("ab.cd@"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("a@b..cD"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z+@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z+Y@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z-@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z-Y@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z.@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z.Y@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a_Z.Y@b.cd.ef"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ@abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("01234567890123456789012345678901234567890123456789012345678901234@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail(new string('a', 64) + "@" + "a." + new string('a', 255-2)));
      Assert.IsFalse(EmailTextBox.IsValidEmail(new string('a', 64) + "@" + "a." + new string('a', 256-2)));
      Assert.IsFalse(EmailTextBox.IsValidEmail(new string('a', 65) + "@" + "a." + new string('a', 255-2)));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a!Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a#Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a$Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a%Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a&Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a'Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a*Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a/Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a=Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a?Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a^Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a`Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a{Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a|Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a}Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a~Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a\"Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a(Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a)Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a,Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a:Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a;Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a<Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a>Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a[Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a\\Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("a]Z@b.cd"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("\"a Z\"@b.cd"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("ABC@[192.168.0.1]"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("ABC@[IPv6:2001:db8:1ff::a0b:dbd0]"));
      Assert.IsTrue(EmailTextBox.IsValidEmail("punnycode@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));
      Assert.IsFalse(EmailTextBox.IsValidEmail("试.یشی@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));

      EmailTextBox.IsInternationalCharSetAllowed = true;
      Assert.IsTrue(EmailTextBox.IsValidEmail("试.یشی@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));
    }
  }
}