using Morality.Core.Shared.Entities;

interface IObserver
{
  Type PayloadType { get; }
  void Notify(object eventObject, Simulation simulation);
}

class Observer<T> : IObserver
{
  public required Action<Event<T>, Simulation> action;

  public Type PayloadType => typeof(T);

  public void Notify(object eventObject, Simulation simulation)
  {
    if (eventObject is Event<T> typedEvent)
    {
      action(typedEvent, simulation);
    }
  }
}

abstract class LogicalEntity<T> : Entity<T>
{
  private readonly List<IObserver> _observers = [];
  private readonly Simulation _simulation;

  protected LogicalEntity(T props, Simulation simulation, UniqueId? id = null) : base(props, id)
  {
    this._simulation = simulation;
  }

  abstract public void Update();
  abstract public void Render();

  public void Register<U>(Observer<U> observer)
  {
    this._observers.Add(observer);
  }

  public void Unregister<U>(Observer<U> observer)
  {
    this._observers.Remove(observer);
  }

  public void Notify<U>(Event<U> eventObject, Simulation simulation)
  {
    foreach (var observer in this._observers)
    {
      if (observer.PayloadType == typeof(U))
      {
        observer.Notify(eventObject, simulation);
      }
    }
  }

  public Simulation simulation { get { return this._simulation; } }
}
