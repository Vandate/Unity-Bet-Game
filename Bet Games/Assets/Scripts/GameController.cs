using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	private int playMoney;
	private int betNumber;
	private int actualNumber;
	private int betMoney;

	[SerializeField]
	private InputField balanceInputField;
	[SerializeField]
	private Text balanceText;
	[SerializeField]
	private InputField numberInputField;
	[SerializeField]
	private Text text;
	[SerializeField]
	private InputField moneyInputField;
	[SerializeField]
	private Button startButton;
	[SerializeField]
	private Button stopButton;
	[SerializeField]
	private GameObject playAgainButton;
	[SerializeField]
	private GameObject gameOverText;
	[SerializeField]
	private GameObject resetButton;

	private void Start()
	{
		balanceInputField.contentType = InputField.ContentType.IntegerNumber;
		numberInputField.contentType = InputField.ContentType.IntegerNumber;
		moneyInputField.contentType = InputField.ContentType.IntegerNumber;
		startButton.interactable = false;
		stopButton.interactable = false;
	}

	public void SetBalance(string money)
	{
		BalanceCheck(int.Parse(money));
		//balanceInputField.text = "";
		balanceInputField.enabled = false;
	}

	void BalanceCheck(int money)
	{
		if (money <= 0)
		{
			playMoney = 0;
			balanceText.text = playMoney.ToString();
		}
		else
		{
			playMoney = money;
			balanceText.text = playMoney.ToString();
		}
	}

	public void getInput(string guess)
	{
		betNumber = int.Parse(guess);
		numberInputField.text = betNumber.ToString();
		//numberInputField.text = "";
	}

	public void SetAmount(string money)
	{
		MoneyCheck(int.Parse(money));
	}

	public void MoneyCheck(int money)
	{
		if (money > playMoney)
		{
			moneyInputField.text = playMoney.ToString();
			betMoney = playMoney;
		}
		else if (money <= 0)
		{
			moneyInputField.text = "0";
			betMoney = 0;
		}
		else
		{
			moneyInputField.text = money.ToString();
			betMoney = money;
		}
		if (playMoney >= 0 && betNumber >= 0 && betMoney >= 0)
		{
			startButton.interactable = true;
		}
	}

	public void NumberGenerator()
	{
		numberInputField.enabled = false;
		moneyInputField.enabled = false;
		actualNumber = Random.Range(0, 100);
		Debug.Log(actualNumber);
		stopButton.interactable = true;
		startButton.interactable = false;
	}

	public void CompareGuesses()
	{
		if (actualNumber == betNumber)
		{
			if (playMoney + (betMoney * 70) < 2147483647 && playMoney + (betMoney * 70) > 0)
			{
				text.text = "You Guess Correctly. You win " + betMoney * 70 + "$";
				playMoney += betMoney * 70;
			}
			else
			{
				text.text = "The Result is " + (actualNumber + 1) + ". You lost " + betMoney + "$";
				playMoney -= betMoney;
			}
		}
		else if (actualNumber < betNumber)
		{
			text.text = "The Result is " + actualNumber + ". You lost " + betMoney + "$";
			playMoney -= betMoney;
		}
		else if (actualNumber > betNumber)
		{
			text.text = "The Result is " + actualNumber + ". You lost " + betMoney + "$";
			playMoney -= betMoney;
		}
		balanceText.text = playMoney.ToString();
		playAgainButton.SetActive(true);
		stopButton.interactable = false;
	}

	public void PlayAgain()
	{
		if (playMoney <= 0)
		{
			resetButton.SetActive(true);
			gameOverText.SetActive(true);
		}
		text.text = "Enter Your Bet Number(0-99) Here:";
		numberInputField.text = "";
		numberInputField.enabled = true;
		moneyInputField.text = "";
		moneyInputField.enabled = true;
		playAgainButton.SetActive(false);
	}

	public void Reset()
	{
		resetButton.SetActive(false);
		gameOverText.SetActive(false);
		balanceInputField.enabled = true;
		balanceInputField.text = "";
		numberInputField.enabled = true;
		moneyInputField.enabled = true;
	}
}