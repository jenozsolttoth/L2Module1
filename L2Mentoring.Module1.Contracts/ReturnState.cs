using System;

namespace L2Mentoring.Module1.Contracts
{
    public class ReturnState
    {
        public static readonly ReturnState READY = new ReturnState(0, "READY");
        public static readonly ReturnState NOPRODUCTS = new ReturnState(1, "No products in the order.");
        public static readonly ReturnState PRODUCTNOTFOUND = new ReturnState(2, "Product not found.");
        public static readonly ReturnState HELP = new ReturnState(3, "L2Mentorin.Module1.exe usage:\n" +
                                                                     " \tL2Mentoring.Module1.exe \"Oleksandr Zhevzhyk\" " +
                                                                     "\"ProductA:2;ProductB:1;ProductC:1\"");
        public static readonly ReturnState NOPARAMETERS = new ReturnState(4, "No parameters provided. Run L2Mentoring.Module1.exe help");
        public static readonly ReturnState ORDERWASNOTPOSTED = new ReturnState(5, "Order was not posted.");
        public static readonly ReturnState OK = new ReturnState(7, "OK");

        private ReturnState(int returnCode, string message)
        {
            ReturnCode = returnCode;
            Message = message;
        }
        public int ReturnCode { get; }
        public string Message { get; }
    }
}
