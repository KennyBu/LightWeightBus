using System;
using Infrastructure;

namespace Events
{
    [Serializable]
    public class AccountOpenedEvent : IEvent
    {
        public string AccountName { get; set; }
        public decimal InitialBalance { get; set; } 
    }
}