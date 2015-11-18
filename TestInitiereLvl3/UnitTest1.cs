using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestInitiereLvl3
{

    public struct BaseConvertStructure
    {
        private uint baseToConvertTo;
        private uint numberToConvert;
        private byte[] numberInBaseAsByteString;

        private byte[] AllocateStorage()
        {
            uint powerOfBase = (uint)Math.Truncate(Math.Log(this.numberToConvert, this.baseToConvertTo));
            return new byte[powerOfBase + 1];         
        }

        public BaseConvertStructure(uint inputBase, uint inputNumber)
        {
            if (inputBase > 36)
            {
                this.baseToConvertTo = 36;
            }
            else
            {
                this.baseToConvertTo = inputBase;
            }
            this.numberToConvert = inputNumber;
            this.numberInBaseAsByteString = null;
            this.numberInBaseAsByteString = this.AllocateStorage();
            ConvertNumberToBase(this.baseToConvertTo, this.numberToConvert, 0, ref this.numberInBaseAsByteString);
            TransposeArray(ref this.numberInBaseAsByteString);
        }

        private static void TransposeArray(ref byte[] arrayToTranspose)
        {
            for (int i = 0; i < arrayToTranspose.Length/2; i++)
            {
                byte temp = arrayToTranspose[i];
                arrayToTranspose[i] = arrayToTranspose[arrayToTranspose.Length - i - 1];
                arrayToTranspose[arrayToTranspose.Length - i - 1] = temp;
             }

        }

        private static bool CheckZeroNineRepresentation(uint number)
        {

            if (number > 9) return false;
            return true;

        }
        private static byte BuildAlphanumericRepresnetation(uint number)
        {
            if (CheckZeroNineRepresentation(number)) {

                return (byte)number;
            }
            uint difference = number - 9;
            return (byte)(difference + 64);

        }
        private static void ConvertNumberToBase(uint theBase, uint theNumber, int recurrsion, ref byte[] result)
        {
                     
            uint div = theNumber / theBase;
            uint reminder = theNumber % theBase;
            result[recurrsion] = BuildAlphanumericRepresnetation(reminder);
                      
            if (div >= theBase)
            {
                ConvertNumberToBase(theBase, div, recurrsion + 1, ref result);
            }
            else
            {
                result[recurrsion + 1] = BuildAlphanumericRepresnetation(div);            }
                                   
        }
        public byte[] GetBaseRepresentation()
        {
            return numberInBaseAsByteString;
        }

        public static BaseConvertStructure operator ! (BaseConvertStructure member)
        {
           for (int i = 0; i < member.numberInBaseAsByteString.Length; i++)
            {
                byte temp = member.numberInBaseAsByteString[i];

                if (member.baseToConvertTo > 10)
                {
                    member.numberInBaseAsByteString[i] = (byte)(member.baseToConvertTo - 1 - (uint)temp - 9 + 64);
                }
                else
                {
                    member.numberInBaseAsByteString[i] = (byte)(member.baseToConvertTo - 1 - temp);
                }
            }

            return member;
        }
    }

    [TestClass]
    public class TestBaseConvertor
    {
        [TestMethod]
        public void TestConstructorAndConversionBase2()
        {

            BaseConvertStructure test = new BaseConvertStructure(2, 22);
            byte[] result = test.GetBaseRepresentation();

            byte[] expected = new byte[5];
            expected[0] = 1;
            expected[1] = 0;
            expected[2] = 1;
            expected[3] = 1;
            expected[4] = 0;

            CollectionAssert.AreEqual(result, expected);
        }
        [TestMethod]
        public void TestConstructorAndConversionBase5()
        {

            BaseConvertStructure test = new BaseConvertStructure(5, 75);
            byte[] result = test.GetBaseRepresentation();

            byte[] expected = new byte[3];
            expected[0] = 3;
            expected[1] = 0;
            expected[2] = 0;

            CollectionAssert.AreEqual(result, expected);
        }
        [TestMethod]
        public void TestNotOperationBase2()
        {

            BaseConvertStructure test = new BaseConvertStructure(2, 22);
            BaseConvertStructure notTest = !test;
            byte[] result = notTest.GetBaseRepresentation();

            byte[] expected = new byte[5];
            expected[0] = 0;
            expected[1] = 1;
            expected[2] = 0;
            expected[3] = 0;
            expected[4] = 1;

            CollectionAssert.AreEqual(result, expected);
        }
        [TestMethod]
        public void TestNotOperationBase5()
        {

            BaseConvertStructure test = new BaseConvertStructure(5, 13);
            BaseConvertStructure notTest = !test;
            byte[] result = notTest.GetBaseRepresentation();

            byte[] expected = new byte[2];
            expected[0] = 2;
            expected[1] = 1;

            CollectionAssert.AreEqual(result, expected);
        }
        [TestMethod]
        public void TestConstructorAndConversionBase16()
        {

            BaseConvertStructure test = new BaseConvertStructure(16, 31);
            byte[] result = test.GetBaseRepresentation();

            byte[] expected = new byte[2];
            expected[0] = 1;
            expected[1] = 70;       

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestNotOperationBase16()
        {

            BaseConvertStructure test = new BaseConvertStructure(16, 31);
            BaseConvertStructure notTest = !test;
            byte[] result = notTest.GetBaseRepresentation();

            byte[] expected = new byte[2];
            expected[0] = 69;
            expected[1] = 0;
            
            CollectionAssert.AreEqual(result, expected);
        }

    }
}
