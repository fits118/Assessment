using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PubSubMessages.Entities;
using PubSubMessages.Interfaces;
using PubSubMessages.Models;
using PubSubMessages.Services;

namespace PubSubMessagesTest
{
	/// <summary>
	/// Summary description for SubscriberServiceTest
	/// </summary>
	[TestClass]
	public class SubscriberServiceTest
	{
		public SubscriberServiceTest()
		{
		}

		[TestMethod]
		public void RetrieveMessagesTest()
		{
			var serviceProvider = new ServiceCollection()
				.AddSingleton<IPublisherService, PublisherService>()
				.AddSingleton<IChannelService, ChannelService>()
				.AddSingleton<ISubscriberService, SubscriberService>()
				.BuildServiceProvider();

			var publisherService = serviceProvider.GetService<IPublisherService>();
			var channelService = serviceProvider.GetService<IChannelService>();
			var subscriberService = serviceProvider.GetService<ISubscriberService>();

			var testPub = new Publisher("testPub");
			var testMessage = new Message("testMsg");
			var testChannel = new Channel();
			var testSub = new Subscriber();

			Assert.AreEqual(0, subscriberService.RetrieveMessages(testSub).Count);
			channelService.SubscribeChannel(testChannel, testSub);
			Assert.AreEqual(0, subscriberService.RetrieveMessages(testSub).Count);
			publisherService.Post(testPub, testMessage, testChannel);
			publisherService.Post(testPub, testMessage, testChannel);
			Assert.AreEqual(0, subscriberService.RetrieveMessages(testSub).Count);
			channelService.Broadcast(testChannel);
			Assert.AreEqual(2, subscriberService.RetrieveMessages(testSub).Count);
		}
	}
}
