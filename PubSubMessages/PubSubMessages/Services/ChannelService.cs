using PubSubMessages.Entities;
using PubSubMessages.Interfaces;

namespace PubSubMessages.Services
{
	public class ChannelService : IChannelService
	{
		public void Broadcast(Channel channel)
		{
			while (channel.Messages.Count != 0)
			{
				var message = channel.Messages.Dequeue();
				foreach (var s in channel.Subscribers)
				{
					s.PendingMessages.Enqueue(message);
				}
			}
		}

		public void SubscribeChannel(Channel channel, Subscriber subscriber)
		{
			if (!channel.Subscribers.Contains(subscriber))
				channel.Subscribers.Add(subscriber);
		}

		public void UnsubscribeChannel(Channel channel, Subscriber subscriber)
		{
			channel.Subscribers.Remove(subscriber);
		}
	}
}
