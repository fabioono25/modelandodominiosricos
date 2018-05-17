using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;

        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;

            _subscriptions = new List<Subscription>();

            //evitar exceção
            // if (string.IsNullOrEmpty(name.FirstName))
            //     Notifications.Add("Nome invalido");

            // if (string.IsNullOrEmpty(Name.FirstName))
            //     AddNotification("Name.FirstName", "Nome inválido");

            // if (firstName.Length == 0)
            //     throw new Exception("Nome Inválido");

            AddNotifications(name, document, email);
        }

        // public string FirstName { get; private set; }
        // public string LastName { get; private set; }
        public Name Name {get; set;}
        public Document Document { get; private set; }

        public Email Email { get; private set; }
        //se precisar do ID, melhor que endereco seja entidade (externalizar outra tabela etc)
        public Address Address { get; private set; } 
        //public List<Subscription> Subscriptions { get; private set; }           
        //public IReadOnlyCollection<Subscription> Subscriptions { get; private set; }           
        public IReadOnlyCollection<Subscription> Subscriptions { get { 
            return _subscriptions.ToArray();
         }}

        public void AddSubscription(Subscription subscription){
            //se ja tiver uma assinatura ativa, cancela
            //evitar enviar throw Exception aqui
            

            //cancela todas as outras assinaturas, e coloca esta como principal
            // foreach (var sub in Subscriptions)
            // {
            //     sub.Inactivate();
            // } 

            //Subscriptions.Add(subscription);
            //_subscriptions.Add(subscription);

            //if (subscription.Payments.Count == 0)

            var hasSubscriptionActive = false;

            foreach (var sub in _subscriptions)
            {
                if (sub.Active)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract()
                        .Requires()
                        .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa.")
                        .AreEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "Esta assinatura nao possui pagamentos")
            );
        
            //alternativa
            // if (hasSubscriptionActive)
            //     AddNotification("Student.Subscriptions", "Você já tem uma assinatura ativa.");
        }
    }
}