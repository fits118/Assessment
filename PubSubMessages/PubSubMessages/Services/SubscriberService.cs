using PubSubMessages.Entities;
using PubSubMessages.Interfaces;
using PubSubMessages.Models;
using System.Collections.Generic;
using System.Linq;

namespace PubSubMessages.Services
{
	public class SubscriberService : ISubscriberService
	{
		public IList<Message> RetrieveMessages(Subscriber subscriber)
		{
			var messages = subscriber.PendingMessages.ToList();
			subscriber.PendingMessages.Clear();

			return messages;
		}
	}
}
