using L2Mentoring.Module1.States;

namespace L2Mentoring.Module1.Interfaces
{
    public interface IArgsVerifier
    {
        ReturnState VerifyArgs(string[] args);
    }
}
