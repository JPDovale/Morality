namespace Morality.Core.Shared.Entities;

class ValueObject<T>
{
  private T _value;

  protected ValueObject(T value)
  {
    this._value = value;
  }

  protected T value
  {
    get { return _value; }
    set { _value = value; }
  }
}