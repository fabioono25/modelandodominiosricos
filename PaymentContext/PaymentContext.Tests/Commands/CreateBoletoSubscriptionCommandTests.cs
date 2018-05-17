using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        //red, green, refactor
        [TestMethod]
        public void ShoulReturnErrorWhenNameIsInvalid(){

            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "";

            command.Validate();

            Assert.AreEqual(false, command.Valid);
        }
    }
}