using Morality.Core.Shared.Entities;

class SimulationProps
{
  public required List<Race> races = new List<Race>();
  public Age? age = null;
  public required int WORLD_START_AGE;
  public required int COUNT_OF_START_HUMANS;
  public required int LENGTH_OF_DAY;
  public required int LENGTH_OF_MONTH;
  public required int LENGTH_OF_YEAR;
}

class CreateSimulationProps
{
  public Age? age = null;
  public int WORLD_START_AGE = 0;
  public int COUNT_OF_START_HUMANS = 10;
  public int LENGTH_OF_DAY = 24;
  public int LENGTH_OF_MONTH = 30;
  public int LENGTH_OF_YEAR = 365;
}

class Simulation : Entity<SimulationProps>
{
  protected Simulation(SimulationProps props, UniqueId? id = null) : base(props, id) { }

  static public Simulation Create(CreateSimulationProps props, UniqueId? id = null)
  {
    return new Simulation(new SimulationProps
    {
      age = props.age,
      races = new List<Race>(),
      COUNT_OF_START_HUMANS = props.COUNT_OF_START_HUMANS,
      LENGTH_OF_DAY = props.LENGTH_OF_DAY,
      LENGTH_OF_MONTH = props.LENGTH_OF_MONTH,
      LENGTH_OF_YEAR = props.LENGTH_OF_YEAR,
      WORLD_START_AGE = props.WORLD_START_AGE
    }, id);
  }

  public int WORLD_START_AGE { get { return this.props.WORLD_START_AGE; } }
  public int COUNT_OF_START_HUMANS { get { return this.props.COUNT_OF_START_HUMANS; } }
  public int LENGTH_OF_DAY { get { return this.props.LENGTH_OF_DAY; } }
  public int LENGTH_OF_MONTH { get { return this.props.LENGTH_OF_MONTH; } }
  public int LENGTH_OF_YEAR { get { return this.props.LENGTH_OF_YEAR; } }

  public void Update() { }

  public void Render() { }

  public List<Race> races { get { return this.props.races; } }

  public void AddRace(Race race)
  {
    this.props.races.Add(race);
  }

  public void RemoveObject(Race race)
  {
    this.props.races.Remove(race);
  }

  public bool HasRace(Race race)
  {
    return this.props.races.Contains(race);
  }

  public bool HasRaceByName(string name)
  {
    return this.props.races.Any(race => race.name == name);
  }

  public Race GetRaceByName(string name)
  {
    return this.props.races.First(r => r.name == name);
  }

  public void Register<U>(Observer<U> observer)
  {
    if (this.props.age == null)
    {
      throw new Exception("Simulation has no Age component to register the observer.");
    }

    this.props.age.Register(observer);
  }

  public Age age
  {
    get
    {
      if (this.props.age == null)
      {
        throw new Exception("Simulation has no Age component.");
      }
      return this.props.age;
    }

    set
    {
      this.props.age = value;
    }
  }
}