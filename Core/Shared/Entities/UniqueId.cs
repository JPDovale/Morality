namespace Morality.Core.Shared.Entities;

class UniqueId : ValueObject<String>
{
  protected UniqueId(String uniqueId) : base(uniqueId) { }

  static public UniqueId Create(String? uniqueId = null)
  {
    return new UniqueId(uniqueId ?? Guid.NewGuid().ToString());
  }

  public override string ToString()
  {
    return base.value.ToString();
  }
}