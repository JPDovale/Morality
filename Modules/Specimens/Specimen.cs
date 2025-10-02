

using Morality.Core.Shared.Entities;

class SpecimenProps
{
  public required string name;
  public required int gender;
  public required double health;
  public required double intelligence;
  public required double strength;
  public required double dexterity;
  public required double constitution;
  public required double length;
  public required double weight;
  public required double lifetime;
  public required double age;
  public required double spawned_at_age;

}

class CreateSpecimenProps
{
  public required string name;
  public required int gender;
  public required double health;
  public required double intelligence;
  public required double strength;
  public required double dexterity;
  public required double constitution;
  public required double length;
  public required double weight;
  public required double lifetime;
  public required double spawned_at_age;
  public double age = 0;

}

enum SpecimenGender
{
  MALE = 0,
  FEMALE = 1,
}

class Specimen : LogicalEntity<SpecimenProps>
{
  protected Specimen(SpecimenProps props, Simulation simulation, UniqueId? id = null) : base(props, simulation, id) { }

  static public Specimen Create(CreateSpecimenProps props, Simulation simulation, UniqueId? id = null)
  {
    return new Specimen(new SpecimenProps
    {
      spawned_at_age = props.spawned_at_age,
      gender = props.gender,
      name = props.name,
      health = props.health,
      intelligence = props.intelligence,
      strength = props.strength,
      dexterity = props.dexterity,
      constitution = props.constitution,
      length = props.length,
      weight = props.weight,
      age = props.age,
      lifetime = props.lifetime
    }, simulation, id);
  }

  public override void Update() { }

  public override void Render() { }


}