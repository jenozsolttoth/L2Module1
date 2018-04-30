
using L2Mentoring.Module1.Interfaces;

namespace L2Mentoring.Module1.InterfaceImplementations
{
    public class LineSeparator : ILineSeparator
    {
        public string[] Separate(string line)
        {
            return line.Split(';');
        }
    }
}
