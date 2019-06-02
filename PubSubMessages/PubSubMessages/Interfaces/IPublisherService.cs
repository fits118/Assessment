using PubSubMessages.Entities;
using PubSubMessages.Models;

namespace PubSubMessages.Interfaces
{
	public interface IPublisherService
	{
		void Post(Publisher publisher, Message message, Channel channel);
	}
}
