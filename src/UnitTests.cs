namespace KeyGenerator
{
    class UnitTests
    {
        public bool allTeamsHaveDifferentNumbers(Group[] l)
        {
            foreach (Group group in l)
                for (int i = 0; i < group.team.Length; i++)
                    for (int j = i + 1; j < group.team.Length; j++)
                        if (group.team[i].key == group.team[j].key)
                            return false;
            return true;
        }

        public bool allTeamsHaveValidNumbers(Group[] l)
        {
            foreach (Group group in l)
                for (int i = 0; i < group.team.Length; i++)
                    if (group.team[i].key < 1 || group.team[i].key > group.field)
                        return false;
            return true;
        }

        public bool checkValidity(Club[] v)
        {
            return true;
        }
    }
}
