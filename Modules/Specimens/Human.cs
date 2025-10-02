using Morality.Core.Shared.Entities;

class HumanProps : SpecimenProps
{
  // You can add Human-specific properties here in the future
}

class CreateHumanProps : CreateSpecimenProps
{
  // You can add Human-specific properties here in the future
}

class Human : Specimen
{
  protected Human(HumanProps props, Simulation simulation, UniqueId? id = null) : base(props, simulation, id)
  {
    Show(this);
  }

  static public Human Create(CreateHumanProps props, Simulation simulation, UniqueId? id = null)
  {
    Race humanRace = simulation.GetRaceByName("Human");

    if (humanRace == null)
    {
      throw new Exception("Human race not found in the simulation. Cannot create Human specimen.");
    }

    double variationFactor = 0.05; // 05% variation
    Random rand = new Random();
    props.health = (int)(props.health * (1 + (rand.NextDouble() * 2 - 1) * variationFactor));
    props.intelligence = (int)(props.intelligence * (1 + (rand.NextDouble() * 2 - 1) * variationFactor));
    props.strength = (int)(props.strength * (1 + (rand.NextDouble() * 2 - 1) * variationFactor));
    props.dexterity = (int)(props.dexterity * (1 + (rand.NextDouble() * 2 - 1) * variationFactor));
    props.constitution = (int)(props.constitution * (1 + (rand.NextDouble() * 2 - 1) * variationFactor));
    props.length = (int)(props.length * (1 + (rand.NextDouble() * 2 - 1) * variationFactor));
    props.weight = (int)(props.weight * (1 + (rand.NextDouble() * 2 - 1) * variationFactor));
    props.lifetime = (int)(props.lifetime * (1 + (rand.NextDouble() * 2 - 1) * variationFactor));

    Human newHuman = new Human(new HumanProps
    {
      spawned_at_age = props.spawned_at_age,
      age = props.age,
      gender = props.gender,
      name = props.name,
      health = props.health,
      intelligence = props.intelligence,
      strength = props.strength,
      dexterity = props.dexterity,
      constitution = props.constitution,
      length = props.length,
      weight = props.weight,
      lifetime = props.lifetime
    }, simulation, id);

    humanRace.AddSpecimen(newHuman);

    return newHuman;
  }

  static public void Show(Human human)
  {
    Console.WriteLine($"Human {human.props.name} created with:");
    Console.WriteLine($"    - Age              {human.props.age} days");
    Console.WriteLine($"    - Health           {human.props.health}");
    Console.WriteLine($"    - Intelligence     {human.props.intelligence}");
    Console.WriteLine($"    - Strength         {human.props.strength}");
    Console.WriteLine($"    - Dexterity        {human.props.dexterity}");
    Console.WriteLine($"    - Constitution     {human.props.constitution}");
    Console.WriteLine($"    - Length           {human.props.length}");
    Console.WriteLine($"    - Weight           {human.props.weight}");
    Console.WriteLine($"    - Lifetime         {human.props.lifetime} days");
    Console.WriteLine($"    - Spawned At       {human.props.spawned_at_age} days");

    if (human.props.gender == 0)
    {
      Console.WriteLine($"    - Gender Male      (0)");
    }

    if (human.props.gender == 1)
    {
      Console.WriteLine($"    - Gender Female    (1)");
    }

    Console.WriteLine("--------------------------------------------------");
  }
  public override void Update() { }

  public override void Render() { }

  public double age
  {
    get { return this.props.age; }
    set { this.props.age = value; }
  }

  public double spawned_at_age
  {
    get { return this.props.spawned_at_age; }
  }

  public string name { get { return this.props.name; } }

  public int lifetime { get { return (int)this.props.lifetime; } }
}