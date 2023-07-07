using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
	static string[] rainbowColors = {
		"red",
		"orange",
		"yellow",
		"blue",
		"#4B0082",
		"#EE82EE"
	};

	public static string Rainbowize(string dullsville) {
		var lessDullsville = "";
		var color = 0;

		foreach (var dullChar in dullsville) {
			if (char.IsWhiteSpace(dullChar)) {
				lessDullsville += dullChar;
				continue;
			}

			lessDullsville += $"<color={rainbowColors[color]}>{dullChar}</color>";
			if (color < rainbowColors.Length - 1) {
				color++;
			} else {
				color = 0;
			}
		}

		return lessDullsville;
	}
}
