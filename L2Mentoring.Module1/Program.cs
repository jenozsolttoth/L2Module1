// L2 mentoring program
// 
// Module 1: Software Design Principles (OOP, Functional programming, SOLID, GRASP, KISS, YAGNI, Domain Driven Design)
// 
// Task: Refactor this code using software design principles. 
// 
// Important Note: this task IS NOT heavily focused on desing patterns, but more or object's design, SOLID, DRY, KISS and clean code. 
// If a mentee is able to introduce some petterns, it's good but not required.  
// 
// Questions -> Oleksandr_Zhevzhyk@epam.com

using StructureMap;

namespace L2Mentoring.Module1
{
    class Program
    {
        public static int Main(string[] args)
        {
            Container container = new ModuleConfiguration().ConfigureModule();
            Runner runner = container.GetInstance<Runner>();
            return runner.Run(args);
        }

        public static void SendEmail(string topic, string body, string from, string to)
        {
            // sending email 
        }
    }
}
