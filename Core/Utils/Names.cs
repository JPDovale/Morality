class Names
{
  string brMaleFileNamesPath = "Assets/names/br_male_names.txt";
  string brFemaleFileNamesPath = "Assets/names/br_female_names.txt";
  string brFileLastNamesPath = "Assets/names/br_last_names.txt";

  List<string> brMaleNames = new List<string>();
  List<string> brFemaleNames = new List<string>();
  List<string> brLastNames = new List<string>();

  public Names()
  {
    this.brMaleNames = System.IO.File.ReadAllLines(brMaleFileNamesPath).ToList();
    this.brFemaleNames = System.IO.File.ReadAllLines(brFemaleFileNamesPath).ToList();
    this.brLastNames = System.IO.File.ReadAllLines(brFileLastNamesPath).ToList();
  }

  public string GetRandomName(int gender)
  {
    Random rand = new Random();

    int percentageOfChanceToContainsMoreThanOneFirstName = 30; // 30%

    int chance = rand.Next(100);
    int index = 0;
    List<string> names = new List<string>();

    if (gender == (int)SpecimenGender.MALE)
    {
      index = rand.Next(brMaleNames.Count);
      names = brMaleNames;
    }

    if (gender == (int)SpecimenGender.FEMALE)
    {
      index = rand.Next(brFemaleNames.Count);
      names = brFemaleNames;
    }

    String name = names[index];

    if (chance > percentageOfChanceToContainsMoreThanOneFirstName)
    {
      name = name.Split(" ")[0];
    }

    return name;
  }

}