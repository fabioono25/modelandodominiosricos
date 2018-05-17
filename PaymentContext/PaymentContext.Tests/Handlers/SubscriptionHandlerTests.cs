using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShoulReturnErrorWhenDocumentExists(){

            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());

            var command = new CreateBoletoSubscriptionCommand();

            command.BarCode = "12312";
            command.FirstName = "Joao";
            command.LastName = "de Barro";
            command.Document = "99999999999";
            command.Email = "teste@gmail.com";
            command.BarCode = "123";
            command.BoletoNumber = "123";

            command.PaymentNumber = "123";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);

            command.Total = 60;
            command.TotalPaid = 60;

            command.Payer = "NCorp";
            command.PayerDocument = "12312";
            command.PayerDocumentType = EDocumentType.CPF;

            command.PayerEmail = "teste@asdasd.com";
            command.Street = "123";
            command.Number = "qaseasd";
            command.Neighborhood = "asasd";
            command.City = "asd";
            command.State = "ads";
            command.Country = "sfasd";
            command.ZipCode = "2131232";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }        
    }
}