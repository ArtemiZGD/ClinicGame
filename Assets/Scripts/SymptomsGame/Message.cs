public class Message
{
	// �������������� ����� ��������� ������ Message.
	public Message(string text, Sender sender, ButtonType buttonType = ButtonType.Null)
	{
		Text = text;
		Sender = sender;
		ButtonTypeToActive = buttonType;
	}

	// ����� ���������.
	public string Text;

	// ����������� ��������� (������, �������).
	public Sender Sender;

	// ��� ������, ������� ������� ������������ (�� ��������� - ButtonType.Null).
	public ButtonType ButtonTypeToActive;
}
