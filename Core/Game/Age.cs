using System.Timers;
using Morality;
using Morality.Core.Shared.Entities;
using Timer = System.Timers.Timer;

class AgeProps
{

  public required int simulation_works_in = (int)SimulationWorksIn.MINUTES;
  public required int minutes;
  public required int hours;
  public required int days;
  public required int months;
  public required int years = 365;
  public required Timer timer;
}

class CreateAgeProps
{
  public required int simulation_speed;
  public required int simulation_works_in = (int)SimulationWorksIn.MINUTES;
  public int minutes = 0;
  public int hours = 0;
  public int days = 1;
  public int months = 0;
  public int years = 0;
}

class Age : LogicalEntity<AgeProps>
{

  protected Age(AgeProps props, Simulation simulation, UniqueId? id = null) : base(props, simulation, id)
  {
    this.props.timer.Elapsed += OnTimedEvent;
    this.props.timer.AutoReset = true;
    this.props.timer.Enabled = true;
    this.props.timer.Start();
  }

  protected void OnTimedEvent(Object? source, ElapsedEventArgs e)
  {
    if (this.props.simulation_works_in == 0)
    {
      this.props.minutes += 1;
    }

    if (this.props.simulation_works_in == 1)
    {
      this.props.hours += 1;
    }

    if (this.props.simulation_works_in == 2)
    {
      this.props.days += 1;
    }

    if (this.props.simulation_works_in == 3)
    {
      this.props.months += 1;
    }

    if (this.props.simulation_works_in == 4)
    {
      this.props.years += 1;
    }

    if (this.props.minutes >= 60)
    {
      this.props.minutes = 1;
      this.props.hours += 1;
    }

    if (this.props.hours > simulation.LENGTH_OF_DAY)
    {
      this.props.hours = 1;
      this.props.days += 1;
    }

    if (this.props.days > simulation.LENGTH_OF_MONTH)
    {
      this.props.days = 1;
      this.props.months += 1;
    }

    if (this.props.months > simulation.LENGTH_OF_YEAR)
    {
      this.props.months = 1;
      this.props.years += 1;
    }

    Console.WriteLine($"[Age] The simulation is now {this.props.days}/{this.props.months}/{this.props.years} {this.props.hours}:{this.props.minutes}");
    Console.WriteLine($"[Day] {this.total_days} days have passed");

    this.Notify<AgeChangeEvent>(Event<AgeChangeEvent>.Create(new AgeChangeEvent
    {
      age = this.total_days,
    }),

    this.simulation
    );
  }

  static public Age Create(CreateAgeProps props, Simulation simulation, UniqueId? id = null)
  {
    return new Age(new AgeProps
    {
      simulation_works_in = props.simulation_works_in,
      minutes = props.minutes,
      hours = props.hours,
      days = props.days,
      months = props.months,
      years = props.years,
      timer = new Timer((1000) / props.simulation_speed),
    }, simulation, id);
  }

  public override void Render() { }

  public override void Update()
  {

  }

  public int age
  {
    get { return this.props.months; }
  }

  public int total_days
  {
    get { return ((this.props.years - 1) * this.simulation.LENGTH_OF_YEAR + this.props.months - 1) * simulation.LENGTH_OF_MONTH + this.props.days; }
  }
}