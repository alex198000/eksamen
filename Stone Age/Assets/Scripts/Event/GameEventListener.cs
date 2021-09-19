using UnityEngine.Events; // 1 Требование использования класса UnityEvent.
using UnityEngine;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent; // 2 GameEvent, на который будет подписан этот GameEventListener.
    [SerializeField] private UnityEvent response; // 3 Отклик UnityEvent, который будет вызываться, при генерации событием GameEvent этого GameEventListener.

    private void OnEnable() // 4 Привязка GameEvent к GameEventListener, когда этот GameObject включен.
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable() // 5 Отвязка GameEvent от GameEventListener, когда этот GameObject отключен.
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised() // 6 Вызывается при генерации GameEvent, приводящей к вызову слушателем GameEventListener события UnityEvent.
    {
        response.Invoke();
    }
}

