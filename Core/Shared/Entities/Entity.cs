namespace Morality.Core.Shared.Entities;

class Entity<T>
{
  private T _props;
  private UniqueId _id;

  protected Entity(T props, UniqueId? id = null)
  {
    this._props = props;
    this._id = id ?? UniqueId.Create();
  }

  static public Entity<T> Create(T props, UniqueId? id = null)
  {
    return new Entity<T>(props, id);
  }

  public UniqueId id
  {
    get { return _id; }
  }

  protected T props
  {
    get { return _props; }
  }
}