namespace GuildComm.Web.Models.Character
{
    public class CharacterViewModel
    {
        public string Name { get; set; }

        public string Faction { get; set; }

        public string Race { get; set; }

        public string Class { get; set; }

        public string Spec { get; set; }

        public string Realm { get; set; }

        public string Guild { get; set; }

        public int AvgItemLevel { get; set; }

        public int EquippedItemLevel { get; set; }

        public string CharacterRender { get; set; }
    }
}
