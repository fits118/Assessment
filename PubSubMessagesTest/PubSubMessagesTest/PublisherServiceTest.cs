using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PubSubMessages.Entities;
using PubSubMessages.Interfaces;
using PubSubMessages.Models;
using PubSubMessages.Services;

namespace PubSubMessagesTest
{
	/// <summary>
	/// Summary description for PublisherServiceTest
	/// </summary>
	[TestClass]
	public class PublisherServiceTest
	{
		public PublisherServiceTest()
		{
		}

		[TestMethod]
		public void PostTest()
		{
			var serviceProvider = new ServiceCollection()
				.AddSingleton<IPublisherService, PublisherService>()
				.AddSingleton<IChannelService, ChannelService>()
				.AddSingleton<ISubscriberService, SubscriberService>()
				.BuildServiceProvider();

			var publisherService = serviceProvider.GetService<IPublisherService>();

			var testPub = new Publisher("testPub");
			var testMessage = new Message("testMsg");
			var testChannel = new Channel();

			Assert.AreEqual(testChannel.Messages.Count, 0);
			publisherService.Post(testPub, testMessage, testChannel);
			Assert.AreEqual(1, testChannel.Messages.Count);
			publisherService.Post(testPub, testMessage, testChannel);
			Assert.AreEqual(2, testChannel.Messages.Count);
			Assert.AreEqual("testPub", testChannel.Messages.Peek().From);
			Assert.AreEqual("testMsg", testChannel.Messages.Peek().Body);
		}
	}
}
