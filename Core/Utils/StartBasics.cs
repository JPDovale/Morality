class StartBasics
{
  public static void Setup(Simulation simulation)
  {
    Race humanRace = Race.Create(new CreateRaceProps
    {
      name = "Human",
      description = "Humans are a versatile and ambitious species known for their adaptability and creativity.",
      base_health = 100,
      base_intelligence = 100,
      base_strength = 100,
      base_dexterity = 100,
      base_constitution = 100,
      base_length = 170,
      base_weight = 70,
      base_lifetime = 70 * simulation.LENGTH_OF_YEAR * simulation.LENGTH_OF_MONTH // in days
    }, simulation);

    simulation.AddRace(humanRace);

    for (int i = 0; i < simulation.COUNT_OF_START_HUMANS; i++)
    {
      Names names = new Names();
      int gender = new Random().Next(0, 2);

      Human.Create(new CreateHumanProps
      {
        name = names.GetRandomName(gender),
        gender = gender, // Random
        health = humanRace.base_health,
        intelligence = humanRace.base_intelligence,
        strength = humanRace.base_strength,
        dexterity = humanRace.base_dexterity,
        constitution = humanRace.base_constitution,
        length = humanRace.base_length,
        weight = humanRace.base_weight,
        lifetime = humanRace.base_lifetime,
        spawned_at_age = simulation.age.total_days,
        age = 0
      }, simulation);
    }
  }
}