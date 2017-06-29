namespace LostTimeWeb.WebApp.Models.QuestManagerViewModel
{
    public class QuestViewModel
    {
       public string QuestID { get; set; }
       public string QuestTitle  { get; set; }
       public string QuestData { get; set; }
       public DateTime QuestLastEdit { get; set; }
       public int QuestAuthorID { get; set; }
    }
}