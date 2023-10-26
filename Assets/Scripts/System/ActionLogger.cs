using System;
using System.Collections.Generic;
using UnityEngine;

public enum ELogType
{
    Movement
}

public enum EMessageType
{
    Log,
    Warning,
    Error
}

public class ActionLogger : MonoBehaviour
{
    [SerializeField] private List<LogType> logs = new();
    
    public void Log(EMessageType messageType, ELogType logType, string message)
    {
        foreach (var log in logs) 
        {
            if (log.logType == logType)
            {
                if (!log.value)
                {
                    return;
                }
                switch (messageType)
                {
                    case EMessageType.Log:
                        Debug.Log(message);
                        break;
                    case EMessageType.Warning:
                        Debug.LogWarning(message);
                        break;
                    case EMessageType.Error:
                        Debug.LogError(message);
                        break;
                }
            }
        }
    }

    [Serializable]
    public class LogType
    {
        public ELogType logType;
        public bool value;
    }
}