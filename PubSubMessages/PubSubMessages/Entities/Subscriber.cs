using PubSubMessages.Models;
using System.Collections.Generic;

namespace PubSubMessages.Entities
{
	public class Subscriber
	{
		public Subscriber()
		{
			PendingMessages = new Queue<Message>();
		}

		public virtual Queue<Message> PendingMessages { get; set; }
	}
}
