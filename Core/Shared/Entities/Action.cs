using Morality.Core.Shared.Entities;

class Event<T> : Entity<T>
{
  protected Event(T props, UniqueId? id = null) : base(props, id) { }

  static public new Event<T> Create(T props, UniqueId? id = null)
  {
    return new Event<T>(props, id);
  }

  public T data { get { return this.props; } }

}