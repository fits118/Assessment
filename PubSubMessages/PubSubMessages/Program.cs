using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubSubMessages.Interfaces;
using PubSubMessages.Services;
using PubSubMessages.Entities;
using PubSubMessages.Models;

namespace PubSubMessages
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				//inject dependency
				var serviceProvider = new ServiceCollection()
					.AddSingleton<IPublisherService, PublisherService>()
					.AddSingleton<IChannelService, ChannelService>()
					.AddSingleton<ISubscriberService, SubscriberService>()
					.BuildServiceProvider();

				var publisherService = serviceProvider.GetService<IPublisherService>();
				var channelService = serviceProvider.GetService<IChannelService>();
				var subscriberService = serviceProvider.GetService<ISubscriberService>();

				//declare to-be-used variable
				var channelCentre = new Channel();
				var ship = new Publisher("GoBig Ship");
				var australiaNorth = new Subscriber();
				var australiaSouth = new Subscriber();

				//subscribe channel, post message and then broadcast
				channelService.SubscribeChannel(channelCentre, australiaNorth);
				channelService.SubscribeChannel(channelCentre, australiaSouth);
				publisherService.Post(ship, new Message("Arriving north point in 30minutes"), channelCentre);
				publisherService.Post(ship, new Message("Arrived north point"), channelCentre);
				channelService.Broadcast(channelCentre);

				//round 1 result
				Console.WriteLine("round 1 result - australiaNorth pending message count: {0}", australiaNorth.PendingMessages.Count);
				Console.WriteLine("round 1 result - australiaNorth's log:");
				foreach (var m in subscriberService.RetrieveMessages(australiaNorth))
				{
					Console.WriteLine(m.MessageBroadcast);
				}
				Console.WriteLine("round 1 result - australiaSouth pending message count: {0}", australiaSouth.PendingMessages.Count);
				Console.WriteLine("round 1 result - australiaSouth's log:");
				foreach (var m in subscriberService.RetrieveMessages(australiaSouth))
				{
					Console.WriteLine(m.MessageBroadcast);
				}
				Console.WriteLine();

				//unsubscribe one channel and reiterate
				channelService.UnsubscribeChannel(channelCentre, australiaNorth);
				publisherService.Post(ship, new Message("Departed to New Zealand"), channelCentre);
				channelService.Broadcast(channelCentre);

				//round 2 result
				Console.WriteLine("round 2 result - australiaNorth pending message count: {0}", australiaNorth.PendingMessages.Count);
				Console.WriteLine("round 2 result - australiaNorth's log:");
				foreach (var m in subscriberService.RetrieveMessages(australiaNorth))
				{
					Console.WriteLine(m.MessageBroadcast);
				}
				Console.WriteLine("round 2 result - australiaSouth pending message count: {0}", australiaSouth.PendingMessages.Count);
				Console.WriteLine("round 2 result - australiaSouth's log:");
				foreach (var m in subscriberService.RetrieveMessages(australiaSouth))
				{
					Console.WriteLine(m.MessageBroadcast);
				}

				Console.Read();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
