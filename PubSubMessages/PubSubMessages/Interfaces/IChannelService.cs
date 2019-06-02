using PubSubMessages.Entities;

namespace PubSubMessages.Interfaces
{
	public interface IChannelService
	{
		void SubscribeChannel(Channel channel, Subscriber subscriber);
		void UnsubscribeChannel(Channel channel, Subscriber subscriber);
		void Broadcast(Channel channel);
	}
}
