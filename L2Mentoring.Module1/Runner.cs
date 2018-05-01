using System.Collections.Generic;
using System.Linq;
using L2Mentoring.Module1.Interfaces;
using L2Mentoring.Module1.States;
using ServiceInterfaces;

namespace L2Mentoring.Module1
{
    public class Runner:IRunner
    {
        private readonly ICustomerService _customerService;
        private readonly IArgsVerifier _argsVerifyer;
        private readonly ICustomerAttendant _customerAttendant;
        public Runner(
            ICustomerService customerService,  
            IArgsVerifier argsVerifyer, 
            ICustomerAttendant customerAttendant
            )
        {
            _customerService = customerService;
            _argsVerifyer = argsVerifyer;
            _customerAttendant = customerAttendant;
        }
        public ReturnState Startup(string[] args)
        {
            ReturnState argsVerification = _argsVerifyer.VerifyArgs(args);
            if (argsVerification == ReturnState.Ok)
            {
                var customerResponse = _customerService.GetCustomer(args[0]);
                ICustomer currentCustomer = customerResponse.Entity;

                var servingResponse = _customerAttendant.AttendCustomer(currentCustomer, args[1]);

                List<IProduct> orderedProducts = new List<IProduct>();
                if (servingResponse.Success)
                {
                    orderedProducts = servingResponse.Entity.ToList();
                }
            }
            else
            {
                return argsVerification;
            }
            return ReturnState.Ready;
        }
    }
}
