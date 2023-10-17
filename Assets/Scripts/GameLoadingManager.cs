using System;
using System.Collections;
using InventorySystem;
using UnityEngine;
using Zenject;

public interface IInitialize<T>
{
    public void Initialize();
}

public class GameLoadingManager : MonoBehaviour
{
}