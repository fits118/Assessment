namespace PubSubMessages.Models
{
	public class Message
	{
		public Message(string content)
		{
			this.Body = content;
		}

		public string Body { get; set; }
		public string From { get; set; }
		public string MessageBroadcast
		{
			get
			{
				return string.Format("[{0}]: {1}", From, Body.Trim());
			}
		}

	}
}
