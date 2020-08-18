/**************************************************************************************

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
      EmailValidator.SetAsciiSpecialCharsDefault();
      EmailValidator.IsBlankAllowed = false;
      EmailValidator.IsInternationalCharSetAllowed = false;
      Assert.IsFalse(EmailValidator.IsValidEmailChar('\x00'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('\x1F'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('!'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('"'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('#'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('$'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('%'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('&'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('\''));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('('));
      Assert.IsFalse(EmailValidator.IsValidEmailChar(')'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('*'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('+'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar(','));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('-'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('.'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('/'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar(' '));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('0'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('1'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('8'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('9'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar(':'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar(';'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('<'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('='));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('>'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('?'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('@'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('A'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('B'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('Y'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('Z'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('['));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('\\'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar(']'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('^'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('_'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('`'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('a'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('b'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('y'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('z'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('{'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('|'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('}'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('~'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('\x7F'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('试'));

      EmailValidator.SetExtendedAsciiSpecialChars();
      Assert.IsFalse(EmailValidator.IsValidEmailChar('\x00'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('\x1F'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('!'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('"'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('#'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('$'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('%'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('&'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('\''));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('('));
      Assert.IsFalse(EmailValidator.IsValidEmailChar(')'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('*'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('+'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar(','));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('-'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('.'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('/'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar(' '));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('0'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('1'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('8'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('9'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar(':'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar(';'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('<'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('='));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('>'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('?'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('@'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('A'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('B'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('Y'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('Z'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('['));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('\\'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar(']'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('^'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('_'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('`'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('a'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('b'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('y'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('z'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('{'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('|'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('}'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('~'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('\x7F'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('试'));

      EmailValidator.SetExtendedQuotedAsciiSpecialChars();
      EmailValidator.IsBlankAllowed = true;
      Assert.IsFalse(EmailValidator.IsValidEmailChar('\x00'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('\x1F'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('!'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('"'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('#'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('$'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('%'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('&'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('\''));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('('));
      Assert.IsTrue(EmailValidator.IsValidEmailChar(')'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('*'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('+'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar(','));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('-'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('.'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('/'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar(' '));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('0'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('1'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('8'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('9'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar(':'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar(';'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('<'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('='));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('>'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('?'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('@'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('A'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('B'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('Y'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('Z'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('['));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('\\'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar(']'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('^'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('_'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('`'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('a'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('b'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('y'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('z'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('{'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('|'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('}'));
      Assert.IsTrue(EmailValidator.IsValidEmailChar('~'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('\x7F'));
      Assert.IsFalse(EmailValidator.IsValidEmailChar('试'));

      EmailValidator.IsInternationalCharSetAllowed = true;
      Assert.IsTrue(EmailValidator.IsValidEmailChar('试'));
    }


    [TestMethod()]
    public void TestIsValidDnsChar() {
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('\x00'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('\x1F'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('!'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('"'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('#'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('$'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('%'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('&'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('\''));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('('));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault(')'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('*'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('+'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault(','));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('-'));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('.'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('/'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault(' '));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('0'));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('1'));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('8'));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('9'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault(':'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault(';'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('<'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('='));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('>'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('?'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('@'));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('A'));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('B'));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('Y'));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('Z'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('['));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('\\'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault(']'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('^'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('_'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('`'));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('a'));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('b'));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('y'));
      Assert.IsTrue(EmailValidator.IsValidEmailDnsDefault('z'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('{'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('|'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('}'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('~'));
      Assert.IsFalse(EmailValidator.IsValidEmailDnsDefault('\x7F'));
    }

    [TestMethod()]
    public void TestEmailAdr() {
      //default settings, must be reapplied because another test might have changed them
      EmailValidator.SetAsciiSpecialCharsDefault();
      EmailValidator.IsBlankAllowed = false;
      EmailValidator.IsInternationalCharSetAllowed = false;
      Assert.IsFalse(EmailValidator.IsValidEmail(""));
      Assert.IsFalse(EmailValidator.IsValidEmail("@"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@b"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@b."));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@b.c"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("ab.cd@"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@b..cD"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z+@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z+Y@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z-@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z-Y@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z.@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z.Y@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z.Y@b.cd.ef"));
      Assert.IsTrue(EmailValidator.IsValidEmail("abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ@abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
      Assert.IsFalse(EmailValidator.IsValidEmail("01234567890123456789012345678901234567890123456789012345678901234@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail(new string('a', 64) + "@" + "a." + new string('a', 255-2)));
      Assert.IsFalse(EmailValidator.IsValidEmail(new string('a', 64) + "@" + "a." + new string('a', 256-2)));
      Assert.IsFalse(EmailValidator.IsValidEmail(new string('a', 65) + "@" + "a." + new string('a', 255-2)));
      Assert.IsFalse(EmailValidator.IsValidEmail("a!Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a#Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a$Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a%Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a&Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a'Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a*Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a/Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a=Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a?Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a^Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a`Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a{Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a|Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a}Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a~Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a\"Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a(Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a)Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a,Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a:Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a;Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a<Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a>Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a[Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a\\Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a]Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("\"a Z\"@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("ABC@[192.168.0.1]"));
      Assert.IsFalse(EmailValidator.IsValidEmail("ABC@[IPv6:2001:db8:1ff::a0b:dbd0]"));
      Assert.IsTrue(EmailValidator.IsValidEmail("punnycode@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));
      Assert.IsFalse(EmailValidator.IsValidEmail("试.یشی@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));

      EmailValidator.SetExtendedAsciiSpecialChars();
      Assert.IsFalse(EmailValidator.IsValidEmail(""));
      Assert.IsFalse(EmailValidator.IsValidEmail("@"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@b"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@b."));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@b.c"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("ab.cd@"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@b..cD"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z+@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z+Y@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z-@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z-Y@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z.@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z.Y@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z.Y@b.cd.ef"));
      Assert.IsTrue(EmailValidator.IsValidEmail("abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ@abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
      Assert.IsFalse(EmailValidator.IsValidEmail("01234567890123456789012345678901234567890123456789012345678901234@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail(new string('a', 64) + "@" + "a." + new string('a', 255-2)));
      Assert.IsFalse(EmailValidator.IsValidEmail(new string('a', 64) + "@" + "a." + new string('a', 256-2)));
      Assert.IsFalse(EmailValidator.IsValidEmail(new string('a', 65) + "@" + "a." + new string('a', 255-2)));
      Assert.IsTrue(EmailValidator.IsValidEmail("a!Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a#Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a$Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a%Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a&Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a'Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a*Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a/Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a=Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a?Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a^Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a`Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a{Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a|Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a}Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a~Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a\"Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a(Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a)Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a,Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a:Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a;Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a<Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a>Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a[Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a\\Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a]Z@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("\"a Z\"@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("ABC@[192.168.0.1]"));
      Assert.IsFalse(EmailValidator.IsValidEmail("ABC@[IPv6:2001:db8:1ff::a0b:dbd0]"));
      Assert.IsTrue(EmailValidator.IsValidEmail("punnycode@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));
      Assert.IsFalse(EmailValidator.IsValidEmail("试.یشی@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));

      EmailValidator.SetExtendedQuotedAsciiSpecialChars();
      EmailValidator.IsBlankAllowed = true;
      Assert.IsFalse(EmailValidator.IsValidEmail(""));
      Assert.IsFalse(EmailValidator.IsValidEmail("@"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@b"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@b."));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@b.c"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("ab.cd@"));
      Assert.IsFalse(EmailValidator.IsValidEmail("a@b..cD"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z+@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z+Y@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z-@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z-Y@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z.@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z.Y@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a_Z.Y@b.cd.ef"));
      Assert.IsTrue(EmailValidator.IsValidEmail("abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ@abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
      Assert.IsFalse(EmailValidator.IsValidEmail("01234567890123456789012345678901234567890123456789012345678901234@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail(new string('a', 64) + "@" + "a." + new string('a', 255-2)));
      Assert.IsFalse(EmailValidator.IsValidEmail(new string('a', 64) + "@" + "a." + new string('a', 256-2)));
      Assert.IsFalse(EmailValidator.IsValidEmail(new string('a', 65) + "@" + "a." + new string('a', 255-2)));
      Assert.IsTrue(EmailValidator.IsValidEmail("a!Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a#Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a$Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a%Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a&Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a'Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a*Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a/Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a=Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a?Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a^Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a`Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a{Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a|Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a}Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a~Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a\"Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a(Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a)Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a,Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a:Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a;Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a<Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a>Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a[Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a\\Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("a]Z@b.cd"));
      Assert.IsTrue(EmailValidator.IsValidEmail("\"a Z\"@b.cd"));
      Assert.IsFalse(EmailValidator.IsValidEmail("ABC@[192.168.0.1]"));
      Assert.IsFalse(EmailValidator.IsValidEmail("ABC@[IPv6:2001:db8:1ff::a0b:dbd0]"));
      Assert.IsTrue(EmailValidator.IsValidEmail("punnycode@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));
      Assert.IsFalse(EmailValidator.IsValidEmail("试.یشی@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));

      EmailValidator.IsInternationalCharSetAllowed = true;
      Assert.IsTrue(EmailValidator.IsValidEmail("试.یشی@XN--0ZWM56D.XN--HGBK6AJ7F53BBA"));
    }
  }
}