namespace QuestionnaireFirstTry.Models
{
    public class ClaimModel
    {
        static int modelsCount =1;
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public ClaimModel(string type)
        {
            this.Id = modelsCount++;
            this.Type = type;
        }
    }
}
