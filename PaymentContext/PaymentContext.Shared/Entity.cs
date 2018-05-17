using System;
using System.Collections.Generic;
using Flunt.Notifications;

namespace PaymentContext.Shared
{
    public abstract class Entity : Notifiable
    {
        //public IList<string> Notifications;        
        public Entity()
        {
            Id = Guid.NewGuid(); //nao necessita de banco pra ser gerado
        }

        public Guid Id { get; private set; }
    }
}