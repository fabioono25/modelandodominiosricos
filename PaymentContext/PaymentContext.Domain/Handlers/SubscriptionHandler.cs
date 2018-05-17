using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, 
                IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        //utilizando injeção de dependencia
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        protected SubscriptionHandler()
        {
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail fast validations
            command.Validate();

            if (command.Invalid){
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            //verificar se documento já está cadastrado
            // AddNotifications(new Contract()
            // );
            if (_repository.DocumentExists(command.Document)){
                AddNotification("Document", "Este CPF já está em uso");
                //AddNotifications(command);
                //return new CommandResult(false, "Não foi possível realizar sua assinatura");                
            }

            //verificar se email já está cadastrado
            if (_repository.EmailExists(command.Email)){
                AddNotification("Email", "Este e-mail já está em uso");
            }

            //gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document =  new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), address, email);

            //relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //checar as notificacoes
            if (Invalid)
                return new CommandResult(false, "Não foi possivel realizar a sua assinatura");

            //salvar as informações
            _repository.CreateSubscription(student);

            //enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao curso", "Sua assinatura foi criada");

            //retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}