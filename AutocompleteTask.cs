using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using NUnit.Framework;

namespace Autocomplete;

internal class AutocompleteTask
{
	/// <returns>
	/// Возвращает первую фразу словаря, начинающуюся с prefix.
	/// </returns>
	/// <remarks>
	/// Эта функция уже реализована, она заработает, 
	/// как только вы выполните задачу в файле LeftBorderTask
	/// </remarks>
	public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
			return phrases[index];
            
		return null;
	}

	/// <returns>
	/// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
	/// элементов словаря, начинающихся с prefix.
	/// </returns>
	/// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
	public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
	{
		var phrasesCount = phrases.Count;
		var leftBorder=LeftBorderTask.GetLeftBorderIndex(phrases,prefix,-1, phrasesCount)+1;
		if (leftBorder == phrasesCount)
			return new string[0];
		var actualCount =Math.Min(count, phrasesCount-leftBorder);
		var result=new List<string>();
		var nextPhraseIndex = 0;
		for(int i = 0; i < actualCount; i++)
		{
			nextPhraseIndex = leftBorder + i;
			if (!phrases[nextPhraseIndex].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
				break;
			result.Add(phrases[nextPhraseIndex]);
		}
		// тут стоит использовать написанный ранее класс LeftBorderTask
		return result.ToArray();
	}

	/// <returns>
	/// Возвращает количество фраз, начинающихся с заданного префикса
	/// </returns>
	public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
		var right=RightBorderTask.GetRightBorderIndex(phrases,prefix,-1,phrases.Count);
		var result = right - left - 1;
		// тут стоит использовать написанные ранее классы LeftBorderTask и RightBorderTask
		return result;
	}
}

[TestFixture]
public class AutocompleteTests
{
	[Test]
	public void TopByPrefix_IsEmpty_WhenNoPhrases()
	{
		var phrases =new List<string>();
		var result = AutocompleteTask.GetTopByPrefix(phrases, "q",0);
		CollectionAssert.IsEmpty(result);
	}

	// ...

	[Test]
	public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
	{
		// ...
		//Assert.AreEqual(expectedCount, actualCount);
	}

	// ...
}