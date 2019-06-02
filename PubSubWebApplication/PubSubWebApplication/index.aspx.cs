using Microsoft.Extensions.DependencyInjection;
using PubSubMessages.Entities;
using PubSubMessages.Interfaces;
using PubSubMessages.Models;
using PubSubMessages.Services;
using System;
using System.Text;
using System.Web.UI.WebControls;

namespace PubSubWebApplication
{
	public partial class index : System.Web.UI.Page
	{
		IPublisherService publisherService;
		IChannelService channelService;
		ISubscriberService subscriberService;

		protected void Page_Load(object sender, EventArgs e)
		{
			var serviceProvider = new ServiceCollection()
				.AddSingleton<IPublisherService, PublisherService>()
				.AddSingleton<IChannelService, ChannelService>()
				.AddSingleton<ISubscriberService, SubscriberService>()
				.BuildServiceProvider();

			publisherService = serviceProvider.GetService<IPublisherService>();
			channelService = serviceProvider.GetService<IChannelService>();
			subscriberService = serviceProvider.GetService<ISubscriberService>();

			var channelCentre = new Channel();
			var publisher = new Publisher("Publisher");
			for (int i = 0; i < 5; i++)
			{
				channelService.SubscribeChannel(channelCentre, new Subscriber());
			}

			publisherService.Post(publisher, new Message("Update from Publisher"), channelCentre);
			channelService.Broadcast(channelCentre);

			int subno = 0;
			StringBuilder html = new StringBuilder();
			foreach (var subscriber in channelCentre.Subscribers)
			{
				subno += 1;
				RetrieveMessages(html, subscriber, subno);
			}
			SubscriberMessage.Controls.Add(new Literal { Text = html.ToString() });
		}

		private void RetrieveMessages(StringBuilder html, Subscriber subscriber, int subscriberNumber)
		{
			html.Append("<table border = '1'>");

			html.Append("<tr>");
			foreach (var m in subscriberService.RetrieveMessages(subscriber))
			{
				html.Append("<td>");
				html.Append($"Subscriber {subscriberNumber}");
				html.Append(m.MessageBroadcast);
				html.Append("</td>");
			}
			html.Append("</tr>");
			html.Append("</table>");
		}
	}
}