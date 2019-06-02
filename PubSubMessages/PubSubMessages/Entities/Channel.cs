using PubSubMessages.Models;
using System.Collections.Generic;

namespace PubSubMessages.Entities
{
	public class Channel
	{
		public Channel()
		{
			Subscribers = new List<Subscriber>();
			Messages = new Queue<Message>();
		}

		public IList<Subscriber> Subscribers { get; private set; }
		public Queue<Message> Messages { get; private set; }
	}
}
