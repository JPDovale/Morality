using Morality.Core.Shared.Entities;

class RaceProps
{
  public required List<Specimen> specimens = new List<Specimen>();
  public required string name;
  public required string description;
  public required int base_health;
  public required int base_intelligence;
  public required int base_strength;
  public required int base_dexterity;
  public required int base_constitution;
  public required int base_length;
  public required int base_weight;
  public required int base_lifetime;
}

class CreateRaceProps
{
  public required string name;
  public required string description;
  public required int base_health;
  public required int base_intelligence;
  public required int base_strength;
  public required int base_dexterity;
  public required int base_constitution;
  public required int base_length;
  public required int base_weight;
  public required int base_lifetime;
}

class Race : LogicalEntity<RaceProps>
{
  protected Race(RaceProps props, Simulation simulation, UniqueId? id = null) : base(props, simulation, id)
  {
    Console.WriteLine($"New race spawned: {props.name}");
    Console.WriteLine($"Description: {props.description}");
  }

  static public Race Create(CreateRaceProps props, Simulation simulation, UniqueId? id = null)
  {
    return new Race(new RaceProps
    {
      name = props.name,
      description = props.description,
      base_health = props.base_health,
      base_intelligence = props.base_intelligence,
      base_strength = props.base_strength,
      base_dexterity = props.base_dexterity,
      base_constitution = props.base_constitution,
      base_length = props.base_length,
      base_weight = props.base_weight,
      base_lifetime = props.base_lifetime,
      specimens = new List<Specimen>()
    }, simulation, id);
  }

  public override void Update() { }

  public override void Render() { }

  public string name { get { return this.props.name; } }

  public string description { get { return this.props.description; } }
  public int base_health { get { return this.props.base_health; } }
  public int base_intelligence { get { return this.props.base_intelligence; } }
  public int base_strength { get { return this.props.base_strength; } }
  public int base_dexterity { get { return this.props.base_dexterity; } }
  public int base_constitution { get { return this.props.base_constitution; } }
  public int base_length { get { return this.props.base_length; } }
  public int base_weight { get { return this.props.base_weight; } }
  public int base_lifetime { get { return this.props.base_lifetime; } }

  public void AddSpecimen(Specimen specimen)
  {
    this.props.specimens.Add(specimen);
  }

  public void RemoveSpecimen(Specimen specimen)
  {
    this.props.specimens.Remove(specimen);
  }

  public int specimensCount
  {
    get { return this.props.specimens.Count(); }
  }

  public List<Specimen> specimens
  {
    get { return this.props.specimens; }
  }
}