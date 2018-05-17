using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        //refactoring
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;

        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests(){
            _name = new Name("Bruce", "Wayne");
            _document =  new Document("22222222222", EDocumentType.CPF);
            _email = new Email("batman@dc.com");
            _address = new Address("rua x", "123", "asdasd", "Sao Paulo", "SP", "Brasil", "1231231");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void AdicionarAssinatura(){

            // var subscription = new Subscription(null);

            // var student = new Student(
            //     new Name(firstName: "Andre",
            //              lastName: "Junqueira"),
            //     new Document("123123", EDocumentType.CPF),
            //     new Email("junqueira.andre@gmail.com")
            // );

            //student.FirstName = "";
            //student.Subscriptions.Add(subscription); //consigo ferir dessa forma
            //student.AddSubscription(subscription);
            // var name = new Name("Joao", "Teste");

            // foreach(var not in name.Notifications){
            //     not.Message;
            // }
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription(){

            var payment = new PayPalPayment("123", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadSubscriptionNoPayment(){
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }        

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription(){
            var payment = new PayPalPayment("123", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Valid);
        }
        
    }
}