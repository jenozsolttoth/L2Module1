using System;
using L2Mentoring.Module1.Interfaces;
using L2Mentoring.Module1.States;

namespace L2Mentoring.Module1.InterfaceImplementations
{
    public class ArgsVerifier : IArgsVerifier
    {
        public ReturnState VerifyArgs(string[] args)
        {
            if ( args.Length > 0 )
            {
                string customerName= args[0];
                if ( customerName != "help" )
                {
                    return ReturnState.Ok;
                }

                Console.WriteLine("L2Mentoring.Module1.exe usage:");
                Console.WriteLine("\tL2Mentoring.Module1.exe \"Oleksandr Zhevzhyk\" \"ProductA:2;ProductB:1;ProductC:1\"");
                Console.WriteLine("\tL2Mentoring.Module1.exe \"\" \"ProductA:1;ProductB:2\"");
                return ReturnState.Help;
            }
            Console.WriteLine("No parameters provided.");
            Console.WriteLine("Run L2Mentoring.Module1.exe help");
            return ReturnState.Noparameters;
        }
    }
}


