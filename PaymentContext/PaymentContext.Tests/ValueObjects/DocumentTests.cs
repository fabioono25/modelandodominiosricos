using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        //red, green, refactor
        [TestMethod]
        public void ShoulReturnErrorWhenCNPJIsInvalid(){

            var doc = new Document("123", EDocumentType.CNPJ);

            Assert.IsTrue(doc.Invalid);
            //Assert.Fail();

        }

        [TestMethod]
        public void ShoulReturnErrorWhenCNPJIsValid(){

            var doc = new Document("12345678901234", EDocumentType.CNPJ);

            Assert.IsTrue(doc.Valid);

        }        

        [TestMethod]
        public void ShoulReturnErrorWhenCPFIsInvalid(){

            var doc = new Document("123", EDocumentType.CPF);

            Assert.IsTrue(doc.Invalid);            

        }        

        [TestMethod]
        [DataTestMethod]
        [DataRow("22232332312")]
        [DataRow("22232332322")]
        [DataRow("22232332777")]
        public void ShoulReturnErrorWhenCPFIsValid(string cpf){

            var doc = new Document(cpf, EDocumentType.CPF);

            Assert.IsTrue(doc.Valid);            

        }        
    }
}