namespace PokeApiConsumer.Models
{
    public class Pokemon
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public int Id { get; set; }
        public string? name { get; set; }
        public int? base_experience { get; set; }
        public int height { get; set; }
        public bool is_default { get; set; }
        public int order { get; set; }
        public int weight { get; set; }
        public List<Ability>? abilities { get; set; }
        public List<Form>? forms { get; set; }
        public Sprites? sprites { get; set; }
        public string? base64_sprite_default { get; set; }
        public Evolution? evolution { get; set; }

        public class Ability
        {
            public bool is_hidden { get; set; }
            public int slot { get; set; }
            public Ability? ability { get; set; }
        }

        public class Form
        {
            public string? name { get; set; }
            public string? url { get; set; }
        }

        public class Sprites
        {
            public string? back_default { get; set; }
        }

    }
}
