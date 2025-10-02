using Morality.Core.Game;

namespace Morality;

enum SimulationWorksIn
{
  MINUTES = 0,
  HOURS = 1,
  DAYS = 2,
  MONTHS = 3,
  YEARS = 4
}

class Program
{
  static void Main()
  {
    int SIMULATION_WORKS_IN = (int)SimulationWorksIn.YEARS;
    int LENGTH_OF_DAY = 24; // hours
    int LENGTH_OF_MONTH = 30; // days
    int LENGTH_OF_YEAR = 12; // days

    int WORLD_START_AGE = 1000;
    int COUNT_OF_START_HUMANS = 10;

    int SIMULATION_SPEED = 1;
    int FRAMES_PER_SECOND = 60;

    Simulation simulation = Simulation.Create(new CreateSimulationProps
    {
      COUNT_OF_START_HUMANS = COUNT_OF_START_HUMANS,
      WORLD_START_AGE = WORLD_START_AGE,
      LENGTH_OF_DAY = LENGTH_OF_DAY,
      LENGTH_OF_MONTH = LENGTH_OF_MONTH,
      LENGTH_OF_YEAR = LENGTH_OF_YEAR
    });

    Age simulationAge = Age.Create(new CreateAgeProps
    {
      years = 1,
      months = 1,
      days = 1,
      hours = 0,
      minutes = 0,
      simulation_speed = SIMULATION_SPEED,
      simulation_works_in = SIMULATION_WORKS_IN
    }, simulation);

    simulation.age = simulationAge;
    simulation.Register(SimulationAgeChangeObserver.observer);

    Game game = Game.Create(new CreateGameProps { frames_per_second = FRAMES_PER_SECOND, simulation = simulation });

    game.Start();
  }
}