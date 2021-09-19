using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Game Event/Game Event")] // 1 Добавляет GameEvent в качестве ассета в Asset Menu.
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>(); // 3 Список GameEventListeners, которые будут подписаны на GameEvent.
    public void Raise() // 4 Метод для вызова всех подписчиков GameEvent.
    {
        for (int i = listeners.Count - 1; i >= 0; i--) // 5 Последний подписанный GameEventListener будет первым вызываемым (последним пришёл, первым вышел).
        {
            listeners[i].OnEventRaised(); // 6 Вызов каждого UnityEvent GameEventListeners.
        }
    }

    public void RegisterListener(GameEventListener listener) // 7 Метод, позволяющий GameEventListeners подписаться на этот GameEvent.
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener) // 8 Метод, позволяющий GameEventListeners отписываться от этого GameEvent.
    {
        listeners.Remove(listener);
    }
}
