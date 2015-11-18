using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestInitiereLvl3
{

    public struct BaseConvertStructure
    {
        public uint baseToConvertTo;
        public uint numberToConvert;
        private byte[] numberInBaseAsByteString;

        private byte[] AllocateStorage()
        {
            uint powerOfBase = (uint)Math.Truncate(Math.Log(numberToConvert, baseToConvertTo));
            return new byte[powerOfBase + 1];         
        }

        public BaseConvertStructure(uint inputBase, uint inputNumber)
        {
            baseToConvertTo = inputBase;
            numberToConvert = inputNumber;
            numberInBaseAsByteString = null;
            numberInBaseAsByteString = AllocateStorage();
            ConvertNumberToBase(baseToConvertTo, numberToConvert, 0, ref numberInBaseAsByteString);
            TransposeArray(ref numberInBaseAsByteString);
        }

        private void TransposeArray(ref byte[] arrayToTranspose)
        {
            for (int i = 0; i < arrayToTranspose.Length/2; i++)
            {
                byte temp = arrayToTranspose[i];
                arrayToTranspose[i] = arrayToTranspose[arrayToTranspose.Length - i - 1];
                arrayToTranspose[arrayToTranspose.Length - i - 1] = temp;
             }

        }

        private void ConvertNumberToBase(uint theBase, uint theNumber, int recurrsion, ref byte[] result)
        {
                     
            uint div = theNumber / theBase;
            uint reminder = theNumber % theBase;
            result[recurrsion] = (byte)reminder;
            if (div >= baseToConvertTo)
            {
                ConvertNumberToBase(theBase, div, recurrsion + 1, ref result);
            }
            else
            {
                result[recurrsion+1] = (byte)div;
            }
                                   
        }
        public byte[] GetBaseRepresentation()
        {
            return numberInBaseAsByteString;
        }

        public byte[] Not()
        {

            for (int i = 0; i < numberInBaseAsByteString.Length; i++)
            {
                byte temp = numberInBaseAsByteString[i];
                numberInBaseAsByteString[i] = (byte)(baseToConvertTo - 1 - temp);
            }

            return numberInBaseAsByteString;
        }
    }

    [TestClass]
    public class TestBaseConvertor
    {
        [TestMethod]
        public void TestConstructorandConversionBase2()
        {

            BaseConvertStructure test = new BaseConvertStructure(2,22);
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
        public void TestConstructorandConversionBase5()
        {

            BaseConvertStructure test = new BaseConvertStructure(5, 75);
            byte[] expected = new byte[3];
            expected[0] = 3;
            expected[1] = 0;
            expected[2] = 0;

            byte[] result = test.GetBaseRepresentation();

            CollectionAssert.AreEqual(result, expected);
        }
        [TestMethod]
        public void TestNotOperationBase2()
        {

            BaseConvertStructure test = new BaseConvertStructure(2, 22);
            byte[] result = test.Not();
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
            byte[] result = test.Not();
            byte[] expected = new byte[2];
            expected[0] = 2;
            expected[1] = 1;
            
            CollectionAssert.AreEqual(result, expected);
        }

    }
}
