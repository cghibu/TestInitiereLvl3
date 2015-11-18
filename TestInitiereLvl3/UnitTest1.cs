using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestInitiereLvl3
{

    public struct BaseConvertStructure
    {
        public uint baseToConvertTo;
        public uint numberToConvert;
        byte[] numberInBaseAsByteString;

        public void AllocateStorage()
        {
            uint powerOfBase = (uint)Math.Truncate(Math.Log(numberToConvert, baseToConvertTo));
            numberInBaseAsByteString = new byte[powerOfBase + 1];         
        }

        public BaseConvertStructure(uint inputBase, uint inputNumber)
        {
            baseToConvertTo = inputBase;
            numberToConvert = inputNumber;
            numberInBaseAsByteString = null;
        }

        public void ConvertInputToBase()
        {

            AllocateStorage();
            uint mod = numberToConvert / baseToConvertTo;
            uint reminder = numberToConvert % baseToConvertTo;
            int indexToInsertTo = 0;
            if (mod > baseToConvertTo)
            {
                numberInBaseAsByteString[indexToInsertTo] = (byte)reminder;
                indexToInsertTo = indexToInsertTo + 1;
                ConvertInputToBase()
            }
            




        }
        
    }

    [TestClass]
    public class TestBaseConvertor
    {
        [TestMethod]
        public void TestMethod1()
        {
            
        }
    }
}
