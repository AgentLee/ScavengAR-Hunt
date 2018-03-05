using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats 
{
	private static int score;
	private static string name;
	private static string email;
	private static string phone;

	public static int Score
	{
		get { return score; }
		set { score = value; }
	}

	public static string Name
	{
		get { return name; }
		set { name = value; }
	}

	public static string Email
	{
		get { return email; }
		set { email = value; }
	}

	public static string Phone
	{
		get { return phone; }
		set { phone = value; }
	}
}
