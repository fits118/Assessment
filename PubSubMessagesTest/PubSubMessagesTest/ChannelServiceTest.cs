using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PubSubMessages.Entities;
using PubSubMessages.Interfaces;
using PubSubMessages.Models;
using PubSubMessages.Services;

namespace PubSubMessagesTest
{
	/// <summary>
	/// Summary description for ChannelServiceTest
	/// </summary>
	[TestClass]
	public class ChannelServiceTest
	{
		public ChannelServiceTest()
		{
		}

		[TestMethod]
		public void SubscribeChannelTest()
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
			var testSub2 = new Subscriber();

			Assert.AreEqual(0,testChannel.Subscribers.Count);
			channelService.SubscribeChannel(testChannel, testSub);
			Assert.AreEqual(1, testChannel.Subscribers.Count);
			channelService.SubscribeChannel(testChannel, testSub);
			channelService.SubscribeChannel(testChannel, testSub);
			Assert.AreEqual(1, testChannel.Subscribers.Count);
			channelService.SubscribeChannel(testChannel, testSub2);
			Assert.AreEqual(2, testChannel.Subscribers.Count);
		}

		[TestMethod]
		public void UnsubscribeChannelTest()
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
			var testSub2 = new Subscriber();

			Assert.AreEqual(0,testChannel.Subscribers.Count);
			channelService.UnsubscribeChannel(testChannel, testSub);
			Assert.AreEqual(0, testChannel.Subscribers.Count);
			channelService.SubscribeChannel(testChannel, testSub);
			channelService.UnsubscribeChannel(testChannel, testSub);
			Assert.AreEqual(0, testChannel.Subscribers.Count);
			channelService.SubscribeChannel(testChannel, testSub);
			channelService.SubscribeChannel(testChannel, testSub2);
			channelService.UnsubscribeChannel(testChannel, testSub);
			Assert.IsNotNull(testChannel.Subscribers);
			Assert.IsTrue(testChannel.Subscribers.Contains(testSub2));
			Assert.IsFalse(testChannel.Subscribers.Contains(testSub));
		}

		[TestMethod]
		public void Broadcast()
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

			channelService.SubscribeChannel(testChannel, testSub);
			publisherService.Post(testPub, testMessage, testChannel);
			Assert.AreEqual(1, testChannel.Messages.Count);
			channelService.Broadcast(testChannel);
			Assert.AreEqual(0, testChannel.Messages.Count);
			publisherService.Post(testPub, testMessage, testChannel);
			publisherService.Post(testPub, testMessage, testChannel);
			channelService.Broadcast(testChannel);
			Assert.AreEqual(0, testChannel.Messages.Count);
		}
	}
}
