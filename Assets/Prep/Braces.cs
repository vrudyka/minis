using System.Collections.Generic;
using UnityEngine;

public class Braces : MonoBehaviour
{
	string[] testStrings = new string[]
	{
		// Valid Strings
		"()",
		"([])",
		"({[()]})",
		"(([]){})",
		"a + (b * [c - d]) / {e}",
		"{[()([]){}]((()[]){{}})}",
    
		// Invalid Strings
		"(",
		"[}",
		"({[})]",
		"(()",
		"([)]",
		"{[(()])}",
		"{[}",
	};

	private void Awake()
	{
		foreach (var testString in testStrings)
		{
			bool isValid = Validate(testString);

			Debug.Log($"{isValid}: {testString}");
		}
	}

	private bool Validate(string testString)
	{
		var charArray = testString.ToCharArray();
		var openStack = new Stack<char>();

		for (int i = 0; i < charArray.Length; i++)
		{
			if (charArray[i] == '(' || charArray[i] == '{' || charArray[i] == '[')
			{
				openStack.Push(charArray[i]);
			}

			if (charArray[i] == ')' || charArray[i] == '}' || charArray[i] == ']')
			{
				var last = openStack.Pop();
				if (charArray[i] == ')' && last != '(' ||
					charArray[i] == '}' && last != '{' ||
					charArray[i] == ']' && last != '[')
					return false;
			}
		}

		if (openStack.Count != 0)
			return false;

		return true;
	}
}
