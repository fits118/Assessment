using PubSubMessages.Entities;
using PubSubMessages.Interfaces;
using PubSubMessages.Models;

namespace PubSubMessages.Services
{
	public class PublisherService : IPublisherService
	{
		public virtual void Post(Publisher publisher, Message message, Channel channel)
		{
			message.From = publisher.Name;
			channel.Messages.Enqueue(message);
		}
	}
}
