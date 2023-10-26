using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DevConsole : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text consoleText; // Ссылка на Text компонент для вывода команд и сообщений
    public ItemStorage itemStorage; // Подразумевается, что у вас есть класс для хранения предметов

    private List<ConsoleMessage> consoleMessages = new List<ConsoleMessage>();

    private void Start()
    {
        inputField.onEndEdit.AddListener(ProcessInput); // Привязываем метод ProcessInput к событию окончания ввода
    }

    private void ProcessInput(string input)
    {
        string[] inputParts = input.Split(' ');

        if (inputParts.Length < 3 || inputParts[0] != "add" || inputParts[1] != "item")
        {
            string errorMessage = "Ошибка: Неправильный формат команды. Используйте 'add item [id]'.";
            DisplayMessage(errorMessage, Color.red);
            return;
        }

        string itemId = inputParts[2];
        ItemDescription itemDescription = itemStorage.GetItemDescriptionById(itemId);

        if (itemDescription != null)
        {
            // Если предмет существует, добавляем его в инвентарь
            // inventory.AddItem(itemDescription);
            string successMessage = "Предмет '" + itemId + "' добавлен в инвентарь.";
            DisplayMessage(successMessage, Color.white);
        }
        else
        {
            string errorMessage = "Ошибка: Предмет с id '" + itemId + "' не найден.";
            DisplayMessage(errorMessage, Color.red);
        }

        inputField.text = ""; // Очищаем поле ввода
    }

    private void DisplayMessage(string message, Color color)
    {
        ConsoleMessage consoleMessage = new ConsoleMessage(message, color);
        consoleMessages.Insert(0, consoleMessage);

        consoleText.text = string.Empty;
        
        for (int i = consoleMessages.Count - 1; i >= 0; i--)
        {
            consoleText.text += $"<color=#{ColorUtility.ToHtmlStringRGB(consoleMessages[i].Color)}>{consoleMessages[i].Message}</color>\n";
        }
    }
}

public class ConsoleMessage
{
    public string Message { get; set; }
    public Color Color { get; set; }

    public ConsoleMessage(string message, Color color)
    {
        Message = message;
        Color = color;
    }
}

