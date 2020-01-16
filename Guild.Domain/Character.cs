namespace Guild.Domain
{
    using Guild.Domain.Enums;

    public class Character
    {
        public string Name { get; set; }

        public Role Role { get; set; }

        public Class Class { get; set; }

        public int Level { get; set; }

        public int ItemLevel { get; set; }
    }
}
