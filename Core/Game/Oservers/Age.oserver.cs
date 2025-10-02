static class SimulationAgeChangeObserver
{
  public static readonly Observer<AgeChangeEvent> observer = new Observer<AgeChangeEvent>
  {
    action = (ageEvent, simulation) =>
    {
      Race humanRace = simulation.GetRaceByName("Human");

      if (humanRace != null)
      {
        foreach (var specimen in humanRace.specimens)
        {
          if (specimen is Human human)
          {
            int actualAgeInDays = ageEvent.data.age;
            double spawnedAgeInDaysOfHuman = human.spawned_at_age;
            double difBetweenActualAndSpawnedAge = actualAgeInDays - spawnedAgeInDaysOfHuman;

            if (difBetweenActualAndSpawnedAge < 0) difBetweenActualAndSpawnedAge = 0;

            human.age = difBetweenActualAndSpawnedAge;

            if (difBetweenActualAndSpawnedAge >= human.lifetime)
            {
              Console.WriteLine($"[AgeChangeEvent] {human.name} has reached the end of their lifetime and has died at age {(human.age / simulation.LENGTH_OF_MONTH) / simulation.LENGTH_OF_YEAR} years.");
              Human.Show(human);
              humanRace.RemoveSpecimen(human);
              continue;
            }

            if (difBetweenActualAndSpawnedAge % (simulation.LENGTH_OF_MONTH * simulation.LENGTH_OF_YEAR) == 0)
            {
              Console.WriteLine($"[AgeChangeEvent] {human.name} is now {(human.age / simulation.LENGTH_OF_MONTH) / simulation.LENGTH_OF_YEAR} years old.");
              Human.Show(human);
            }
          }
        }
      }
    }
  };
}
