using PubSubMessages.Entities;
using PubSubMessages.Models;
using System.Collections.Generic;

namespace PubSubMessages.Interfaces
{
	public interface ISubscriberService
	{
		IList<Message> RetrieveMessages(Subscriber subscriber);
	}
}
