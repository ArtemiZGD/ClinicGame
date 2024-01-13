public class Message
{
	// Инициализирует новый экземпляр класса Message.
	public Message(string text, Sender sender, ButtonType buttonType = ButtonType.Null)
	{
		Text = text;
		Sender = sender;
		ButtonTypeToActive = buttonType;
	}

	// Текст сообщения.
	public string Text;

	// Отправитель сообщения (доктор, пациент).
	public Sender Sender;

	// Тип кнопки, которую следует активировать (по умолчанию - ButtonType.Null).
	public ButtonType ButtonTypeToActive;
}
