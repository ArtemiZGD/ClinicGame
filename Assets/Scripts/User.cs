using System;
using UnityEngine;

public class User
{
	public UserType UserType { get; set; }
	public string Name { get; set; }
	public DateTime BirthDate { get; set; }
	public Gender Gender { get; set; }
	public Sprite Avatar { get; set; }

	public User(UserType userType, string name, DateTime birthDate, Gender gender, Sprite avatar)
	{
		UserType = userType;
		Name = name;
		BirthDate = birthDate;
		Gender = gender;
		Avatar = avatar;
	}
}
