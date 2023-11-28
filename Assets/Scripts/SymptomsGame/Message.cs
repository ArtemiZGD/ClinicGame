public class Message
{
	public Message(string text, Sender sender, ButtonType buttonType = ButtonType.Null)
	{
		Text = text;
		Sender = sender;
		ButtonTypeToActive = buttonType;
	}

	public string Text;
	public Sender Sender;
	public ButtonType ButtonTypeToActive;
}
